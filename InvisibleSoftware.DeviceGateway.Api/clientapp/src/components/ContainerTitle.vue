<template>
  <div class="tile" @click="$emit('click')">
    <div class="icon">{{ icon }}</div>
    <div class="title">{{ title }}</div>
  </div>
</template>

<script setup lang="ts">
defineProps<{
  title: string
  icon: string
}>()
</script>

<style scoped>
.tile {
  width: 150px;
  height: 150px;
  background-color: #f0f0f0;
  border-radius: 12px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  cursor: pointer;
  transition: 0.3s;
}
.tile:hover {
  background-color: #e0e0e0;
}
.icon {
  font-size: 40px;
}
.title {
  margin-top: 10px;
  font-weight: bold;
}
</style>
