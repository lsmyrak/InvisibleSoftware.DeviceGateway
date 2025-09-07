import { createApp } from 'vue';
import App from './App.vue';
import { i18n } from './translations';
import { router } from './router/index'
import '@/style/main.css'; 
import 'bootstrap/dist/css/bootstrap.css';
import "bootstrap"

const app = createApp(App);
app.use(i18n);
app.use(router);
app.mount('#app');
