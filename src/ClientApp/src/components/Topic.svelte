<script lang="ts">
	import Button from "./Button.svelte";

	import type { ITopic } from "$lib/ITopic";
	import { faCalendar, faEye } from "@fortawesome/free-solid-svg-icons";
	import Fa from "svelte-fa";

	export let data: ITopic;
</script>

<div class="card bg-secondary shadow-xl">
	<div class="card-body">
		<h2 class="card-title text-black">{data.title}</h2>
		<div class="badge badge-primary badge-md p-3 text-white">
			<Fa icon={faCalendar} class="mr-2"></Fa>
			{new Date(data.createdAt).toLocaleDateString()}
		</div>
		<p class="text-black">{data.shortDescription}</p>
		<div class="card-actions justify-end">
			<Button class="btn-primary" faIcon={faEye} link="/temas/{data.topicId}">
				Ver Tema
			</Button>
		</div>
	</div>
</div>
