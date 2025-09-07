import type { RouteRecordRaw } from 'vue-router'

export const auth: RouteRecordRaw[] = [
  {
    path: '/login',
    name: 'login',
    component: () => import('@/views/auth/LoginView.vue'),
  },
  {
    path: '/register',
    name: 'register',
    component: () => import('@/views/auth/RegisterView.vue'),
  },
  {
    path: '/editUser',
    name: 'editUser',
    component: () => import('@/views/settings/user/EditForm.vue'),
    meta: { requiresAuth: true },
  },
]
