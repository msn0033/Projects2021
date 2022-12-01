
import { Component, OnInit } from '@angular/core';
import { fade, slide } from '../animations/animations';

@Component({
  selector: 'todo',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.scss'],
  animations: [
    fade
  ]

})
export class TodoListComponent implements OnInit {

  constructor() { }

  listArr: string[] = [
    'c#',
    'java',
    'type script',
    'angular'
  ]
  ngOnInit(): void {

  }
  addItem(itemInput) {

    this.listArr.splice(0, 0, itemInput.value);
    itemInput.value = '';
  }

  remove(item) {

    let index = this.listArr.indexOf(item);
    this.listArr.splice(index, 1);
  }

}
