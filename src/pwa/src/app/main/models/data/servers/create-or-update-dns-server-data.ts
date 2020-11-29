import {DnsServer} from '../../entities/servers/dns-server';

export class CreateOrUpdateDnsServerData {
  name?: string;
  ipAddress?: string;
  port = 53;

  id?: string;
  modifiedOn?: Date;

  constructor(server?: DnsServer) {
    if (server) {
      Object.assign(this, server);
    }
  }
}
