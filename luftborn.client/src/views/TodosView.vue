<template>
  <div class="max-w-3xl p-4 mx-auto">
    <h1 class="mb-6 text-3xl font-bold text-center">Todo List</h1>
    
    <div v-if="loading" class="p-4 text-center">
      <p class="text-xl">Loading...</p>
    </div>
    
    <div v-else-if="error" class="p-4 text-red-600 text-center">
      <p class="text-xl">{{ error }}</p>
      <button 
        @click="loadTodos" 
        class="px-4 py-2 mt-4 text-white bg-blue-600 rounded hover:bg-blue-700"
      >
        Retry
      </button>
    </div>
    
    <div v-else>
      <TodoForm 
        :editTodo="editingTodo" 
        @add="addTodo" 
        @update="updateTodo" 
        @cancel="cancelEdit" 
      />
      
      <div v-if="todos.length === 0" class="p-4 text-center">
        <p class="text-xl text-gray-500">No todos yet. Add one above!</p>
      </div>
      
      <div v-else class="mt-6">
        <h2 class="mb-4 text-xl font-semibold">Your Todos</h2>
        <TodoItem 
          v-for="todo in todos" 
          :key="todo.id" 
          :todo="todo"
          @update="updateTodo"
          @edit="startEdit"
          @delete="deleteTodo"
        />
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import TodoService from '../services/TodoService';
import TodoItem from '../components/TodoItem.vue';
import TodoForm from '../components/TodoForm.vue';

const router = useRouter();
const todos = ref([]);
const loading = ref(true);
const error = ref(null);
const editingTodo = ref(null);

onMounted(() => {
  loadTodos();
});

async function loadTodos() {
  loading.value = true;
  error.value = null;
  
  try {
    todos.value = await TodoService.getAllTodos();
  } catch (err) {
    error.value = 'Failed to load todos. Please try again.';
    console.error(err);
  } finally {
    loading.value = false;
  }
}

async function addTodo(todo) {
  try {
    const newTodo = await TodoService.createTodo(todo);
    todos.value.push(newTodo);
  } catch (err) {
    error.value = 'Failed to add todo. Please try again.';
    console.error(err);
  }
}

async function updateTodo(todo) {
  try {
    const updatedTodo = await TodoService.updateTodo(todo.id, todo);
    const index = todos.value.findIndex(t => t.id === todo.id);
    if (index !== -1) {
      todos.value.splice(index, 1, updatedTodo);
    }
    
    if (editingTodo.value && editingTodo.value.id === todo.id) {
      editingTodo.value = null;
    }
  } catch (err) {
    error.value = 'Failed to update todo. Please try again.';
    console.error(err);
  }
}

async function deleteTodo(id) {
  try {
    await TodoService.deleteTodo(id);
    todos.value = todos.value.filter(todo => todo.id !== id);
  } catch (err) {
    error.value = 'Failed to delete todo. Please try again.';
    console.error(err);
  }
}

function startEdit(todo) {
  router.push({ 
    name: 'editTodo', 
    params: { id: todo.id } 
  });
}

function cancelEdit() {
  editingTodo.value = null;
}
</script> 