<template>
  <div class="todo-container">
    <button style="float: right;" @click="Logout()">Logout</button>
    <h1>Hello, {{ authStore.name }}</h1>
    
    <form @submit.prevent="addTodo" class="form-container">
      <input v-model="newTodo.subject" type="text" placeholder="Subject" required />
      <input v-model="newTodo.description" type="text" placeholder="Description" required />
      <button type="submit">Add Data</button>
    </form>

    <table class="todo-table">
      <thead>
        <tr>
          <th>ID</th>
          <th>Subject</th>
          <th>Description</th>
          <th>Status</th>
          <th>Mark </th>
          <th>Action</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(todo, index) in todos" :key="todo.id">
          <td>{{ todo.iD_Show }}</td>
          <td>{{ todo.subject }}</td>
          <td>{{ todo.description }}</td>
          <td>{{ todo.status }}</td>
          <td>{{ todo.mark }}</td>
          <td>
            <button @click="openEditDialog(index)">Edit</button>
            <button @click="deleteTodo(index)">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>

    <div v-if="isDialogOpen" class="dialog-overlay">
      <div class="dialog">
        <h2>Edit ToDo</h2>
        <form @submit.prevent="updateTodo">
          <label>Subject</label>
          <input :readonly="editTodo.subjectread" v-model="editTodo.subject" type="text" required />
          
          <label>Description</label>
          <input :readonly="editTodo.descriptionread" v-model="editTodo.description" type="text" required />
          
          <label>Status</label>
          <select v-model="editTodo.status" required>
            <option value="Done">Done</option>
            <option value="Canceled">Canceled</option>
            <option value="Not Set">Not Set</option>
          </select>

          <label>Mark</label>
          <select v-model="editTodo.mark" required>
            <option value="Marked">Marked</option>
            <option value="Unmarked">UnMarked</option>
          </select>
          
          <button type="submit">Save</button>
          <button type="button" @click="closeDialog">Cancel</button>
        </form>
      </div>
    </div>
  </div>
</template>

<script setup>
import { reactive, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '~/store/auth'

const authStore = useAuthStore()
const isDialogOpen = ref(false)
const editTodo = ref({ id: null, subjectread: false, descriptionread: false, subject: '', description: '', status: '', mark: 0 })
const currentEditIndex = ref(null)
const todos = ref([])
const router = useRouter()

// Open dialog for editing
const openEditDialog = (index) => {
  currentEditIndex.value = index
  console.log(todos.value[index].mark)
  editTodo.value.id = todos.value[index].iD_Show
  editTodo.value.subject = todos.value[index].subject
  editTodo.value.description = todos.value[index].description
  editTodo.value.status = todos.value[index].status
  editTodo.value.mark = todos.value[index].mark
  if(todos.value[index].mark === "Marked") {
    editTodo.value.subjectread = true
    editTodo.value.descriptionread = true
  } else {
    editTodo.value.subjectread = false
    editTodo.value.descriptionread = false
  }
  isDialogOpen.value = true
}

const closeDialog = () => {
  isDialogOpen.value = false
}
const newTodo = reactive({
  subject: '',
  description: ''
})

onMounted(async () => {
  getData();
})

const addTodo = async () => {
  const body = {
    user_id: authStore.user,
    Subject: newTodo.subject,
    Description: newTodo.description,
  };
  console.log(editTodo.value.id)
  try {
    const response = await fetch('https://localhost:7296/api/ToDoList/InsertList', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(body),
    });

    const result = await response.json();
    if(result.message === "Berhasil Menambahkan Data"){
      alert(result.message)
      getData()
    }
    else{
      alert(result.message);
    }
  } catch (error) {
    alert('ERROR')
  }
}

const deleteTodo = async (index) => {
  const body = {
    ID_Show: todos.value[index].iD_Show,
  };
  console.log(editTodo.value.id)
  try {
    const response = await fetch('https://localhost:7296/api/ToDoList/DeleteList', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(body),
    });

    const result = await response.json();
    if(result.message === "Berhasil Menghapus Data"){
      alert(result.message)
      getData()
    }
    else{
      alert(result.message);
    }
  } catch (error) {
    alert('ERROR')
  }
}

const updateTodo = async () => {
  const body = {
    ID_Show: editTodo.value.id,
    Subject: editTodo.value.subject,
    Description: editTodo.value.description,
    Status: editTodo.value.status,
    Mark: editTodo.value.mark,
  };
  console.log(editTodo.value.id)
  try {
    const response = await fetch('https://localhost:7296/api/ToDoList/UpdateList', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(body),
    });

    const result = await response.json();
    if(result.message === "Berhasil Mengubah Data"){
      closeDialog()
      getData()
    }
    else{
      alert(result.message);
    }
  } catch (error) {
    alert('ERROR')
  }
}

const getData = async () => {
  const body = {
      user_id: authStore.user,
    };
    try {
      const response = await fetch('https://localhost:7296/api/ToDoList/GetListTodo', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(body),
      });

      const result = await response.json();
      if(result[0].message === "Berhasil"){
        todos.value = result
      }
      else{
        alert(result[0].message);
      }
    } catch (error) {
      alert('ERROR')
    }
}

const Logout = () => {
  authStore.logout();
  if (!authStore.isAuthenticated) {
    router.push('/login');
    console.log('Logout berhasil!');
  } else {
    console.log('Logout failed, please check your credentials.');
  }
}
</script>

<style scoped>
.todo-container {
  padding: 20px;
  max-width: 800px;
  margin: auto;
}

.form-container {
  display: flex;
  margin-bottom: 20px;
}

.form-container input[type="text"] {
  padding: 8px;
  margin-right: 10px;
  flex: 1;
}

.form-container button {
  padding: 10px 20px;
  background-color: #007bff;
  color: white;
  border: none;
  cursor: pointer;
}

.form-container button:hover {
  background-color: #0056b3;
}

.todo-table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 20px;
  table-layout: fixed;
}

.todo-table th,
.todo-table td {
  border: 1px solid #ddd;
  padding: 12px;
  text-align: left;
  word-wrap: break-word;
}

.todo-table th {
  background-color: #f4f4f4;
}

.todo-table td:nth-child(3) {
  max-width: 200px;
}

.todo-table button {
  padding: 5px 10px;
  margin-right: 5px;
  background-color: #28a745;
  color: white;
  border: none;
  cursor: pointer;
}

.todo-table button:hover {
  background-color: #218838;
}

.todo-table button:last-of-type {
  background-color: #dc3545;
}

.todo-table button:last-of-type:hover {
  background-color: #c82333;
}

.dialog-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
}

.dialog {
  background-color: white;
  padding: 20px;
  border-radius: 8px;
  max-width: 400px;
  width: 100%;
}

.dialog input,
.dialog select {
  width: 100%;
  padding: 8px;
  margin-bottom: 10px;
}

.dialog button {
  padding: 10px 20px;
  margin-right: 10px;
}
</style>
