import { Injectable } from '@angular/core';
import { Task } from '../Interface/task';


@Injectable({
  providedIn: 'root'
})
export class TaskService {

  tasks: Array<Task> = []
  task: Task

  constructor() { }

  deletetask(i) {
    this.tasks.splice(i, 1)
  }
  saveTask(title, Des) {
    this.tasks.push({ Title: title, Descrption: Des });
  }
  editTask(i, data) {
    this.tasks[i] = data;
  }
}
