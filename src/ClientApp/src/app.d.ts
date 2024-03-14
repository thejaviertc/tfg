import type { IUser } from "$lib/IUser";

declare global {
	namespace App {
		interface Locals {
			user: IUser;
		}
	}
}

export {};
