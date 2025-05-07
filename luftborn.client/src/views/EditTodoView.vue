<template>
  <div class="max-w-3xl p-4 mx-auto">
    <div class="mb-4">
      <router-link 
        :to="{ name: 'todoDetail', params: { id: route.params.id } }" 
        class="flex items-center text-blue-600 hover:underline"
      >
        <span class="mr-1">←</span> Back to Todo Details
      </router-link>
    </div>
    
    <div v-if="loading" class="p-4 text-center">
      <p class="text-xl">Loading...</p>
    </div>
    
    <div v-else-if="error" class="p-4 text-red-600 text-center">
      <p class="text-xl">{{ error }}</p>
      <button 
        @click="loadTodo" 
        class="px-4 py-2 mt-4 text-white bg-blue-600 rounded hover:bg-blue-700"
      >
        Retry
      </button>
    </div>
    
    <div v-else class="p-6 border rounded shadow bg-white">
      <h1 class="mb-6 text-2xl font-bold">Edit Todo</h1>
      
      <form @submit.prevent="saveTodo">
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
          <router-link 
            :to="{ name: 'todoDetail', params: { id: route.params.id } }" 
            class="px-4 py-2 mr-2 text-gray-600 bg-gray-200 rounded hover:bg-gray-300"
          >
            Cancel
          </router-link>
          <button 
            type="submit" 
            class="px-4 py-2 text-white bg-blue-600 rounded hover:bg-blue-700"
            :disabled="saving"
          >
            {{ saving ? 'Saving...' : 'Save Changes' }}
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import TodoService from '../services/TodoService';

const router = useRouter();
const route = useRoute();
const form = reactive({
  title: '',
  description: '',
  completed: false,
  priority: 1
});
const loading = ref(true);
const saving = ref(false);
const error = ref(null);

onMounted(() => {
  loadTodo();
});

async function loadTodo() {
  loading.value = true;
  error.value = null;
  
  try {
    const todo = await TodoService.getTodoById(route.params.id);
    form.title = todo.title;
    form.description = todo.description;
    form.completed = todo.completed;
    form.priority = todo.priority;
  } catch (err) {
    error.value = 'Failed to load todo. Please try again.';
    console.error(err);
  } finally {
    loading.value = false;
  }
}

async function saveTodo() {
  saving.value = true;
  
  try {
    const todoData = {
      ...form,
      priority: Number(form.priority)
    };
    await TodoService.updateTodo(route.params.id, todoData);
    router.push({ 
      name: 'todoDetail', 
      params: { id: route.params.id } 
    });
  } catch (err) {
    error.value = 'Failed to update todo. Please try again.';
    console.error(err);
  } finally {
    saving.value = false;
  }
}
</script> 
