import AuthService from "$lib/AuthService";
import type { IIdea } from "$lib/IIdea";
import { API_URL } from "$lib/constants";
import { fail, redirect, type Actions } from "@sveltejs/kit";
import type { PageServerLoad } from "../$types";

export const load: PageServerLoad = async ({ cookies, locals, params }) => {
	AuthService.redirectNotLoggedUsers(locals);
	AuthService.redirectNotStudents(locals);

	const ideaId = params.slug;

	const response = await fetch(`${API_URL}/ideas/${ideaId}`, {
		method: "GET",
		headers: {
			Authorization: `Bearer ${cookies.get("session_id")}`,
		},
	});

	if (!response.ok) {
		throw redirect(302, "/ideas");
	}

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
