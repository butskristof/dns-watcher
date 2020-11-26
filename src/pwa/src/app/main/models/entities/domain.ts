import {Record} from './record';

export class Domain {
  id?: string;
  modifiedOn?: Date;

  domainName?: string;

  watchedRecords: Record[] = [];

  constructor(domain: Domain | null = null) {
    if (domain != null) {
      Object.assign(this, domain);
      if (domain.watchedRecords) {
        this.watchedRecords = domain.watchedRecords.map(e => new Record(e));
      }
    }
  }
}
