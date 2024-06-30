import AuthService from "$lib/AuthService";
import type { IUser } from "$lib/IUser";
import { API_URL } from "$lib/constants";
import { fail, redirect, type Actions } from "@sveltejs/kit";
import type { PageServerLoad } from "../$types";

export const load: PageServerLoad = async ({ cookies, locals, params }) => {
	AuthService.redirectNotLoggedUsers(locals);
	AuthService.redirectNotTeachers(locals);

	const topicId = params.slug;

	const response = await fetch(`${API_URL}/topics/${topicId}/petition`, {
		method: "GET",
		headers: {
			Authorization: `Bearer ${cookies.get("session_id")}`,
		},
	});

	if (!response.ok) {
		throw redirect(302, "/topics");
	}

	return {
		userRequested: (await response.json()) as IUser,
	};
};

export const actions: Actions = {
	accept: async ({ params, cookies }) => {
		const topicId = params.slug;

		const response = await fetch(`${API_URL}/topics/${topicId}/status/true`, {
			method: "PUT",
			headers: {
				Authorization: `Bearer ${cookies.get("session_id")}`,
			},
		});

		if (!response.ok) {
			const data = await response.json();

			return fail(400, { message: data.message });
		}

		throw redirect(303, `/temas/${topicId}`);
	},
	deny: async ({ params, cookies }) => {
		const topicId = params.slug;

		const response = await fetch(`${API_URL}/topics/${topicId}/status/false`, {
			method: "PUT",
			headers: {
				Authorization: `Bearer ${cookies.get("session_id")}`,
			},
		});

		if (!response.ok) {
			const data = await response.json();

			return fail(400, { message: data.message });
		}

		throw redirect(303, `/temas/${topicId}`);
	},
};
