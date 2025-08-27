// src/router/main.ts
import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router'
import { rooms } from './room'
import { auth } from './auth'

const baseRoutes: RouteRecordRaw[] = [
  {
    path: '/',
    name: 'home',
    component: () => import('@/views/Home.vue'),
  },
  {
    path: '/about',
    name: 'about',
    component: () => import('@/views/About.vue'),
  },
]

const routes: RouteRecordRaw[] = [
  ...baseRoutes,
  ...rooms,
  ...auth,

]

export const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
})
