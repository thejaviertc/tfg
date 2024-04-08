<script lang="ts">
	import Topic from "$components/Topic.svelte";

	import type { PageData } from "../$types";

	export let data: PageData;
</script>

<section class="min-h-screen flex flex-col items-center px-72">
	<h2 class="my-8">Temas Disponibles:</h2>
	<div class="grid grid-cols-3 gap-8">
		{#each data.topics as topic}
			<Topic data={topic} />
		{/each}
	</div>
</section>
