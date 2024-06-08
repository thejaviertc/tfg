import type { IUser } from "./IUser";
import type TStatus from "./TStatus";

interface ITopic {
	topicId: number;
	title: string;
	shortDescription: string;
	description: string;
	createdAt: Date;
	status: TStatus;
	user: IUser;
}

export type { ITopic };
