import { redirect } from "@sveltejs/kit";
import type { Actions } from "./$types";

export const actions: Actions = {
	default: async ({ cookies }) => {
		cookies.delete("session_id", { path: "/" });

		throw redirect(302, "/login");
	},
};
