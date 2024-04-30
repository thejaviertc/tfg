import AuthService from "$lib/AuthService";
import type { IIdea } from "$lib/IIdea";
import { API_URL } from "$lib/constants";
import type { PageServerLoad } from "./$types";

export const load: PageServerLoad = async ({ cookies, locals }) => {
	AuthService.redirectNotLoggedUsers(locals);

	const response = await fetch(`${API_URL}/ideas`, {
		method: "GET",
		headers: {
			Authorization: `Bearer ${cookies.get("session_id")}`,
		},
	});

	return {
		ideas: (await response.json()) as IIdea[],
	};
};
