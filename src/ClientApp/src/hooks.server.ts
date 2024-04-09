import { API_URL } from "$lib/constants";
import type { Handle } from "@sveltejs/kit";

/**
 * Obtains the current User data on every route if is logged
 */
export const handle: Handle = async ({ event, resolve }) => {
	if (event.locals.user) {
		return resolve(event);
	}

	const sessionId = event.cookies.get("session_id");

	if (!sessionId) {
		return resolve(event);
	}

	const response = await fetch(`${API_URL}/users/me`, {
		method: "GET",
		headers: {
			Authorization: `Bearer ${sessionId}`,
		},
	});

	if (!response.ok) {
		return resolve(event);
	}

	event.locals.user = await response.json();

	return resolve(event);
};
