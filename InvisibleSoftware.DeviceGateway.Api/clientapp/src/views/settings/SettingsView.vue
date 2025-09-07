<template>
    <div class="d-flex flex-wrap gap-3">
        <ContainerTitle 
        :title="$t('system.settings.user.title')" 
        icon="⚙️"
        @click="goToUserManagement"
        />    
        <ContainerTitle 
        :title="$t('system.settings.device.title')" 
        icon="⚙️"
        @click="goToDeviceManagement"
        />
        
    </div>
</template>
<script setup lang="ts">
import { useRouter } from 'vue-router';
import ContainerTitle from '@/components/ContainerTitle.vue';

const router = useRouter();
function goToUserManagement() {
  router.push('/editUser')
}
function goToDeviceManagement() {
  router.push('/addEditDevice')
}
</script>
