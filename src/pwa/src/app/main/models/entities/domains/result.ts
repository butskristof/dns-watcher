import {DnsServer} from '../servers/dns-server';

export class Result {
  modifiedOn?: Date;

  value?: string;
  timeToLive?: number;
  dnsServer?: DnsServer;

  constructor(result: Result | null = null) {
    if (result) {
      Object.assign(this, result);

      if (result.dnsServer) {
        this.dnsServer = new DnsServer(result.dnsServer);
      }
    }
  }

  get prettyServer(): string {
    return this.dnsServer == null
      ? ''
      : `${this.dnsServer.name} (${this.dnsServer.ipAddress}:${this.dnsServer.port})`;
  }
}
