﻿import axios from 'axios';
const api = axios.create({
    baseURL: 'http://localhost:8080/api',
    timeout: 10000,
    headers: {
        'Content-Type': 'application/json',
    },
})
api.interceptors.request.use(config => {
    const token = localStorage.getItem('devicegateway.token')
    if (token) {
        config.headers.Authorization = `Bearer ${token}`
    }
    return config
})

export default api