<script lang="ts">
	import Button from "$components/Button.svelte";
	import Idea from "$components/Idea.svelte";

	import TUserRole from "$lib/TUserRole";
	import { faPlus } from "@fortawesome/free-solid-svg-icons";

	export let data;
</script>

<section class="min-h-screen flex flex-col items-center px-72">
	<h2 class="mt-8">Ideas Disponibles:</h2>
	{#if data.user.role === TUserRole.ALUMNO}
		<Button class="btn-accent my-8" faIcon={faPlus} link="/ideas/nuevo">Añadir Idea</Button>
	{/if}
	<div class="grid grid-cols-3 gap-8">
		{#each data.ideas as idea}
			<Idea data={idea} />
		{/each}
	</div>
</section>
