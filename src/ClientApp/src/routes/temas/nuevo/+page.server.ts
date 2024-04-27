import AuthService from "$lib/AuthService";
import { API_URL } from "$lib/constants";
import { fail, redirect, type Actions } from "@sveltejs/kit";
import type { PageServerLoad } from "./$types";

export const load: PageServerLoad = async ({ locals }) => {
	AuthService.redirectNotLoggedUsers(locals);
	AuthService.redirectNotTeachers(locals);
};

export const actions: Actions = {
	add: async ({ request, cookies }) => {
		const response = await fetch(`${API_URL}/topics`, {
			method: "POST",
			body: await request.formData(),
			headers: {
				Authorization: `Bearer ${cookies.get("session_id")}`,
			},
		});

		if (!response.ok) {
			const data = await response.json();

			return fail(400, { message: data.message });
		}

		throw redirect(303, "/perfil");
	},
};
