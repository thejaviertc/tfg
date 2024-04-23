import type TUserRole from "./TUserRole";

interface IUser {
	name: string;
	surname: string;
	email: string;
	role: TUserRole;
}

export type { IUser };
