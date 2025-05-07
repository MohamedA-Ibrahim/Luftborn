export default class Todo {
  constructor(data = {}) {
    this.id = data.id || null;
    this.title = data.title || '';
    this.description = data.description || '';
    this.completed = data.completed !== undefined ? data.completed : false;
    this.priority = data.priority || 1; // Default priority is 1 (lowest)
  }

  static fromApi(data) {
    return new Todo({
      id: data.id,
      title: data.title,
      description: data.description,
      completed: data.isCompleted !== undefined ? data.isCompleted : data.completed,
      priority: data.priority
    });
  }

  toApiPayload() {
    return {
      title: this.title,
      description: this.description,
      completed: this.completed,
      priority: this.priority
    };
  }
} 