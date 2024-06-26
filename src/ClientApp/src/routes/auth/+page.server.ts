import { API_URL } from "$lib/constants";
import { fail, redirect } from "@sveltejs/kit";
import type { Actions, PageServerLoad } from "./$types";

/**
 * Prevents logged users to log again
 */
export const load: PageServerLoad = async ({ locals }) => {
	if (locals.user) {
		throw redirect(302, "/");
	}
};

export const actions: Actions = {
	register: async ({ request }) => {
		const response = await fetch(`${API_URL}/auth/register`, {
			method: "POST",
			body: await request.formData(),
		});

		if (!response.ok) {
			const data = await response.json();

			return fail(400, { message: data.message });
		}

		return { success: true };
	},
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
			maxAge: 60 * 60 * 24 * 7,
		});

		throw redirect(303, "/");
	},
};
