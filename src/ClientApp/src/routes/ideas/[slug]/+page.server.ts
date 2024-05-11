import AuthService from "$lib/AuthService";
import type { IIdea } from "$lib/IIdea";
import { API_URL } from "$lib/constants";
import { fail, redirect, type Actions } from "@sveltejs/kit";
import type { PageServerLoad } from "./$types";

export const load: PageServerLoad = async ({ cookies, locals, params }) => {
	AuthService.redirectNotLoggedUsers(locals);

	const ideaId = params.slug;

	const response = await fetch(`${API_URL}/ideas/${ideaId}`, {
		method: "GET",
		headers: {
			Authorization: `Bearer ${cookies.get("session_id")}`,
		},
	});

	return {
		idea: (await response.json()) as IIdea,
	};
};

export const actions: Actions = {
	delete: async ({ params, cookies }) => {
		const ideaId = params.slug;

		const response = await fetch(`${API_URL}/ideas/${ideaId}`, {
			method: "DELETE",
			headers: {
				Authorization: `Bearer ${cookies.get("session_id")}`,
			},
		});

		if (!response.ok) {
			return fail(403, { message: "No tienes permisos para eliminar esta idea" });
		}

		throw redirect(303, "/perfil");
	},
};
