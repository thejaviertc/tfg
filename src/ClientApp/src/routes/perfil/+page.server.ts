import AuthService from "$lib/AuthService";
import type { IIdea } from "$lib/IIdea";
import type { ITopic } from "$lib/ITopic";
import { API_URL } from "$lib/constants";
import { fail, redirect, type Actions } from "@sveltejs/kit";
import type { PageServerLoad } from "./$types";

export const load: PageServerLoad = async ({ cookies, locals }) => {
	AuthService.redirectNotLoggedUsers(locals);

	const topicsResponse = await fetch(`${API_URL}/topics/me`, {
		method: "GET",
		headers: {
			Authorization: `Bearer ${cookies.get("session_id")}`,
		},
	});

	const ideasResponse = await fetch(`${API_URL}/ideas/me`, {
		method: "GET",
		headers: {
			Authorization: `Bearer ${cookies.get("session_id")}`,
		},
	});

	return {
		topics: (await topicsResponse.json()) as ITopic[],
		ideas: (await ideasResponse.json()) as IIdea[],
	};
};

export const actions: Actions = {
	"update-info": async ({ request, cookies }) => {
		const response = await fetch(`${API_URL}/users/me`, {
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
	"update-password": async ({ request, cookies }) => {
		const response = await fetch(`${API_URL}/users/me/password`, {
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
	delete: async ({ request, cookies }) => {
		const response = await fetch(`${API_URL}/users/me`, {
			method: "DELETE",
			body: await request.formData(),
			headers: {
				Authorization: `Bearer ${cookies.get("session_id")}`,
			},
		});

		if (!response.ok) {
			const data = await response.json();

			return fail(400, { message: data.message });
		}

		cookies.delete("session_id", { path: "/" });

		throw redirect(303, "/");
	},
};
