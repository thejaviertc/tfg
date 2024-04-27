<script lang="ts">
	import Button from "$components/Button.svelte";
	import Topic from "$components/Topic.svelte";

	import TUserRole from "$lib/TUserRole";
	import { faPlus } from "@fortawesome/free-solid-svg-icons";

	export let data;
</script>

<section class="min-h-screen flex flex-col items-center px-72">
	<h2 class="mt-8">Temas Disponibles:</h2>
	{#if data.user.role === TUserRole.PROFESOR}
		<Button class="btn-accent my-8" faIcon={faPlus} link="/temas/nuevo">Añadir Tema</Button>
	{/if}
	<div class="grid grid-cols-3 gap-8">
		{#each data.topics as topic}
			<Topic data={topic} />
		{/each}
	</div>
</section>
