export class DnsServer {
  name?: string;
  ipAddress?: string;
  port?: number;

  constructor(server: DnsServer | null = null) {
    if (server) {
      Object.assign(this, server);
    }
  }
}
