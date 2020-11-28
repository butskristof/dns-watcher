import {Component, Input, OnInit} from '@angular/core';
import {Record} from '../../../models/entities/domains/record';

@Component({
  selector: 'app-domain-propagation',
  templateUrl: './domain-propagation.component.html',
  styleUrls: ['./domain-propagation.component.scss']
})
export class DomainPropagationComponent implements OnInit {
  @Input()
  record?: Record;

  constructor() { }

  ngOnInit(): void {
  }

  get propagation(): number {
    return 0;
  }

  getPropagation(): string {
    return `${this.propagation} %`;
  }

  getClass(): string {
    const propagation = this.propagation;
    if (propagation >= 70) {
      return 'success';
    } else if (propagation < 70 && propagation >= 40) {
      return 'warning';
    } else {
      return 'danger';
    }
  }
}
