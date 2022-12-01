import { Component, Input, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { TaskService } from 'src/app/service/task.service';


@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.scss']
})
export class AddTaskComponent implements OnInit {


  constructor(private taskSer: TaskService, private router: Router, private title: Title) {
    this.title.setTitle('add new task')
  }
  ngOnInit(): void {

  }
  saveTask(ww, Des) {
    this.taskSer.saveTask(ww.value, Des.value);
    this.router.navigate(['/']);
  }
}
