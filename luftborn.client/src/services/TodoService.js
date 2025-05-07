import axios from 'axios';
import Todo from '../models/Todo';

const apiClient = axios.create({
  baseURL: '/api',
  headers: {
    'Content-Type': 'application/json'
  }
});

export default {
  async getAllTodos() {
    try {
      const response = await apiClient.get('/todos');
      if (Array.isArray(response.data)) {
        return response.data.map(todo => Todo.fromApi(todo));
      } else {
        console.warn('API response is not an array:', response.data);
        return [];
      }
    } catch (error) {
      console.error('Error fetching todos:', error);
      throw error;
    }
  },

  async getTodoById(id) {
    try {
      const response = await apiClient.get(`/todos/${id}`);
      return Todo.fromApi(response.data);
    } catch (error) {
      console.error(`Error fetching todo ${id}:`, error);
      throw error;
    }
  },

  async createTodo(todo) {
    try {
      const todoToSend = todo instanceof Todo ? todo.toApiPayload() : todo;
      const apiPayload = {
        ...todoToSend,
        isCompleted: todoToSend.completed,
      };
      
      const response = await apiClient.post('/todos', apiPayload);
      return Todo.fromApi(response.data);
    } catch (error) {
      console.error('Error creating todo:', error);
      throw error;
    }
  },

  async updateTodo(id, todo) {
    try {
      const todoToSend = todo instanceof Todo ? todo.toApiPayload() : todo;
      const apiPayload = {
        ...todoToSend,
        isCompleted: todoToSend.completed,
      };
      
      const response = await apiClient.put(`/todos/${id}`, apiPayload);
      return Todo.fromApi(response.data);
    } catch (error) {
      console.error(`Error updating todo ${id}:`, error);
      throw error;
    }
  },

  async deleteTodo(id) {
    try {
      await apiClient.delete(`/todos/${id}`);
      return true;
    } catch (error) {
      console.error(`Error deleting todo ${id}:`, error);
      throw error;
    }
  }
}; 
