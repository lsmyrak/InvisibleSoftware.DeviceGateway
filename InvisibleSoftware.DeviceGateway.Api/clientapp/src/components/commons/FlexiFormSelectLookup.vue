<template>
  <div class="flexi-form-select-lookup">
    <label :for="id" class="form-label">{{ $t(translateLabel) }}</label>


    <div v-if="multiselect && Array.isArray(modelValue)" class="selected-items">
      <span v-for="val in modelValue" :key="val" class="selected-item">
        {{ getDisplayById(val) }}
        <span v-if="!required" class="remove" @click="removeItem(val)">❌</span>
      </span>
    </div>
<div v-else-if="modelValue != null && !Array.isArray(modelValue)" class="selected-item">
  {{ getDisplayById(modelValue) }}
  <span v-if="!required" class="remove" @click="removeItem(modelValue)">❌</span>
</div>

    <input
      v-model="searchQuery"
      type="text"
      class="form-control mb-2"
      @focus="showDropdown = true"
      @blur="hideDropdown"
    />
  
    <div v-if="showDropdown && filteredData.length > 0" class="dropdown-menu show table-dropdown">
  <table class="table table-hover table-sm mb-0">
    <thead>
      <tr>
        <th v-for="col in columns.filter(c => c.display)" :key="col.key">
          {{ $t(col.label ?? col.key) }}
        </th>
      </tr>
    </thead>
    <tbody>
      <tr
        v-for="item in filteredData"
        :key="getValue(item, { key: 'id' }) ?? ''"
        @click="selectItem(item)"
        class="dropdown-row"
      >
        <td v-for="col in columns.filter(c => c.display)" :key="col.key">
          {{ getValue(item, col) }}
        </td>
      </tr>
    </tbody>
  </table>
</div>

  </div>
</template>
<script setup lang="ts">
import { ref, computed, watch, onMounted } from "vue"
import api from "@/services/api"

interface LookupColumn {
  key: string
  label?: string
  display?: boolean
}

interface LookupItem {
  [key: string]: unknown
}

const props = defineProps<{
  label: string
  translatePrefix?: string 
  id: string
  url: string
  modelValue: string | number | (string | number)[] | null
  enableSearch?: boolean
  className?: string
  required?: boolean
  multiselect?: boolean
}>()

const emit = defineEmits<{
  (e: "update:modelValue", value: string | number | (string | number)[] | null): void
}>()

const searchQuery = ref("")
const showDropdown = ref(false)
const columns = ref<LookupColumn[]>([])
const data = ref<LookupItem[]>([])
var translateLabel = ""

if (props.translatePrefix === undefined) {
  translateLabel = props.label
}
else {
  var translateLabel = props.translatePrefix + props.label
}
const debouncedQuery = ref("")
let debounceTimeout: ReturnType<typeof setTimeout> | null = null

watch(searchQuery, (val) => {
  if (debounceTimeout) clearTimeout(debounceTimeout)
  debounceTimeout = setTimeout(() => {
    debouncedQuery.value = val
  }, 300)
})

onMounted(async () => {
  try {
    const response = await api.get(props.url)
    const rawColumns = response.data.columns || []
    const rawData = response.data.data || []

    // mapuj klucze na lowercase
    columns.value = rawColumns.map((col: any) => ({
      key: col.key.toLowerCase(),
      label: col.label,
      display: col.display
    }))

    data.value = rawData.map((item: any) => {
      const mapped: Record<string, unknown> = {}
      for (const key in item) {
        mapped[key.toLowerCase()] = item[key]
      }
      return mapped
    })
  } catch (error) {
    console.error("Błąd ładowania danych lookup:", error)
  }
})

function getValue(item: LookupItem, column: { key: string }): string | number | null {
  return item[column.key] as string | number | null
}

const filteredData = computed(() => {
  if (!props.enableSearch || !debouncedQuery.value) {
    return data.value
  }

  return data.value.filter((item) =>
    columns.value.some((column) =>
      String(getValue(item, column) ?? "")
        .toLowerCase()
        .includes(debouncedQuery.value.toLowerCase())
    )
  )
})

function getDisplayValue(item: LookupItem): string {
  const displayColumns = columns.value.filter((col) => col.display)
  return displayColumns
    .map((col) => getValue(item, col))
    .filter((val) => val != null)
    .join(" | ") 
}


function getDisplayById(id: string | number): string | number {
  const item = data.value.find((i) => getValue(i, { key: "id" }) === id)
  return item ? getDisplayValue(item) ?? id : id
}

function selectItem(item: LookupItem) {
  const value = getValue(item, { key: "id" })
  if (value == null) return

  if (props.multiselect) {
    const current = props.modelValue && Array.isArray(props.modelValue)
      ? [...props.modelValue]
      : []
    if (!current.includes(value)) {
      emit("update:modelValue", [...current, value])
    }
  } else {
    emit("update:modelValue", value)
    searchQuery.value = String(getDisplayValue(item) ?? "")
    showDropdown.value = false
  }
}

function removeItem(value: string | number) {
  if (props.multiselect) {
    const current = props.modelValue && Array.isArray(props.modelValue)
      ? [...props.modelValue]
      : []
    emit("update:modelValue", current.filter((v) => v !== value))
  } else {
    emit("update:modelValue", null)
    searchQuery.value = ""
  }
}

function hideDropdown() {
  setTimeout(() => {
    showDropdown.value = false
  }, 200)
}
</script>



<style scoped>
.flexi-form-select-lookup {
  position: relative;
  margin-bottom: 1rem;
}

.dropdown-menu {
  position: absolute;
  z-index: 100;
  width: 100%;
  background-color: lightgreen;
  box-shadow: 0px 8px 16px rgba(0, 0, 0, 0.2);
  max-height: 200px;
  overflow-y: auto;
}

.list-group-item {
  cursor: pointer;
}
.list-group-item:hover {
  background-color: #f1f1f1;
}

.selected-items {
  display: block;
  flex-wrap: wrap;
  /* gap: 8px; */
  margin: 8px;
}

.selected-item {
  background-color: #e0e0e0;
  padding: 4px 8px;
  margin: 8px;
  display: block;
  /* width: 10px; */
}

.remove {
  margin-left: 6px;
  cursor: pointer;
  color: red;
}
</style>
