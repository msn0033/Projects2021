import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Task } from 'src/app/Interface/task';
import { TaskService } from 'src/app/service/task.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.scss']
})
export class TaskComponent implements OnInit {

  TaskId;
  task :Task;

  constructor(private taskSer: TaskService, private router: Router, private param: ActivatedRoute, private title: Title) {
  


  }
  ngOnInit(): void {
    this.TaskId = this.param.snapshot.paramMap.get('id');
    this.task = this.taskSer.tasks[this.TaskId];
    this.title.setTitle((this.task.Title ? this.task.Title : '___') + '   Edit Task');

  }
  editTask() {
    setTimeout(() => {
      this.taskSer.editTask(this.TaskId, this.task);
    }, 20000);
    this.router.navigate(['/'])
  }

  DeleteTask() {
    this.taskSer.deletetask(this.TaskId);
    this.router.navigate(['/'])
  }
}
