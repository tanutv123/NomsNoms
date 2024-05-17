export interface User {
  name: string;
  knownAs: string;
  token: string;
  phoneNumber: string;
  email: string;
  introduction: string;
  imageUrl: string;
  lastActive: Date;
  createdDate: Date;
  status: number;
  roles: string;
}
