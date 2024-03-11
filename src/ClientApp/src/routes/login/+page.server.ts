import { fail } from "@sveltejs/kit";
import type { Actions } from "./$types";

export const actions = {
	login: async ({ request, cookies }) => {
		const response = await fetch("http://localhost:5173/api/auth/login", {
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
} satisfies Actions;
