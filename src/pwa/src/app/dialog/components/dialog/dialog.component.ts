import {
  Component,
  Type,
  OnDestroy,
  AfterViewInit,
  ComponentRef,
  ViewChild,
  ComponentFactoryResolver,
  ChangeDetectorRef, HostListener
} from '@angular/core';
import {Subject} from 'rxjs';
import {InsertionDirective} from '../../directives/insertion.directive';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.scss'],
})
export class DialogComponent implements AfterViewInit, OnDestroy {
  private readonly innerOnClose = new Subject<any>();

  public componentRef?: ComponentRef<any>;
  public childComponentType?: Type<any>;
  public onClose = this.innerOnClose.asObservable();

  @ViewChild(InsertionDirective)
  insertionPoint?: InsertionDirective;

  constructor(
    private componentFactoryResolver: ComponentFactoryResolver,
    private cd: ChangeDetectorRef
  ) {
  }

  ngAfterViewInit(): void {
    if (this.childComponentType) {
      this.loadChildComponent(this.childComponentType);
    }
    this.cd.detectChanges();
  }

  ngOnDestroy(): void {
    this.componentRef?.destroy();
  }

  loadChildComponent(componentType: Type<any>): void {
    const componentFactory = this.componentFactoryResolver
      .resolveComponentFactory(componentType);

    const viewContainerRef = this.insertionPoint?.viewContainerRef;
    viewContainerRef?.clear();

    this.componentRef = viewContainerRef?.createComponent(componentFactory);
  }

  // region clicks

  onOverlayClicked(event: MouseEvent): void {
    this.close();
  }

  onDialogClicked(event: MouseEvent): void {
    event.stopPropagation();
  }

  // endregion

  // region escape
  @HostListener('document:keydown.escape', ['$event'])
  private onKeydownHandler(event: KeyboardEvent): void {
    this.close();
  }
  // endregion

  // region close
  private close(): void {
    this.innerOnClose.next(null);
  }
  // endregion
}
