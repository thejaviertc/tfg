import type { ITopic } from "$lib/ITopic";
import type { IUser } from "$lib/IUser";

declare global {
	namespace App {
		interface Locals {
			user: IUser;
			topics: ITopic;
		}
	}
}

export {};
