<template>
    <div class="auth-container">
       <label class="auth-label">{{$t("login")}}</label> 
       <input class="auth-input" v-model="form.email" type="text" :placeholder="$t('email')" />
       <label class="auth-label">{{$t("password")}}</label>
        <input class="auth-input" v-model="form.password" type="password" :placeholder="$t('password')" />
        <button @click="handleLogin">{{$t('login')}} </button>
    </div>
</template>

<script setup lang="ts">
    import { reactive } from 'vue';
    import { useRouter, useRoute } from 'vue-router'
    import api from '@/services/api'

    const router = useRouter();
    const route = useRoute();

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
            const redirectPath = (route.query.redirect as string) || '/';
            if (redirectPath) {
                router.push(redirectPath);
            } else {
                router.push('/');
            }

        } catch (error) {
            console.log("błąd logowania");
        }
    }
</script>

