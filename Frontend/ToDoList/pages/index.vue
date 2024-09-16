<template>
    <div>Redirecting...</div>
  </template>
  
  <script setup>
  import { useRouter } from 'vue-router'
  import { onMounted } from 'vue'
  
  const router = useRouter()
  
  onCreated(() => {
    router.push('/login')
  })
  </script>