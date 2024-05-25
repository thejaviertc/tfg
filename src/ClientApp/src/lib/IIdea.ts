import type TStatus from "./TStatus";

interface IIdea {
	ideaId: number;
	title: string;
	shortDescription: string;
	description: string;
	createdAt: Date;
	status: TStatus;
	userId: number;
}

export type { IIdea };

