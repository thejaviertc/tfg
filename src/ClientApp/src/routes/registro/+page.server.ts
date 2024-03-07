import { fail } from "@sveltejs/kit";
import type { Actions } from "./$types";

export const actions = {
	register: async ({ request }) => {
		const response = await fetch("http://localhost:5173/api/auth/register", {
			method: "POST",
			body: await request.formData(),
		});

		if (!response.ok) {
			const data = await response.json();

			return fail(400, { message: data.message });
		}

		return { success: true };
	},
} satisfies Actions;
