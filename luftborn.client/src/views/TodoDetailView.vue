<template>
  <div class="max-w-3xl p-4 mx-auto">
    <div class="mb-4">
      <router-link 
        to="/" 
        class="flex items-center text-blue-600 hover:underline"
      >
        <span class="mr-1">←</span> Back to Todo List
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
    
    <div v-else-if="todo" class="p-6 border rounded shadow bg-white">
      <div class="flex items-center justify-between mb-4">
        <h1 class="text-2xl font-bold">{{ todo.title }}</h1>
        <div class="flex items-center">
          <span 
            class="px-2 py-1 text-sm rounded mr-2" 
            :class="priorityClass"
          >
            Priority: {{ todo.priority }}
          </span>
          <span 
            class="px-2 py-1 text-sm rounded" 
            :class="todo.completed ? 'bg-green-100 text-green-800' : 'bg-yellow-100 text-yellow-800'"
          >
            {{ todo.completed ? 'Completed' : 'Pending' }}
          </span>
        </div>
      </div>
      
      <div class="mb-6">
        <h2 class="text-lg font-semibold mb-2">Description</h2>
        <p class="text-gray-700">{{ todo.description || 'No description provided.' }}</p>
      </div>
      
      <div class="flex justify-end">
        <button 
          @click="toggleComplete" 
          class="px-4 py-2 mr-2 text-white rounded"
          :class="todo.completed ? 'bg-yellow-600 hover:bg-yellow-700' : 'bg-green-600 hover:bg-green-700'"
        >
          Mark as {{ todo.completed ? 'Pending' : 'Completed' }}
        </button>
        <button 
          @click="editTodo" 
          class="px-4 py-2 mr-2 text-white bg-blue-600 rounded hover:bg-blue-700"
        >
          Edit
        </button>
        <button 
          @click="deleteTodo" 
          class="px-4 py-2 text-white bg-red-600 rounded hover:bg-red-700"
        >
          Delete
        </button>
      </div>
    </div>
    
    <div v-else class="p-4 text-center">
      <p class="text-xl text-gray-500">Todo not found.</p>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import TodoService from '../services/TodoService';

const router = useRouter();
const route = useRoute();
const todo = ref(null);
const loading = ref(true);
const error = ref(null);

const priorityClass = computed(() => {
  if (!todo.value) return '';
  
  switch (todo.value.priority) {
    case 1:
      return 'bg-gray-100 text-gray-800';
    case 2:
      return 'bg-blue-100 text-blue-800';
    case 3:
      return 'bg-yellow-100 text-yellow-800';
    case 4:
      return 'bg-orange-100 text-orange-800';
    case 5:
      return 'bg-red-100 text-red-800';
    default:
      return 'bg-gray-100 text-gray-800';
  }
});

onMounted(() => {
  loadTodo();
});

async function loadTodo() {
  loading.value = true;
  error.value = null;
  
  try {
    todo.value = await TodoService.getTodoById(route.params.id);
  } catch (err) {
    error.value = 'Failed to load todo. Please try again.';
    console.error(err);
  } finally {
    loading.value = false;
  }
}

async function toggleComplete() {
  try {
    const updatedTodo = { ...todo.value, completed: !todo.value.completed };
    todo.value = await TodoService.updateTodo(todo.value.id, updatedTodo);
  } catch (err) {
    error.value = 'Failed to update todo. Please try again.';
    console.error(err);
  }
}

function editTodo() {
  router.push({ name: 'editTodo', params: { id: todo.value.id } });
}

async function deleteTodo() {
  if (!confirm('Are you sure you want to delete this todo?')) {
    return;
  }
  
  try {
    await TodoService.deleteTodo(todo.value.id);
    router.push('/');
  } catch (err) {
    error.value = 'Failed to delete todo. Please try again.';
    console.error(err);
  }
}
</script> 