import {Directive, HostListener, ElementRef, Input} from '@angular/core';

// https://stackoverflow.com/a/55627196

@Directive({
  selector: '[appHoverClass]'
})
export class HoverClassDirective {

  constructor(public elementRef: ElementRef) {
  }

  @Input('appHoverClass') hoverClass: any;

  @HostListener('mouseenter') onMouseEnter(): void {
    this.elementRef.nativeElement.classList.add(this.hoverClass);
  }

  @HostListener('mouseleave') onMouseLeave(): void {
    this.elementRef.nativeElement.classList.remove(this.hoverClass);
  }

}
