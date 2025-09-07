import type { RouteRecordRaw } from 'vue-router'

export const settings: RouteRecordRaw[] = [
  {
    path: '/settings',
    name: 'settings',
    component: () => import('@/views/settings/SettingsView.vue'),
    meta: { requiresAuth: true },
  },
  {
    path: '/addEditDevice',
    name: 'addEditDevice',
    component: () => import('@/views/settings/device/AddEditForm.vue'),
    meta: { requiresAuth: true },
  },
]