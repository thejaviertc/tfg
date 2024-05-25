import type TStatus from "./TStatus";

interface ITopic {
	topicId: number;
	title: string;
	shortDescription: string;
	description: string;
	createdAt: Date;
	status: TStatus;
	userId: number;
}

export type { ITopic };
