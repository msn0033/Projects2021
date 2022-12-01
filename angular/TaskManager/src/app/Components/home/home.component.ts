import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { Task } from 'src/app/Interface/task';
import { TaskService } from 'src/app/service/task.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private tskesSer: TaskService, private router: Router, private title: Title) {
    this.title.setTitle('Home')
  }
  tasks: Array<Task> = [];



  ngOnInit(): void {
    this.tasks = this.tskesSer.tasks;
  }

  deletetask(i) {
    if (confirm("Are you shor ?") == true)
      this.tskesSer.deletetask(i)
    this.router.navigate(['/'])
    console.log(i);
  }

}
