<template>
    <div class="form-container">
      <h2>Register</h2>
      <form @submit.prevent="Register">
        <div class="form-group">
          <label for="name">Nama:</label>
          <input type="text" id="name" v-model="name" required placeholder="Enter your name" />
        </div>
        <div class="form-group">
          <label for="user_id">User ID:</label>
          <input type="text" id="user_id" v-model="user_id" required placeholder="Enter your User ID" />
        </div>
        <div class="form-group">
          <label for="password">Password:</label>
          <input type="password" id="password" v-model="password" required placeholder="Enter your password" />
        </div>
        <button type="submit" class="btn">Register</button>
      </form>
      <p class="footer-text">Sudah punya akun? <NuxtLink to="/login">Login</NuxtLink></p>
    </div>
  </template>
  
  <script setup>
  import { ref } from 'vue'
  import { useRouter } from 'vue-router'
  import { useAuthStore } from '~/store/auth'
  
  const name = ref('')
  const user_id = ref('')
  const password = ref('')
  const authStore = useAuthStore()
  const router = useRouter()
  
  const Register = async () => {
    

  if (user_id.value && password.value && name.value) {
    const body = {
      user_id: user_id.value,
      password: password.value,
      full_name: name.value,
    };
    try {
      const response = await fetch('https://localhost:7296/api/ToDoList/Registration', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(body),
      });

      const result = await response.json();
      if(result.message === "Berhasil Registrasi"){
        alert(result.message);
        router.push('/login');
      }
      else{
        alert(result.message);
      }
    } catch (error) {
      console.error('Register error:', error);
    }
  } else {
    alert("Mohon untuk melengkapi data !")
  }
  }
  </script>
  
  <style scoped>
  .form-container {
    max-width: 400px;
    margin: 50px auto;
    padding: 20px;
    border: 1px solid #ddd;
    border-radius: 8px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    background-color: #fff;
  }
  
  h2 {
    text-align: center;
    margin-bottom: 20px;
    color: #333;
  }
  
  .form-group {
    margin-bottom: 15px;
  }
  
  label {
    display: block;
    margin-bottom: 5px;
    font-weight: bold;
    color: #555;
  }
  
  input {
    width: 100%;
    padding: 10px;
    border: 1px solid #ccc;
    border-radius: 4px;
    box-sizing: border-box;
  }
  
  button.btn {
    width: 100%;
    padding: 10px;
    border: none;
    border-radius: 4px;
    background-color: #28a745;
    color: #fff;
    font-size: 16px;
    cursor: pointer;
    transition: background-color 0.3s;
  }
  
  button.btn:hover {
    background-color: #218838;
  }
  
  .footer-text {
    text-align: center;
    margin-top: 15px;
  }
  
  .footer-text a {
    color: #007bff;
    text-decoration: none;
  }
  
  .footer-text a:hover {
    text-decoration: underline;
  }
  </style>
  