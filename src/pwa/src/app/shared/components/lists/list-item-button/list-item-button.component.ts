import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-list-item-button',
  templateUrl: './list-item-button.component.html',
  styleUrls: ['./list-item-button.component.scss']
})
export class ListItemButtonComponent
  implements OnInit
{
  @Input()
  icon = '';
  @Input()
  buttonStyle = 'default';

  constructor() { }

  ngOnInit(): void {
  }

  // region getters
  get buttonClassList(): string {
    return `button button-outline ${this.buttonStyle}`;
  }
  // endregion
}
