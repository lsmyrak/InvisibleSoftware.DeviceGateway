
import type { RouteRecordRaw } from 'vue-router'

export const rooms: RouteRecordRaw[] = [
  {
    path: '/rooms',
    name: 'rooms',
    component: () => import('@/views/room/DisplayListView.vue'),
    meta: { requiresAuth: true},
  },
  
]
