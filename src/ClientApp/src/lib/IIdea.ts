import type { IUser } from "./IUser";
import type TStatus from "./TStatus";

interface IIdea {
	ideaId: number;
	title: string;
	shortDescription: string;
	description: string;
	createdAt: Date;
	status: TStatus;
	user: IUser;
}

export type { IIdea };
