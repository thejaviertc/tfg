<script lang="ts">
	import Button from "$components/Button.svelte";
	import Idea from "$components/Idea.svelte";

	import TUserRole from "$lib/TUserRole";
	import { faPlus } from "@fortawesome/free-solid-svg-icons";

	export let data;
</script>

<section class="min-h-screen flex flex-col items-center">
	<h2 class="my-8">Lista de Ideas:</h2>
	{#if data.user.role === TUserRole.Alumno}
		<Button class="btn-accent mb-8" faIcon={faPlus} link="/ideas/nuevo">Añadir Idea</Button>
	{/if}
	{#if data.ideas.length > 0}
		<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8 mx-10 md:mx-28 mb-8">
			{#each data.ideas as idea}
				<Idea data={idea} />
			{/each}
		</div>
	{:else}
		<h3 class="text-center">Actualmente no hay temas disponibles</h3>
	{/if}
</section>
