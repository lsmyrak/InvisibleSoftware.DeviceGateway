<template>
  <div v-if="visible" class="modal-overlay">
    <div class="modal-content">
      <h5 class="modal-title">
        {{ isAdd ? $t(translatePrefix + "add") : $t(translatePrefix + "edit") }}
      </h5>
      <button
        type="button"
        class="btn-close"
        @click="emit('close')"
        aria-label="Close"
      ></button>

      <div class="modal-body">
        <div class="mb-3">
          <label for="topic" class="form-label">
            {{ $t(translatePrefix + "topic") }}
          </label>
          <input
            type="text"
            class="form-control"
            id="topic"
            v-model="payload.topic"
          />
        </div>

        <div class="mb-3">
          <label for="payload" class="form-label">
            {{ $t(translatePrefix + "payload") }}
          </label>
          <input
            type="text"
            class="form-control"
            id="payload"
            v-model="payload.payload"
          />
        </div>

        <div class="mb-3">
          <label for="commandName" class="form-label">
            {{ $t(translatePrefix + "command.name") }}
          </label>
          <input
            type="text"
            class="form-control"
            id="commandName"
            v-model="payload.commandName"
          />
        </div>
        
        <div class="mb-3">
          <label for="displayCommandName" class="form-label">
            {{ $t(translatePrefix + "display.command.name") }}
          </label>
          <input
            type="text"
            class="form-control"
            id="displayCommandName"
            v-model="payload.displayCommandName"
          />
      </div>

      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" @click="emit('close')">
          {{ $t("close") }}
        </button>
        <button type="button" class="btn btn-primary" @click="savePayload">
          {{ $t("save") }}
        </button>
      </div>
    </div>
  </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch } from "vue";
import api from "@/services/api";

const translatePrefix = "settings.payload.";

const props = defineProps<{
  visible: boolean;
  model?: Payload | null; // model może być obiektem albo null
}>();

const emit = defineEmits<{
  (e: "close"): void;
  (e: "saved"): void;
}>();

interface Payload {
  topic: string;
  payload: string;
  commandName: string;
  displayCommandName: string;
}

const payload = ref<Payload>({
  topic: "",
  payload: "",
  commandName: "",
  displayCommandName: "",
});

const isAdd = computed(() => !props.model);


watch(
  () => props.visible,
  (newVal) => {
    if (newVal) {
      if (props.model) {
        payload.value = { ...props.model };
      } else {
        payload.value = {
          topic: "",
          payload: "",
          commandName: "",
          displayCommandName: "",
        };
      }
    }
  }
);

const savePayload = async () => {
  try {
    const endpoint = isAdd.value
      ? "/api/setting/add-payload"
      : "/api/setting/edit-payload";

    await api.post(endpoint, payload.value);

    emit("saved");
    emit("close");
  } catch (error) {
    console.error("Error saving payload:", error);
  }
};
</script>
