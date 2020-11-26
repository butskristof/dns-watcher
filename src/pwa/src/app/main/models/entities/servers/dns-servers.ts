import {DnsServer} from './dns-server';

export class DnsServers {
  dnsServers: DnsServer[] = [];

  constructor(servers: DnsServers | null = null) {
    if (servers) {
      Object.assign(this, servers);

      if (servers.dnsServers) {
        this.dnsServers = servers.dnsServers.map(e => new DnsServer(e));
      }
    }
  }
}
