import AuthService from "$lib/AuthService";
import type { IIdea } from "$lib/IIdea";
import { API_URL } from "$lib/constants";
import { fail, type Actions } from "@sveltejs/kit";
import type { PageServerLoad } from "../$types";

export const load: PageServerLoad = async ({ cookies, locals, params }) => {
	// TODO: Check if is the user who created it
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
	update: async ({ request, params, cookies }) => {
		const ideaId = params.slug;

		const response = await fetch(`${API_URL}/ideas/${ideaId}`, {
			method: "PUT",
			body: await request.formData(),
			headers: {
				Authorization: `Bearer ${cookies.get("session_id")}`,
			},
		});

		if (!response.ok) {
			const data = await response.json();

			return fail(400, { message: data.message });
		}

		return { success: true };
	},
};
