<script lang="ts">
	import Button from "$components/Button.svelte";

	import { faAddressCard, faBook, faLightbulb } from "@fortawesome/free-solid-svg-icons";

	export let data;
</script>

<section class="min-h-screen flex flex-col justify-center items-center text-center px-72">
	<h1>Conecta TFG</h1>
	<h3 class="mt-6 mb-10">
		Aplicación web para ayudar a la búsqueda de tutores y temas para Trabajos de Fin De Grado (TFG)
	</h3>
	<div>
		{#if data.user}
			<Button class="btn-accent" link="/ideas" faIcon={faLightbulb}>Ver Ideas</Button>
			<Button class="btn-accent" link="/temas" faIcon={faBook}>Ver Temas</Button>
		{:else}
			<Button class="btn-accent" link="/auth" faIcon={faAddressCard}>Empezar</Button>
		{/if}
	</div>
</section>
