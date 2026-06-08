export interface Role {
  roleId: number;
  roleName: string;
}
export interface User {
  userId?: number;
  email: string;
  password?: string;
  roleId: number;
  role?: Role;
}
