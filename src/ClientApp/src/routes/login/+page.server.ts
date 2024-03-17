import { API_URL } from "$lib/constants";
import { fail, redirect } from "@sveltejs/kit";
import type { Actions, PageServerLoad } from "./$types";

export const load: PageServerLoad = async ({ locals }) => {
	if (locals.user) {
		throw redirect(302, "/");
	}
};

export const actions: Actions = {
	login: async ({ request, cookies }) => {
		const response = await fetch(`${API_URL}/auth/login`, {
			method: "POST",
			body: await request.formData(),
		});

		const data = await response.json();

		if (!response.ok) {
			return fail(400, { message: data.message });
		}

		cookies.set("session_id", data.sessionId, {
			path: "/",
			httpOnly: true,
			sameSite: "strict",
			maxAge: 60 * 120,
		});

		throw redirect(303, "/perfil");
	},
};
