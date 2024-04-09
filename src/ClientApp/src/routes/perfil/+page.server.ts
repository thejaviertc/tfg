import { redirect, type Actions, fail } from "@sveltejs/kit";
import type { PageServerLoad } from "./$types";
import { API_URL } from "$lib/constants";

export const load: PageServerLoad = async ({ locals }) => {
	if (!locals.user) {
		throw redirect(302, "/auth");
	}
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
