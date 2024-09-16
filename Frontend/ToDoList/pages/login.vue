<template>
    <div class="form-container">
      <h2>Login</h2>
      <form @submit.prevent="handleLogin">
        <div class="form-group">
          <label for="user_id">User ID:</label>
          <input type="text" id="user_id" v-model="user_id" required placeholder="Enter your email" />
        </div>
        <div class="form-group">
          <label for="password">Password:</label>
          <input type="password" id="password" v-model="password" required placeholder="Enter your password" />
        </div>
        <button type="submit" class="btn">Login</button>
      </form>
      <p class="footer-text">Belum punya akun? <NuxtLink to="/register">Register</NuxtLink></p>
    </div>
  </template>
  
  <script setup>
  import { ref } from 'vue'
  import { useRouter } from 'vue-router'
  import { useAuthStore } from '~/store/auth'
  
  const user_id = ref('')
  const password = ref('')
  const authStore = useAuthStore()
  const router = useRouter()
  
  const handleLogin = async () => {
  const body = {
    user_id: user_id.value,
    password: password.value
  };

  if (user_id.value && password.value) {
    try {
      const response = await fetch('https://localhost:7296/api/ToDoList/Login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(body),
      });

      // Check if response is OK
      if (!response.ok) {
        const errorText = await response.text(); // Get error message from response
        throw new Error(`Server Error: ${errorText}`);
      }
      const result = await response.json();
      console.log(result);

      // Assuming authStore has a method `login` that needs to be called
      if(result[0].message !== "Berhasil"){
        return alert(result[0].message)
      }
      await authStore.login(user_id.value, result[0].full_name);
      
      if (authStore.isAuthenticated) {
        router.push('/todoList');
        console.log('Login berhasil!');
      } else {
        console.log('Login failed, please check your credentials.');
      }
      
    } catch (error) {
      // Handle fetch errors or server errors
      console.error('Login error:', error);
      // You can also display an error message to the user here
    }
  } else {
    console.log('Please provide both user_id and password.');
  }
};
  
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
    background-color: #007bff;
    color: #fff;
    font-size: 16px;
    cursor: pointer;
    transition: background-color 0.3s;
  }
  
  button.btn:hover {
    background-color: #0056b3;
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
  