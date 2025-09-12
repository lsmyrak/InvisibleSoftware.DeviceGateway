
<template>
  <div class="container mx-auto p-4">
    <form @submit.prevent="submitForm" class="space-y-4">
      <div>
        <label class="block text-sm font-medium">{{ $t( translatePrefix+"name") }}</label>
        <input v-model="device.name" type="text" class="border" />
      </div>

        <div>
        <label class="block text-sm font-medium">{{ $t(translatePrefix+"description") }}</label>
        <input v-model="device.description" class="border"></input>
      </div>

      <div>
        <label class="block text-sm font-medium">{{ $t(translatePrefix+"ip.address") }}</label>
        <input v-model="device.ipAddress" type="text" class="border" />
      </div>
      <div>
        <label class="block text-sm font-medium">{{ $t(translatePrefix+"manufacturer") }}</label>
        <input v-model="device.manufacturer" type="text" class="border p-2 rounded w-full" />
      </div>

      <!-- Model -->
      <div>
        <label class="block text-sm font-medium">{{ $t(translatePrefix+"model") }}</label>
        <input v-model="device.model" type="text" class="border p-2 rounded w-full" />
      </div>

      <!-- Serial Number -->
      <div>
        <label class="block text-sm font-medium">{{ $t(translatePrefix+"serial.number") }}</label>
        <input v-model="device.serialNumber" type="text" class="border p-2 rounded w-full" />
      </div>

      <!-- Firmware Version -->
      <div>
        <label class="block text-sm font-medium"> {{ $t(translatePrefix+"firmware.version") }}</label>
        <input v-model="device.firmwareVersion" type="text" class="border p-2 rounded w-full" />
      </div>

      <div>

        <FlexiFormSelectLookup
          v-model="device.deviceTypeId"
          id="deviceType"
          label="device.type"
          url="/Device/lookup-device-type"
          translatePrefix="device.form.add."
        />
      </div>


      <!-- Room -->
      <div>
        <FlexiFormSelectLookup
          v-model="device.roomId"
          id="room"
          label="room"
          url="/Device/loopup-room"
          translatePrefix="device.form.add."
        />
      </div>

      <!-- Device Groups -->
      <div>
        <div>
          <label class="block text-sm font-medium mb-2">{{ $t(translatePrefix+"getExistent") }}</label>
          <button type="button" @click="createNewPayload = true" class="px-3 py-1 bg-blue-500 text-white rounded">          
            {{ $t(translatePrefix + "create.new.mqtt.payload") }}
          </button>
        
        <FlexiFormSelectLookup
          v-model="device.deviceGroupIds"
          id="deviceGroups"
          label="device.groups"
          url="/Device/lookup-device-group"
          multiselect
          translatePrefix="device.form.add."
        />
      </div>
      </div>

      {{$t(translatePrefix +"mqtt.payload.orders")}}
       <div>
   
    <AddEditMqttPayloadModal
      :visible="createNewPayload"
      @close="closeModal"
      @payload-saved="handlePayloadSaved"      
    />
  </div>
      <div class="container-mx mx-auto p-4 border rounded">        
        <div>
          <label class="block text-sm font-medium mb-2">{{ $t(translatePrefix+"getExistent") }}</label>
          <button type="button" @click="createNewPayload = true" class="px-3 py-1 bg-blue-500 text-white rounded">          
            {{ $t(translatePrefix + "create.new.mqtt.payload") }}
          </button>
        <div
          v-for="(order, index) in device.mqttPayloadOrders"
          :key="index"
          class="flex gap-2 mb-2"
        >
          <FlexiFormSelectLookup
            v-model="order.mqttPayloadId"
            id="mqttPayload"
            label="mqttPayload"
            translatePrefix="device.form.add."
            url="/Settings/lookup-payload"
          />
          <div>
            <label class="block text-sm font-medium mb-1">{{ $t(translatePrefix+"order") }}</label>
          <input
            v-model.number="order.displayOrder"
            type="number"
            class="border p-2 rounded w-24"
            placeholder="Order"
          />   
          </div>     
        </div>
    </div>
        <button
          type="button"
          @click="device.mqttPayloadOrders.push({ mqttPayloadId: null, displayOrder: 0 })"
          class="px-3 py-1 bg-blue-500 text-white rounded">
        {{ $t(translatePrefix + "add.payloadorder") }}
        </button>
      </div>

      Submit
      <div>
        <button type="submit" class="px-4 py-2 bg-green-600 text-white rounded">
          Save
        </button>
      </div>

    </form>
    </div>
</template>
<script setup lang="ts">
import { ref } from "vue"
import api from "@/services/api"
import FlexiFormSelectLookup from "@/components/commons/FlexiFormSelectLookup.vue"
import AddEditMqttPayloadModal from "@/views/settings/device/modals/AddEditMqttPayloadModal.vue"

const translatePrefix = "device.form.add."

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
const mqttPayload
  = ref({     
    name: "",
    topic: "",
    payload: "",
    commandName: "",
    displayCommandName: "",
  })
const createNewPayload = ref(false)
const handlePayloadSaved = () => {
  
  createNewPayload.value = false;
};

const closeModal = () => {
  createNewPayload.value = false
}
const mqttPayloadOrders = ref<{ mqttPayloadId: string | null; displayOrder: number }[]>([])
  
const submitForm = async () => {
  try {
    const payload = { device: device.value }
    await api.post("/Device/device/add", payload)
  } catch (err) {        
  }
}
</script>
