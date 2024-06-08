enum TUserRole {
	Alumno,
	Profesor,
}

// eslint-disable-next-line @typescript-eslint/no-namespace
namespace TUserRole {
	export function toText(tUserRole: TUserRole): string {
		switch (tUserRole) {
			case TUserRole.Alumno:
				return "Alumno";
			case TUserRole.Profesor:
				return "Profesor";
			default:
				return "Desconocido";
		}
	}
}

export default TUserRole;
