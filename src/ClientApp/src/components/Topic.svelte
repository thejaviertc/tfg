<script lang="ts">
	import Badge from "./Badge.svelte";
	import Button from "./Button.svelte";

	import type { ITopic } from "$lib/ITopic";
	import { faCalendar, faEye } from "@fortawesome/free-solid-svg-icons";

	export let data: ITopic;
</script>

<div class="card bg-secondary shadow-xl">
	<div class="card-body">
		<h2 class="card-title text-black">{data.title}</h2>
		<Badge faIcon={faCalendar}>
			{new Date(data.createdAt).toLocaleDateString()}
		</Badge>
		<p class="text-black">{data.shortDescription}</p>
		<div class="card-actions justify-end">
			<Button class="btn-primary" faIcon={faEye} link="/temas/{data.topicId}">
				Ver Tema
			</Button>
		</div>
	</div>
</div>
