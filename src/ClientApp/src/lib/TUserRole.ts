enum TUserRole {
	ALUMNO,
	PROFESOR,
}

// eslint-disable-next-line @typescript-eslint/no-namespace
namespace TUserRole {
	export function toText(tUserRole: TUserRole): string {
		switch (tUserRole) {
			case TUserRole.ALUMNO:
				return "Alumno";
			case TUserRole.PROFESOR:
				return "Profesor";
			default:
				return "Desconocido";
		}
	}
}

export default TUserRole;
