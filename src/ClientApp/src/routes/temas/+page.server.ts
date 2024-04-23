import { API_URL } from "$lib/constants";
import { redirect } from "@sveltejs/kit";
import type { PageServerLoad } from "./$types";
import type { ITopic } from "$lib/ITopic";

export const load: PageServerLoad = async ({ cookies, locals }) => {
	if (!locals.user) {
		throw redirect(302, "/auth");
	}

	const response = await fetch(`${API_URL}/topics`, {
		method: "GET",
		headers: {
			Authorization: `Bearer ${cookies.get("session_id")}`,
		},
	});

	return {
		topics: (await response.json()) as ITopic[],
	};
};
