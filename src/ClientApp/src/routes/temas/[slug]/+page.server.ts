import { API_URL } from "$lib/constants";
import { fail, redirect, type Actions } from "@sveltejs/kit";
import type { PageServerLoad } from "./$types";
import type { ITopic } from "$lib/ITopic";
import AuthService from "$lib/AuthService";

export const load: PageServerLoad = async ({ cookies, locals, params }) => {
	AuthService.redirectNotLoggedUsers(locals);

	const topicId = params.slug;

	const response = await fetch(`${API_URL}/topics/${topicId}`, {
		method: "GET",
		headers: {
			Authorization: `Bearer ${cookies.get("session_id")}`,
		},
	});

	return {
		topic: (await response.json()) as ITopic,
	};
};

export const actions: Actions = {
	delete: async ({ params, cookies }) => {
		const topicId = params.slug;

		const response = await fetch(`${API_URL}/topics/${topicId}`, {
			method: "DELETE",
			headers: {
				Authorization: `Bearer ${cookies.get("session_id")}`,
			},
		});

		if (!response.ok) {
			return fail(403, { message: "No tienes permisos para eliminar este tema" });
		}

		throw redirect(303, "/perfil");
	},
};
