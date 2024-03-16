import { API_URL } from "$lib/constants";
import type { Handle } from "@sveltejs/kit";

export const handle: Handle = async ({ event, resolve }) => {
	if (event.locals.user) {
		return await resolve(event);
	}

	const sessionId = event.cookies.get("session_id");

	if (!sessionId) {
		return await resolve(event);
	}

	const response = await fetch(`${API_URL}/auth/me`, {
		method: "GET",
		headers: {
			Authorization: `Bearer ${sessionId}`,
		},
	});

	if (!response.ok) {
		return await resolve(event);
	}

	event.locals.user = await response.json();

	return await resolve(event);
};
