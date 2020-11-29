import { Injectable } from '@angular/core';
import {TokenInfo} from '../models/entities/token-info';

const credentialsKey = 'credentials';

@Injectable({
  providedIn: 'root'
})
export class CredentialsService {
  constructor() {}

  isAuthenticated(): boolean {
    return !!this.credentials;
  }

  get credentials(): TokenInfo | null {
    const savedCredentials =
      this.getWithExpiry(sessionStorage, credentialsKey)
      || this.getWithExpiry(localStorage, credentialsKey);
    return savedCredentials ? JSON.parse(savedCredentials) : null;
  }

  get token(): string | null {
    return this.credentials?.accessToken ?? null;
  }

  setCredentials(credentials?: TokenInfo, remember?: boolean): void {
    if (credentials) {
      const storage = remember ? localStorage : sessionStorage;
      const ttl = new Date(credentials.accessTokenValidUntil ?? 0).getTime() - new Date().getTime();
      this.setWithExpiry(storage, credentialsKey, JSON.stringify(credentials), ttl);
    } else {
      sessionStorage.removeItem(credentialsKey);
      localStorage.removeItem(credentialsKey);
    }
  }

  private setWithExpiry(storage: Storage, key: string, value: string, ttl: number): void {
    const now = new Date();
    const item = {
      value,
      expiry: now.getTime() + ttl,
    };
    storage.setItem(key, JSON.stringify(item));
  }

  private getWithExpiry(storage: Storage, key: string): any {
    const itemStr = storage.getItem(key);
    if (!itemStr) {
      return null;
    }

    const item = JSON.parse(itemStr);
    const now = new Date();
    if (now.getTime() > item.expiry) {
      storage.removeItem(key);
      return null;
    }

    return item.value;
  }
}
