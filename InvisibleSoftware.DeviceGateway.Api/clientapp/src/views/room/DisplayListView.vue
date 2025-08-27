<template>
  <RoomDevices :data="roomDevices" />
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import RoomDevices from '@/components/commons/RoomDevices.vue'
import api from '@/services/api'

const roomDevices = ref([])

onMounted(async () => {
  try {
    const response = await api.get('/api/Room/rooms-with-devices')
    roomDevices.value = response.data
  } catch (error) {
  
    console.error(error)
  }
})
</script>