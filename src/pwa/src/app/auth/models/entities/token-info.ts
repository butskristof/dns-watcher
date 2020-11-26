export class TokenInfo {
  userId = '';
  username = '';
  token = '';
  validUntil: Date = new Date();

  constructor(tokenInfo: TokenInfo | null = null) {
    if (tokenInfo) {
      Object.assign(this, tokenInfo);
    }
  }
}
