import {Domain} from './domain';

export class Domains {
  domains: Domain[] = [];

  constructor(domains: Domains | null = null) {
    if (domains) {
      Object.assign(this, domains);
      if (domains.domains) {
        this.domains = domains.domains.map(e => new Domain(e));
      }
    }
  }
}
