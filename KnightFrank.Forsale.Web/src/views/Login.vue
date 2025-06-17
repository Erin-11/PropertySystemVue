<template>
  <div class="login-container">
    <h2>Log In</h2>
    <el-form @submit.prevent="onLogin" label-width="80px">
      <el-form-item label="username:">
        <el-input v-model="username"
                  placeholder="User name"
                  style="width: 100%">
          <template #prefix>
            <el-icon><User /></el-icon>
          </template>
        </el-input>
      </el-form-item>
      <el-form-item label="password:">
        <div style="position: relative; width: 100%">
          <el-input v-model="password"
                    :type="isPwdVisible ? 'text' : 'password'"
                    placeholder="Password"
                    style="width: 100%"
                    ref="pwdRef">
            <template #prefix>
              <el-icon><Lock /></el-icon>
            </template>
            <template #suffix>
              <el-icon @click="togglePwdVisibility" style="cursor: pointer">
                <component :is="isPwdVisible ? 'View' : 'Hide'" />
              </el-icon>
            </template>
          </el-input>
        </div>
      </el-form-item>
      <el-button type="primary" native-type="submit" style="width: 100%">Login</el-button>
      <p v-if="error" class="error">{{ error }}</p>
    </el-form>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
  import { useRouter } from 'vue-router'

const username = ref('')
const password = ref('')
const error = ref('')
const router = useRouter()

const onLogin = () => {
  if (username.value === 'admin' && password.value === '123456') {
    localStorage.setItem('authToken', 'example-token')
    router.push({ name: 'admin' })
  } else {
    error.value = 'User name or password error'
  }
}

  const isPwdVisible = ref(false)
  const pwdRef = ref()
  const togglePwdVisibility = () => {
    isPwdVisible.value = !isPwdVisible.value
    if (pwdRef.value) {
      const inputElement = pwdRef.value.$el.querySelector('input')
      if (inputElement) {
        inputElement.blur()
      }
    }
  }
</script>

<style scoped>
  .login-container {
    max-width: 400px;
    margin: 100px auto;
    padding: 20px;
  }

  h2 {
    font-weight: bold;
    margin-bottom: 10px;
  }

  :deep(.el-form-item__label) {
    font-weight: bold;
  }

  :deep(.el-input),
  :deep(.el-input .el-input__wrapper),
  :deep(.el-input__inner) {
    width: 100%;
    box-sizing: border-box;
  }

  :deep(.el-input__wrapper.is-focus) {
    box-shadow: 0 0 0 1px #ed1944;
  }

  :deep(.el-button:not(.is-disabled)) {
    background-color: #ed1944;
    border-color: #ed1944;
    color: white;
    border-radius: 10px;
  }

  :deep(.el-button:not(.is-disabled):hover) {
    background-color: #d0103a;
  }

    .error {
      color: red;
      margin-top: 10px;
    }
</style>
