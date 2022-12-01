import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-parent',
  templateUrl: './parent.component.html',
  styleUrls: ['./parent.component.scss']
})
export class ParentComponent implements OnInit {

  constructor() { }

  nu;
  ngOnInit(): void {
  }

  funparent(e) {


    this.nu = e;

    console.log(this.nu)
  }

}
