import { Placeholder } from '@angular/compiler/src/i18n/i18n_ast';
import { Component, OnInit } from '@angular/core';
import { PlaceholderService } from 'src/app/service/placeholder.service';

@Component({
  selector: 'app-test-api',
  templateUrl: './test-api.component.html',
  styleUrls: ['./test-api.component.scss']
})
export class TestApiComponent implements OnInit {

  constructor(public service: PlaceholderService) { }

  ngOnInit(): void {
    this.service.getdata()

  }
}
