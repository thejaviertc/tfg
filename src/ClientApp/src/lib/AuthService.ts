import { redirect } from "@sveltejs/kit";
import TUserRole from "./TUserRole";

export default class AuthService {
	/**
	 * Prevents not logged users entering protected routes
	 * @param locals App.Locals
	 */
	public static redirectNotLoggedUsers(locals: App.Locals) {
		if (!locals.user) {
			throw redirect(302, "/auth");
		}
	}

	/**
	 * Prevents users without Student role entering protected routes
	 * @param locals App.Locals
	 */
	public static redirectNotStudents(locals: App.Locals) {
		if (locals.user.role !== TUserRole.Alumno) {
			throw redirect(302, "/");
		}
	}

	/**
	 * Prevents users without Teacher role entering protected routes
	 * @param locals App.Locals
	 */
	public static redirectNotTeachers(locals: App.Locals) {
		if (locals.user.role !== TUserRole.Profesor) {
			throw redirect(302, "/");
		}
	}
}
