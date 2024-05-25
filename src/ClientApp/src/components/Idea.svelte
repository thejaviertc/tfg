<script lang="ts">
	import Badge from "./Badge.svelte";
	import Button from "./Button.svelte";

	import type { IIdea } from "$lib/IIdea";
	import TStatus from "$lib/TStatus";
	import { faCalendar, faEye } from "@fortawesome/free-solid-svg-icons";

	export let data: IIdea;
</script>

<div class="card bg-secondary shadow-xl">
	<div class="card-body">
		<h2 class="card-title text-black">{data.title}</h2>
		<div class="flex gap-2">
			<Badge class="badge-primary" faIcon={faCalendar}>
				{new Date(data.createdAt).toLocaleDateString()}
			</Badge>
			<Badge class={TStatus.getColor(data.status)} faIcon={TStatus.getFaIcon(data.status)}>
				{TStatus.toText(data.status)}
			</Badge>
		</div>
		<p class="text-black">{data.shortDescription}</p>
		<div class="card-actions justify-end">
			<Button class="btn-primary" faIcon={faEye} link="/ideas/{data.ideaId}">Ver Idea</Button>
		</div>
	</div>
</div>
