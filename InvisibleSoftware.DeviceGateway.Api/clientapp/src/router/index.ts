// src/router/main.ts
import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router'
import { rooms } from './room'
import { auth } from './auth'
import { settings } from './setting'

const baseRoutes: RouteRecordRaw[] = [
  {
    path: '/',
    name: 'home',
    component: () => import('@/views/Home.vue'),
    meta: { requiresAuth: true },
  },
  {
    path: '/about',
    name: 'about',
    component: () => import('@/views/About.vue'),
    meta: { requiresAuth: true },
  },
]

const routes: RouteRecordRaw[] = [
  ...baseRoutes,
  ...rooms,
  ...auth,
  ...settings

]

export const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
})

function parseJwt(token: string): any | null {
  try {
    const base64Url = token.split('.')[1]
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/')
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split('')
        .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
        .join('')
    )
    return JSON.parse(jsonPayload)
  } catch {
    return null
  }
}

function isTokenValid(): boolean {
  const token = localStorage.getItem('devicegateway.token')
  if (!token) return false

  const payload = parseJwt(token)
  if (!payload) return false

  const now = Math.floor(Date.now() / 1000)
  return payload.exp && payload.exp > now
}


router.beforeEach((to, from, next) => {
  const isLoggedIn = isTokenValid()
    if (to.meta.requiresAuth && !isLoggedIn) {
    next('/login')
  } else {
    next()
  }
})