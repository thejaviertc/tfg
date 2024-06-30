import AuthService from "$lib/AuthService";
import type { ITopic } from "$lib/ITopic";
import { API_URL } from "$lib/constants";
import { fail, redirect, type Actions } from "@sveltejs/kit";
import type { PageServerLoad } from "../$types";

export const load: PageServerLoad = async ({ cookies, locals, params }) => {
	AuthService.redirectNotLoggedUsers(locals);
	AuthService.redirectNotTeachers(locals);

	const topicId = params.slug;

	const response = await fetch(`${API_URL}/topics/${topicId}`, {
		method: "GET",
		headers: {
			Authorization: `Bearer ${cookies.get("session_id")}`,
		},
	});

	if (!response.ok) {
		throw redirect(302, "/topics");
	}

	return {
		topic: (await response.json()) as ITopic,
	};
};

export const actions: Actions = {
	update: async ({ request, params, cookies }) => {
		const topicId = params.slug;

		const response = await fetch(`${API_URL}/topics/${topicId}`, {
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
