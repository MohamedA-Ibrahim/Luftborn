import { createRouter, createWebHistory } from 'vue-router';
import TodosView from '../views/TodosView.vue';
import TodoDetailView from '../views/TodoDetailView.vue';
import EditTodoView from '../views/EditTodoView.vue';

const routes = [
  {
    path: '/',
    name: 'todos',
    component: TodosView
  },
  {
    path: '/todo/:id',
    name: 'todoDetail',
    component: TodoDetailView,
    props: true
  },
  {
    path: '/todo/:id/edit',
    name: 'editTodo',
    component: EditTodoView,
    props: true
  },
  {
    path: '/:catchAll(.*)',
    redirect: '/'
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router; 
