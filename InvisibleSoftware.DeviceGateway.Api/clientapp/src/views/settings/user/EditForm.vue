<template>
<div class="mt-4">
  <h2>{{ $t("system.user.edit") }}</h2>
  <h3>{{ $t("system.user.add.role") }}</h3>
<div class="row">
  <div class="row-sm" style="margin-top: 10px; min-width: none; max-width: 400px;">
    <input
      type="text"
      class="form-control"
      v-model="role.name"
      :placeholder="$t('name')"
    />
  </div>

  <div class="row-sm" style="margin-top: 10px; min-width: none; max-width: 400px;">
    <input
      type="text"
      class="form-control"
      v-model="role.description"
      :placeholder="$t('description')"
    />
  </div>
  </div>

  <button class="btn btn-primary" style="margin-top: 10px; margin-left: 10px;" @click="addRole">
    {{ $t('save') }}
  </button>
</div>

  <div>   
    <h3>{{ $t("system.user.assign.user.role.title") }}</h3>

    <FlexiFormSelectLookup
      label="system.user.assign.user.role.user"
      id="user-select"
      url="/Auth/lookup-user"
      v-model="selectedUserId"
      :enableSearch="true"
      :multiselect="false"
      :required="true"
    />

    <FlexiFormSelectLookup
      label="system.user.assign.user.role.role"
      id="role-multiselect"
      url="/Auth/lookup-role"
      v-model="selectedRoleIds"
      :enableSearch="true"
      :multiselect="true"
      :required="false"
    />
    <button  class="btn btn-primary" style="margin-top: 10px; margin-left: 10px;" @click="assignRolesToUser">{{ $t('assign') }}</button>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue';
import api from '@/services/api';
import FlexiFormSelectLookup from '@/components/commons/FlexiFormSelectLookup.vue';

interface Role {
  id: number;  
  name: string;
  description: string;
}

interface User {
  id: number;
  email: string;
  name: string;
}

const role = reactive<Role>({
  id: 0,
  name: '',
  description: ''
});

const users = ref<User[]>([]);
const roles = ref<Role[]>([]);
const selectedUserId = ref<number | null>(null);
const selectedRoleIds = ref<number[]>([]);

onMounted(async () => {
  try {
    const usersResponse = await api.get('/Auth/lookup-user');
    users.value = usersResponse.data;

    const rolesResponse = await api.get('/Auth/lookup-role');
    roles.value = rolesResponse.data;
  } catch (error) {
    console.error("Błąd podczas pobierania danych:", error);
  }
});

const addRole = async () => {
  try {
    await api.post('/Auth/add-role', {
      role: {
        name: role.name,
        description: role.description
      }
    });
    alert('Rola dodana pomyślnie');

    const rolesResponse = await api.get('/Auth/lookup-role');
    roles.value = rolesResponse.data;
  } catch (error) {
    console.error("Błąd podczas dodawania roli:", error);
    alert('Błąd podczas dodawania roli');
  }
};

const assignRolesToUser = async () => {
  if (selectedUserId.value === null || selectedRoleIds.value.length === 0) {
    alert('Proszę wybrać użytkownika i co najmniej jedną rolę');
    return;
  }

  try {
    await api.post('/Auth/user-role-management', {
        userId: selectedUserId.value,
        roleIds: selectedRoleIds.value     
    });
    alert('Role przypisane pomyślnie');
  } catch (error) {
    console.error("Błąd podczas przypisywania ról:", error);
    alert('Błąd podczas przypisywania ról');
  }
};
</script>
