<template>
  <div v-for="room in groupedRooms" :key="room.name" class="room-block">
    <h2>{{ $t(room.name) }}</h2>

    <div v-for="device in room.devices" :key="device.id" class="device-block">
      <div class="device-header" @click="toggleDevice(device.id)">
        <strong>{{ $t(device.name) }}</strong>
      </div>

      <ul v-if="expandedDevices.includes(device.id)" class="payload-list">
        <li v-for="order in sortedOrders(device)" :key="order.id">
          <button @click="executeOrder(order.mqttPayload.id)">
            {{ $t(order.mqttPayload.displayCommandName) }}
          </button>
        </li>
      </ul>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import api from '@/services/api'
interface MqttPayloadOrder {
  id: string
  displayOrder: number
  mqttPayload: {
    id: string
    displayCommandName: string
  }
}

interface Device {
  id: string
  name: string
  room: {
    name: string
  }
  mqttPayloadOrders: MqttPayloadOrder[]
}

interface RawItem {
  device: Device
}

const props = defineProps<{ data: RawItem[] }>()

const expandedDevices = ref<string[]>([])

function toggleDevice(deviceId: string) {
  if (expandedDevices.value.includes(deviceId)) {
    expandedDevices.value = expandedDevices.value.filter(id => id !== deviceId)
  } else {
    expandedDevices.value.push(deviceId)
  }
}

function sortedOrders(device: Device): MqttPayloadOrder[] {
  return [...device.mqttPayloadOrders].sort((a, b) => a.displayOrder - b.displayOrder)
}

function executeOrder(payloadId: string) {
  
  api.post(`/Room/execute-command/${payloadId}`).catch(error => {    
  })
  
}

const groupedRooms = computed(() => {
  const map = new Map<string, { name: string; devices: Device[] }>()

  for (const item of props.data) {
    const roomName = item.device.room.name
    if (!map.has(roomName)) {
      map.set(roomName, { name: roomName, devices: [] })
    }
    map.get(roomName)!.devices.push(item.device)
  }

  return Array.from(map.values())
})
</script>

<style scoped>
.room-block {
  margin-bottom: 2rem;
  padding: 1rem;
  background-color: #f9f9f9;
  border-radius: 8px;
}
.device-block {
  margin-top: 1rem;
  padding: 0.5rem;
  border: 1px solid #ddd;
  border-radius: 6px;
}
.device-header {
  cursor: pointer;
  font-weight: bold;
}
.payload-list {
  margin-top: 0.5rem;
  padding-left: 1rem;
}
button {
  padding: 0.3rem 0.6rem;
  border: 1px solid #ccc;
  border-radius: 4px;
  background: #fff;
  cursor: pointer;
}
button:hover {
  background: #f0f0f0;
}
</style>
