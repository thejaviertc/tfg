<script lang="ts">
	import Button from "$components/Button.svelte";

	import { faAddressCard, faUser } from "@fortawesome/free-solid-svg-icons";
</script>

<section class="min-h-screen flex flex-col justify-center items-center text-center px-72">
	<h1>TFG Temporal Name</h1>
	<h5 class="my-10">
		Mi idea consiste en desarrollar una aplicación web destinada a facilitar la búsqueda de
		tutores y temas para TFGs en nuestra universidad, ya que en mi opinión, actualmente entre
		que los temas ofrecidos por los profesores no estuvieron disponibles hasta hace unas pocas
		semanas (dejando poco margen de tiempo) y que la universidad no nos ha enviado ninguna
		información para explicar todo el proceso del TFG ni para ayudarnos a la búsqueda de
		tutores, se puede dificultar la búsqueda de tutor, sobre todo si no tienes ideas para un
		TFG.
	</h5>
	<h5 class="mb-10">
		La aplicación web consistiría en un espacio donde los profesores puedan ofrecer sus temas de
		TFG (como se hace actualmente en el tablero de la ETSISI que hay en el Bloque IV) y que los
		alumnos a su vez puedan publicar ideas para TFGs en búsqueda de un tutor al que le guste la
		idea (de forma privada, sin que otros alumnos puedan ver la idea publicada para evitar
		plagios/robos de ideas).
	</h5>
	<div>
		<Button class="btn-accent" link="/registro" faIcon={faAddressCard}>Crear Cuenta</Button>
		<Button class="btn-accent" link="/login" faIcon={faUser}>Iniciar Sesión</Button>
	</div>
</section>
