import { createI18n } from 'vue-i18n';
import pl from './pl.json';

export const i18n = createI18n({
    legacy: false, // Używamy Composition API
    locale: 'pl',  // Domyślny język
    fallbackLocale: 'en',
    messages: {
        pl,     
    }
});
