import {
	faChartPie,
	faCheck,
	faHourglass,
	faWarning,
	type IconDefinition,
} from "@fortawesome/free-solid-svg-icons";

enum TStatus {
	Available,
	WaitingResponse,
	Accepted,
}

// eslint-disable-next-line @typescript-eslint/no-namespace
namespace TStatus {
	/**
	 * Converts the Enum into the corresponding string value
	 * @param tStatus
	 */
	export function toText(tStatus: TStatus): string {
		switch (tStatus) {
			case TStatus.Available:
				return "Disponible";
			case TStatus.WaitingResponse:
				return "Esperando Respuesta";
			case TStatus.Accepted:
				return "Aceptado";
			default:
				return "Desconocido";
		}
	}

	/**
	 * Converts the Enum into the corresponding faIcon
	 * @param tStatus
	 */
	export function getFaIcon(tStatus: TStatus): IconDefinition {
		switch (tStatus) {
			case TStatus.Available:
				return faChartPie;
			case TStatus.WaitingResponse:
				return faHourglass;
			case TStatus.Accepted:
				return faCheck;
			default:
				return faWarning;
		}
	}

	/**
	 * Converts the Enum into the corresponding color
	 * @param tStatus
	 */
	export function getColor(tStatus: TStatus): string {
		switch (tStatus) {
			case TStatus.Available:
				return "badge-primary";
			case TStatus.WaitingResponse:
				return "badge-warning";
			case TStatus.Accepted:
				return "badge-success";
			default:
				return "badge-error";
		}
	}
}

export default TStatus;
