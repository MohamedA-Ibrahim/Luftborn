<template>
  <div class="flex items-center justify-between p-4 mb-2 border rounded shadow">
    <div class="flex items-center">
      <input 
        type="checkbox" 
        :checked="todo.completed" 
        @change="toggleComplete"
        class="w-5 h-5 mr-3 cursor-pointer"
      />
      <div>
        <span 
          :class="{ 'line-through text-gray-500': todo.completed }"
          class="text-lg cursor-pointer"
          @click="viewDetail"
        >
          {{ todo.title }}
        </span>
        <div class="flex items-center mt-1">
          <span class="mr-2 text-xs text-gray-500">Priority:</span>
          <span 
            class="px-2 py-0.5 text-xs rounded-full" 
            :class="priorityClass"
          >
            {{ todo.priority }}
          </span>
        </div>
      </div>
    </div>
    <div class="flex">
      <button 
        @click="$emit('edit', todo)" 
        class="px-3 py-1 mr-2 text-sm text-blue-600 bg-blue-100 rounded hover:bg-blue-200"
      >
        Edit
      </button>
      <button 
        @click="$emit('delete', todo.id)" 
        class="px-3 py-1 text-sm text-red-600 bg-red-100 rounded hover:bg-red-200"
      >
        Delete
      </button>
    </div>
  </div>
</template>

<script setup>
import { useRouter } from 'vue-router';
import { computed } from 'vue';

const props = defineProps({
  todo: {
    type: Object,
    required: true
  }
});

const emit = defineEmits(['update', 'edit', 'delete']);

const router = useRouter();

const priorityClass = computed(() => {
  switch (props.todo.priority) {
    case 1:
      return 'bg-gray-200 text-gray-800';
    case 2:
      return 'bg-blue-200 text-blue-800';
    case 3:
      return 'bg-yellow-200 text-yellow-800';
    case 4:
      return 'bg-orange-200 text-orange-800';
    case 5:
      return 'bg-red-200 text-red-800';
    default:
      return 'bg-gray-200 text-gray-800';
  }
});

function toggleComplete() {
  const updatedTodo = {
    ...props.todo,
    completed: !props.todo.completed
  };
  
  emit('update', updatedTodo);
}

function viewDetail() {
  router.push({ 
    name: 'todoDetail', 
    params: { id: props.todo.id } 
  });
}
</script> 