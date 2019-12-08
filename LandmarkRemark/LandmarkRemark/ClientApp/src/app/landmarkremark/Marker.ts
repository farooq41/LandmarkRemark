import { User } from '../login/login.component';
export class Marker {
  id: number;
  note: string;
  longitude: number;
  latitude: number;
  user: User;
  createdDate: Date;
  userId: number;
  open: boolean;
}
