import { createApp } from 'vue';
import App from './App.vue';
import { i18n } from './translations';
import '@/style/main.css'; 

const app = createApp(App);
app.use(i18n); // to automatycznie dodaje $t do globalProperties
app.mount('#app');
