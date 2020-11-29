import { Component, OnInit } from '@angular/core';
import {Config} from '../../../../config';

@Component({
  selector: 'app-environment-indicator',
  templateUrl: './environment-indicator.component.html',
  styleUrls: ['./environment-indicator.component.scss']
})
export class EnvironmentIndicatorComponent implements OnInit {
  env = Config.environment;

  constructor() { }

  ngOnInit(): void {
  }

}
