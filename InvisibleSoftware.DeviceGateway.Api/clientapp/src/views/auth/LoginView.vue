<template>
    <div class="auth-container">
        <input v-model="form.email" type="text" :placeholder="$t('email')" />
        <input v-model="form.password" type="password" :placeholder="$t('password')" />
        <button @click="handleLogin">{{$t('login')}} </button>
    </div>
</template>

<script setup lang="ts">
    import { reactive } from 'vue';
    import api from '@/services/api'

    interface LoginForm {
        email: string
        password: string
    }

    const form = reactive<LoginForm>({
        email: '',
        password: ''
    })

    const handleLogin = async (event: Event) => {
        event.preventDefault();
        try {
            const response = await api.post('/Auth/login', {
                loginDto: {
                    email: form.email,
                    password: form.password
                }
            })
            const token = response.data.token
            localStorage.setItem('devicegateway.token', token)

        } catch (error) {
            console.log("błąd logowania");
        }
    }
</script>

