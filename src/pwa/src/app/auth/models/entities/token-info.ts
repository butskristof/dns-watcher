export class TokenInfo {
  userId?: string;
  username?: string;
  accessToken?: string;
  accessTokenValidUntil?: Date;

  constructor(tokenInfo: TokenInfo | null = null) {
    if (tokenInfo) {
      Object.assign(this, tokenInfo);
    }
  }
}
