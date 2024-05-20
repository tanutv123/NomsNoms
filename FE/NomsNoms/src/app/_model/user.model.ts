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
  tasteProfileId: number;
  isMealPlanRegistered: boolean;
  roles: string;
}
