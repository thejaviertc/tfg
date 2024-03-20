import { redirect } from "@sveltejs/kit";
import type { Actions, PageServerLoad } from "./$types";

export const load: PageServerLoad = async ({ locals }) => {
	if (!locals.user) {
		throw redirect(302, "/");
	}
};

export const actions: Actions = {
	default: async ({ cookies }) => {
		cookies.delete("session_id", { path: "/" });

		throw redirect(302, "/auth");
	},
};
