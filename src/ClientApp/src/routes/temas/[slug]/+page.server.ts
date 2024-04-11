import { API_URL } from "$lib/constants";
import { redirect } from "@sveltejs/kit";
import type { PageServerLoad } from "./$types";
import type { ITopic } from "$lib/ITopic";

export const load: PageServerLoad = async ({ cookies, locals, params }) => {
	if (!locals.user) {
		throw redirect(302, "/auth");
	}

	const topicId = params.slug;

	const response = await fetch(`${API_URL}/topics/${topicId}`, {
		method: "GET",
		headers: {
			Authorization: `Bearer ${cookies.get("session_id")}`,
		},
	});

	return {
		topic: (await response.json()) as ITopic,
	};
};
