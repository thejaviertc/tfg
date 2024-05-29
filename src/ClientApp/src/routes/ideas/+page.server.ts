import AuthService from "$lib/AuthService";
import type { IIdea } from "$lib/IIdea";
import TUserRole from "$lib/TUserRole";
import { API_URL } from "$lib/constants";
import type { PageServerLoad } from "./$types";

export const load: PageServerLoad = async ({ cookies, locals }) => {
	AuthService.redirectNotLoggedUsers(locals);

	let response;

	if (locals.user.role === TUserRole.Alumno) {
		response = await fetch(`${API_URL}/ideas/me`, {
			method: "GET",
			headers: {
				Authorization: `Bearer ${cookies.get("session_id")}`,
			},
		});
	}

	if (locals.user.role === TUserRole.Profesor) {
		response = await fetch(`${API_URL}/ideas`, {
			method: "GET",
			headers: {
				Authorization: `Bearer ${cookies.get("session_id")}`,
			},
		});
	}

	return {
		ideas: (await response?.json()) as IIdea[],
	};
};
