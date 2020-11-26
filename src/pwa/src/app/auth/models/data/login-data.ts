export class LoginData {
  username: string | null = null;
  password: string | null = null;

  constructor(username: string | null, password: string | null) {
    this.username = username;
    this.password = password;
  }
}
