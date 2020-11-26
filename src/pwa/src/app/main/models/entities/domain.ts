export class Domain {
  id?: string;
  modifiedOn?: Date;

  domainName?: string;

  constructor(domain: Domain | null = null) {
    Object.assign(this, domain);
  }
}
