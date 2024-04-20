<script lang="ts">
	import { faEye } from "@fortawesome/free-solid-svg-icons";
	import Button from "./Button.svelte";
	import type { ITopic } from "$lib/ITopic";

	export let data: ITopic;
</script>

<div class="card bg-secondary shadow-xl">
	<div class="card-body">
		<h2 class="card-title text-black">{data.title}</h2>
		<p class="text-black">{data.shortDescription}</p>
		<div class="card-actions justify-end">
			<Button class="btn-primary" faIcon={faEye} link="/temas/{data.topicId}">
				Ver Tema
			</Button>
		</div>
	</div>
</div>
