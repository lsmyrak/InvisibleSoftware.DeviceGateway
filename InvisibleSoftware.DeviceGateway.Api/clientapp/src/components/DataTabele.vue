<template>
  <table class="datatable">
    <thead>
      <tr>
        <th v-for="col in columns" :key="col.key">
          {{ col.label }}
        </th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="row in data" :key="row[idKey]">
        <td v-for="col in columns" :key="col.key">
          {{ row[col.key] }}
        </td>
      </tr>
    </tbody>
  </table>
</template>

<script setup lang="ts">
import { defineProps } from 'vue'

interface Column {
  key: string
  label: string
}

const props = defineProps<{
  columns: Column[]
  data: Record<string, any>[]
  idKey?: string
}>()

const idKey = props.idKey ?? 'id'
</script>

<style scoped>
.datatable {
  width: 100%;
  border-collapse: collapse;
}
.datatable th,
.datatable td {
  border: 1px solid #ccc;
  padding: 8px;
}
</style>
