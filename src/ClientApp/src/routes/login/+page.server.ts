import { API_URL } from "$lib/constants";
import { fail } from "@sveltejs/kit";
import type { Actions } from "./$types";

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

		return { success: true };
	},
};
