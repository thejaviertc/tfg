import { redirect, type Actions, fail } from "@sveltejs/kit";
import type { PageServerLoad } from "./$types";
import { API_URL } from "$lib/constants";

export const load: PageServerLoad = async ({ locals }) => {
	if (!locals.user) {
		throw redirect(302, "/");
	}
};

export const actions: Actions = {
	"update-info": async ({ request, cookies }) => {
		const sessionId = cookies.get("session_id");

		const response = await fetch(`${API_URL}/user/me`, {
			method: "PUT",
			body: await request.formData(),
			headers: {
				Authorization: `Bearer ${sessionId}`,
			},
		});

		if (!response.ok) {
			const data = await response.json();

			return fail(400, { message: data.message });
		}

		return { success: true };
	},
};
