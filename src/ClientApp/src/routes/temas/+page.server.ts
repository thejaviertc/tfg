import { API_URL } from "$lib/constants";
import type { PageServerLoad } from "./$types";

export const load: PageServerLoad = async ({ cookies, locals }) => {
	if (locals.user) {
		const response = await fetch(`${API_URL}/topics`, {
			method: "GET",
			headers: {
				Authorization: `Bearer ${cookies.get("session_id")}`,
			},
		});

		return {
			topics: await response.json(),
		};
	}
};
