// stores/auth.js
import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useAuthStore = defineStore('auth', () => {
  const isAuthenticated = ref(false)
  const user = ref('')
  const name = ref('')

  const login = async (user_id, full_name) => {
      isAuthenticated.value = true
      user.value = user_id
      name.value = full_name
  }
  const logout = () => {
    isAuthenticated.value = false
    user.value = ""
    name.value = ""
  }

  return { isAuthenticated, user, name, login, logout }
})
