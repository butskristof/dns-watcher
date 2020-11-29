import {Domain} from '../../entities/domains/domain';

export class CreateOrUpdateDomainData {
  domainName?: string;

  id?: string;
  modifiedOn?: Date;

  constructor(domain?: Domain) {
    if (domain) {
      Object.assign(this, domain);
    }
  }
}
