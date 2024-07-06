<script lang="ts">
	import Button from "$components/Button.svelte";
	import Topic from "$components/Topic.svelte";

	import TUserRole from "$lib/TUserRole";
	import { faPlus } from "@fortawesome/free-solid-svg-icons";

	export let data;
</script>

<section class="min-h-screen flex flex-col items-center">
	<h2 class="my-8">Lista de Temas:</h2>
	{#if data.user.role === TUserRole.Profesor}
		<Button class="btn-accent mb-8" faIcon={faPlus} link="/temas/nuevo">Añadir Tema</Button>
	{/if}
	{#if data.topics.length > 0}
		<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8 mx-10 md:mx-28 mb-8">
			{#each data.topics as topic}
				<Topic data={topic} />
			{/each}
		</div>
	{:else}
		<h3 class="text-center">Actualmente no hay temas disponibles</h3>
	{/if}
</section>
