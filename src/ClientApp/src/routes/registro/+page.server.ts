import { API_URL } from "$lib/constants";
import { fail, redirect } from "@sveltejs/kit";
import type { Actions, PageServerLoad } from "./$types";

export const load: PageServerLoad = async ({ locals }) => {
	if (locals.user) {
		redirect(302, "/");
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
};
