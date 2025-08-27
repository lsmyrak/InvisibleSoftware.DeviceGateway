<template>
    <div class="register-container">
        <h1>{{$t("register")}}</h1>
        <input 
            type="text"
            v-model="form.userName"
            placeholder="{{$t('username')}}"
            required>
        <input 
            type="email"
            v-model="form.email"
            placeholder="{{$t('email')}}"
            required>
        <input
            type="password"
            v-model="form.password"
            placeholder="{{$t('password')}}"
            required>
        <button @click="handleRegister">{{$t('register')}}</button>
    </div>
</template>
<script setup lang="ts">
import { reactive } from 'vue';
import api from '@/services/api';
interface RegisterForm {
    userName: string;
    email: string;
    password: string;
}

const form = reactive<RegisterForm>({
    userName: '',
    email: '',
    password: ''
});
const handleRegister = async (event: Event) => {
    event.preventDefault();
    try {
        const response = await api.post('/Auth/register', {
            registerDto: {
                userName: form.userName,
                email: form.email,
                password: form.password
            }
        });
        const token = response.data.token;
        localStorage.setItem('devicegateway.token', token);
    } catch (error) {
        console.log("błąd rejestracji");
    }
}
</script>