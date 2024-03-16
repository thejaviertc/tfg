import { API_URL } from "$lib/constants";
import { fail } from "@sveltejs/kit";
import type { Actions } from "./$types";

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
};
