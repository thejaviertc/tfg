<script lang="ts">
	import Badge from "$components/Badge.svelte";
	import FormNotification from "$components/FormNotification.svelte";

	import { faCalendar } from "@fortawesome/free-solid-svg-icons";
	import type { ActionData } from "./$types";

	export let data;
	export let form: ActionData;
</script>

<FormNotification {form} />

<section class="min-h-screen hero px-72">
	<div class="bg-secondary p-6 my-10 rounded-xl">
		<h3 class="text-black mb-2">{data.idea.title}</h3>
		<Badge class="badge-primary" faIcon={faCalendar}>
			{new Date(data.idea.createdAt).toLocaleDateString()}
		</Badge>
		<h5 class="text-black mt-4">{data.idea.shortDescription}</h5>
		<h5 class="text-black mt-4 mb-2">{data.idea.description}</h5>
	</div>
</section>
