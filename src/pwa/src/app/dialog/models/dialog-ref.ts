import {Observable, Subject} from 'rxjs';

export class DialogRef {
  private readonly innerAfterClosed = new Subject<any>();
  public afterClosed: Observable<any> = this.innerAfterClosed.asObservable();

  close(result?: any): void {
    this.innerAfterClosed.next(result);
  }
}
