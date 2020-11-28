export class TokenInfo {
  userId?: string;
  username?: string;
  token?: string;
  validUntil?: Date;

  constructor(tokenInfo: TokenInfo | null = null) {
    if (tokenInfo) {
      Object.assign(this, tokenInfo);
    }
  }
}
