<script setup lang="ts">
import { ref } from "vue"
import axios from "axios"
import FlexiFormSelectLookup from "@/components/commons/FlexiFormSelectLookup.vue"

const device = ref({
  name: "",
  description: "",
  ipAddress: "",
  deviceTypeId: null as string | null,
  manufacturer: "",
  model: "",
  serialNumber: "",
  firmwareVersion: "",
  roomId: null as string | null,
  deviceGroupIds: [] as string[],
  mqttPayloadOrders: [] as { mqttPayloadId: string | null; displayOrder: number }[]
})

const submitForm = async () => {
  try {
    const payload = { device: device.value }
    console.log("Sending payload:", payload)

    await axios.post("/api/devices", payload)
    alert("Device saved successfully ✅")
  } catch (err) {
    console.error("Error saving device:", err)
    alert("❌ Error while saving device")
  }
}
</script>

<template>
  <form @submit.prevent="submitForm" class="space-y-4">

    <!-- Name -->
    <div>
      <label class="block text-sm font-medium">Name</label>
      <input v-model="device.name" type="text" class="border p-2 rounded w-full" />
    </div>

    <!-- Description -->
    <div>
      <label class="block text-sm font-medium">Description</label>
      <textarea v-model="device.description" class="border p-2 rounded w-full"></textarea>
    </div>

    <!-- IP Address -->
    <div>
      <label class="block text-sm font-medium">IP Address</label>
      <input v-model="device.ipAddress" type="text" class="border p-2 rounded w-full" />
    </div>

    <!-- Device Type -->
    <div>
      <FlexiFormSelectLookup
        v-model="device.deviceTypeId"
        id="deviceType"
        label="deviceType"        
        url="/Device/lookup-device-type"
      />
    </div>

    <!-- Manufacturer -->
    <div>
      <label class="block text-sm font-medium">Manufacturer</label>
      <input v-model="device.manufacturer" type="text" class="border p-2 rounded w-full" />
    </div>

    <!-- Model -->
    <div>
      <label class="block text-sm font-medium">Model</label>
      <input v-model="device.model" type="text" class="border p-2 rounded w-full" />
    </div>

    <!-- Serial Number -->
    <div>
      <label class="block text-sm font-medium">Serial Number</label>
      <input v-model="device.serialNumber" type="text" class="border p-2 rounded w-full" />
    </div>

    <!-- Firmware Version -->
    <div>
      <label class="block text-sm font-medium">Firmware Version</label>
      <input v-model="device.firmwareVersion" type="text" class="border p-2 rounded w-full" />
    </div>

    <!-- Room -->
    <div>
      <FlexiFormSelectLookup
        v-model="device.roomId"
        id="room"
        label="room"
        url="/Device/loopup-room"
      />
    </div>

    <!-- Device Groups -->
    <div>
      <FlexiFormSelectLookup
        v-model="device.deviceGroupIds"
        id="deviceGroups"
        label="deviceGroups"
        url="/Device/lookup-device-group"
        multiselect
      />
    </div>

    <!-- MQTT Payload Orders -->
    <div>
      <label class="block text-sm font-medium">MQTT Payload Orders</label>
      <div
        v-for="(order, index) in device.mqttPayloadOrders"
        :key="index"
        class="flex gap-2 mb-2"
      >
        <FlexiFormSelectLookup
          v-model="order.mqttPayloadId"
          id="mqttPayload"
          label="mqttPayload"
          url="/api/mqttPayloads/lookup"
        />
        <input
          v-model.number="order.displayOrder"
          type="number"
          class="border p-2 rounded w-24"
          placeholder="Order"
        />
        <div>
          <input 
          v-model=""
          </div>
      </div>

      <button
        type="button"
        @click="device.mqttPayloadOrders.push({ mqttPayloadId: null, displayOrder: 0 })"
        class="px-3 py-1 bg-blue-500 text-white rounded"
      >
        + Add Payload Order
      </button>
    </div>

    <!-- Submit -->
    <div>
      <button type="submit" class="px-4 py-2 bg-green-600 text-white rounded">
        Save
      </button>
    </div>

  </form>
</template>
