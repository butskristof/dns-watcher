import {
  Injectable, ComponentFactoryResolver, ApplicationRef, Injector,
  EmbeddedViewRef, ComponentRef, Type
} from '@angular/core';
import {DialogComponent} from '../components/dialog/dialog.component';
import {DialogModule} from '../dialog.module';
import {DialogConfig} from '../models/dialog-config';
import {DialogInjector} from '../injectors/dialog-injector';
import {DialogRef} from '../models/dialog-ref';

@Injectable({
  providedIn: 'root'
})
export class DialogService {
  dialogComponentRef?: ComponentRef<DialogComponent>;

  constructor(
    private componentFactoryResolver: ComponentFactoryResolver,
    private appRef: ApplicationRef,
    private injector: Injector
  ) {
  }

  public open(
    componentType: Type<any>,
    config: DialogConfig
  ): DialogRef {
    const ref = this.appendDialogComponentToBody(config);

    if (this.dialogComponentRef != null) {
      this.dialogComponentRef.instance.childComponentType = componentType;
    }

    return ref;
  }

  private appendDialogComponentToBody(config: DialogConfig): DialogRef {
    const map = new WeakMap();
    map.set(DialogConfig, config);

    // add dialogref to dependency injection
    const dialogRef = new DialogRef();
    map.set(DialogRef, dialogRef);

    const sub = dialogRef.afterClosed.subscribe(() => {
      // close dialog
      this.removeDialogComponentFromBody();
      sub.unsubscribe();
    });

    const factory = this.componentFactoryResolver
      .resolveComponentFactory(DialogComponent);

    const injector = new DialogInjector(this.injector, map);
    const componentRef = factory.create(injector);
    this.appRef.attachView(componentRef.hostView);

    const domElement = (componentRef.hostView as EmbeddedViewRef<any>)
      .rootNodes[0] as HTMLElement;
    document.body.appendChild(domElement);

    this.dialogComponentRef = componentRef;

    this.dialogComponentRef?.instance
      .onClose
      .subscribe(() => {
        this.removeDialogComponentFromBody();
      });

    // return ref
    return dialogRef;
  }

  private removeDialogComponentFromBody(): void {
    if (this.dialogComponentRef == null) {
      return;
    }

    this.appRef.detachView(this.dialogComponentRef.hostView);
    this.dialogComponentRef.destroy();
  }
}
