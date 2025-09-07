<template>
  <div class="dashboard">
    <ContainerTitle title="Pokoje" icon="🛏️" @click="goToRooms" />
    <ContainerTitle title="Ustawienia" icon="⚙️" @click="goToSettings" />
  </div>
</template>

<script setup lang="ts">
    import ContainerTitle from '@/components/ContainerTitle.vue'
   import { useRouter } from 'vue-router';

  const router = useRouter();
 
  function goToRooms() {
   router.push('/rooms') 
  }

  function goToSettings() {
   router.push('/settings')
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
