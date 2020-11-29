import {Injectable} from '@angular/core';
import {TokenInfo} from '../models/entities/token-info';

const accessTokenKey = 'accessToken';
const refreshTokenKey = 'refreshToken';

@Injectable({
  providedIn: 'root'
})
export class CredentialsService {
  constructor() {
  }

  isAuthenticated(): boolean {
    return !!this.credentials;
  }

  get credentials(): TokenInfo | null {
    const savedCredentials =
      this.getWithExpiry(sessionStorage, accessTokenKey)
      || this.getWithExpiry(localStorage, accessTokenKey);
    return savedCredentials ? JSON.parse(savedCredentials) : null;
  }

  private get refreshCredentials(): TokenInfo | null {
    const saved = this.getWithExpiry(sessionStorage, refreshTokenKey)
      ?? this.getWithExpiry(localStorage, refreshTokenKey);
    return saved ? JSON.parse(saved) : null;
  }

  get token(): string | null {
    return this.credentials?.accessToken ?? null;
  }

  get refreshToken(): string | null {
    return this.refreshCredentials?.refreshToken ?? null;
  }

  setCredentials(credentials?: TokenInfo, remember?: boolean): void {
    if (credentials) {
      const storage = remember ? localStorage : sessionStorage;

      const referenceTime = new Date().getTime();

      const accessTtl = new Date(credentials.accessTokenValidUntil ?? 0).getTime() - referenceTime;
      const accessValue = {
        accessToken: credentials.accessToken,
        username: credentials.username,
        userId: credentials.userId,
      };
      this.setWithExpiry(storage, accessTokenKey, JSON.stringify(accessValue), accessTtl);

      const refreshTtl = new Date(credentials.refreshTokenValidUntil ?? 0).getTime() - referenceTime;
      const refreshValue = {
        refreshToken: credentials.refreshToken
      };
      this.setWithExpiry(storage, refreshTokenKey, JSON.stringify(refreshValue), refreshTtl);
    } else {
      sessionStorage.removeItem(accessTokenKey);
      sessionStorage.removeItem(refreshTokenKey);
      localStorage.removeItem(accessTokenKey);
      localStorage.removeItem(refreshTokenKey);
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
