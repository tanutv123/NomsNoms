export interface DecodedToken {
  email: string;
  unique_name: string;
  nameid: string;
  role: string | string[];
  nbf: number;
  exp: number;
  iat: number;
  iss: string;
  aud: string;
}
