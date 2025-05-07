<template>
  <div class="p-6 mb-6 border rounded shadow bg-white">
    <h2 class="mb-4 text-xl font-semibold">{{ isEditing ? 'Edit Todo' : 'Add New Todo' }}</h2>
    
    <form @submit.prevent="submitForm">
      <div class="mb-4">
        <label for="title" class="block mb-1 font-medium">Title</label>
        <input 
          id="title" 
          v-model="form.title" 
          type="text" 
          required
          class="w-full p-2 border rounded"
          placeholder="Enter todo title"
        />
      </div>
      
      <div class="mb-4">
        <label for="description" class="block mb-1 font-medium">Description</label>
        <textarea 
          id="description" 
          v-model="form.description" 
          class="w-full p-2 border rounded"
          placeholder="Enter description (optional)"
          rows="3"
        ></textarea>
      </div>
      
      <div class="mb-4">
        <label for="priority" class="block mb-1 font-medium">Priority (1-5)</label>
        <select 
          id="priority" 
          v-model="form.priority" 
          class="w-full p-2 border rounded"
          required
        >
          <option value="1">1 - Low</option>
          <option value="2">2</option>
          <option value="3">3 - Medium</option>
          <option value="4">4</option>
          <option value="5">5 - High</option>
        </select>
      </div>
      
      <div class="mb-6">
        <label class="flex items-center">
          <input 
            type="checkbox" 
            v-model="form.completed" 
            class="w-5 h-5 mr-2"
          />
          <span>Completed</span>
        </label>
      </div>
      
      <div class="flex justify-end">
        <button 
          v-if="isEditing"
          type="button"
          @click="cancelEdit"
          class="px-4 py-2 mr-2 text-gray-600 bg-gray-200 rounded hover:bg-gray-300"
        >
          Cancel
        </button>
        <button 
          type="submit" 
          class="px-4 py-2 text-white bg-blue-600 rounded hover:bg-blue-700"
        >
          {{ isEditing ? 'Update Todo' : 'Add Todo' }}
        </button>
      </div>
    </form>
  </div>
</template>

<script setup>
import { reactive, computed, watch } from 'vue';

const props = defineProps({
  editTodo: {
    type: Object,
    default: null
  }
});

const emit = defineEmits(['add', 'update', 'cancel']);

const form = reactive({
  title: '',
  description: '',
  completed: false,
  priority: 1
});

const isEditing = computed(() => {
  return props.editTodo !== null;
});

watch(() => props.editTodo, (newVal) => {
  if (newVal) {
    Object.assign(form, { ...newVal });
  } else {
    resetForm();
  }
}, { immediate: true });

function submitForm() {
  const formData = { 
    ...form,
    priority: Number(form.priority)
  };
  
  if (isEditing.value) {
    emit('update', formData);
  } else {
    emit('add', formData);
    resetForm();
  }
}

function cancelEdit() {
  emit('cancel');
}

function resetForm() {
  form.title = '';
  form.description = '';
  form.completed = false;
  form.priority = 1;
}
</script> 