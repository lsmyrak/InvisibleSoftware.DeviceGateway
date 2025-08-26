<template>
    <div class="flexi-form-select-lookup">
        <label :for="id" class="form-label">{{ $t(label) }}</label>
        <input v-model="searchQuery"
               type="text"
               class="form-control mb-2"
               @focus="showDropdown = true"
               @blur="hideDropdown" />

        <div v-if="showDropdown && filteredData.length > 0" class="dropdown-menu show">
            <ul class="list-group">
                <li v-for="item in filteredData"
                    :key="getValue(item, { key: 'id' })"
                    class="list-group-item"
                    @click="selectItem(item)">
                    <slot :item="item" :columns="columns">
                        {{ getDisplayValue(item) }}
                    </slot>
                </li>
            </ul>
        </div>
    </div>
</template>

<script setup>
    import { ref, computed } from "vue";

    const props = defineProps({
        label: { type: String, required: true },
        id: { type: String, required: true },
        name: { type: String, default: "" },
        lookup: {
            type: Object,
            required: true,
            validator(value) {
                return Array.isArray(value.columns) && Array.isArray(value.data);
            },
        },
        modelValue: [String, Number],
        enableSearch: { type: Boolean, default: true },
        className: { type: String, default: "" },
    });

    const emit = defineEmits(["update:modelValue"]);

    const searchQuery = ref("");
    const showDropdown = ref(false);

    const columns = computed(() => props.lookup.columns);

    // 🔧 funkcja która obsłuży różne warianty klucza
    function getValue(item, column) {
        if (!column || !column.key) return null;
        return item[column.key] ?? item[column.key.toLowerCase()];
    }

    const filteredData = computed(() => {
        if (!props.enableSearch || !searchQuery.value) {
            return props.lookup.data;
        }

        return props.lookup.data.filter((item) =>
            columns.value.some((column) =>
                String(getValue(item, column) ?? "")
                    .toLowerCase()
                    .includes(searchQuery.value.toLowerCase())
            )
        );
    });

    function getDisplayValue(item) {
        const displayColumn = columns.value.find((col) => col.display);
        return displayColumn ? getValue(item, displayColumn) : getValue(item, { key: "id" });
    }

    function selectItem(item) {
        emit("update:modelValue", getValue(item, { key: "id" }));
        searchQuery.value = getDisplayValue(item);
        showDropdown.value = false;
    }

    function hideDropdown() {
        setTimeout(() => {
            showDropdown.value = false;
        }, 200);
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
        background-color: white;
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
</style>
