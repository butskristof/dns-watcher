import {Component, Input, OnInit} from '@angular/core';
import {ActionButtonStyle} from '../../../models/viewmodels/action-button-style';

@Component({
  selector: 'app-action-button',
  templateUrl: './action-button.component.html',
  styleUrls: ['./action-button.component.scss']
})
export class ActionButtonComponent implements OnInit {
  @Input()
  icon = '';
  @Input()
  buttonStyle = ActionButtonStyle.Default;

  constructor() { }

  ngOnInit(): void {
  }

  // region getters
  get buttonClassList(): string {
    return `button button-outline ${this.buttonStyle}`;
  }
  // endregion
}
