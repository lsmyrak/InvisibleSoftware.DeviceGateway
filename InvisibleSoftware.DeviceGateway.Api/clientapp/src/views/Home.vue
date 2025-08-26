<template>
  <div class="dashboard">
    <Tile title="Pokoje" icon="🛏️" @click="goToRooms" />
    <Tile title="Ustawienia" icon="⚙️" @click="goToSettings" />
  </div>
</template>

<script setup lang="ts">
import Tile from '@/components/ContainerTitle.vue'

function goToRooms() {
  console.log('Przejście do widoku Pokoi')  
}

function goToSettings() {
  console.log('Przejście do widoku Ustawień')
}
</script>

<style scoped>
.dashboard {
  display: flex;
  gap: 20px;
  padding: 40px;
  justify-content: center;
}
</style>
