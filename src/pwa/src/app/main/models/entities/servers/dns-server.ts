export class DnsServer {
  id?: string;
  modifiedOn?: Date;

  name?: string;
  ipAddress?: string;
  port?: number;

  constructor(server: DnsServer | null = null) {
    if (server) {
      Object.assign(this, server);
    }
  }

  get pretty(): string {
    return `${this.name} (${this.ipAddress}:${this.port})`;
  }
}
