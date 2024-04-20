<script lang="ts">
	import Notification from "$components/Notification.svelte";

	import { faCalendar, faExclamation } from "@fortawesome/free-solid-svg-icons";
	import Fa from "svelte-fa";
	import type { PageData } from "../$types";
	import type { ActionData } from "./$types";

	export let data: PageData;
	export let form: ActionData;
</script>

{#if form?.message}
	<Notification type="error" faIcon={faExclamation}>
		{form.message}
	</Notification>
{/if}

<section class="min-h-screen hero px-72">
	<div class="bg-secondary p-6 my-10 rounded-xl">
		<h3 class="text-black mb-2">{data.topic.title}</h3>
		<Fa icon={faCalendar}></Fa>
		{new Date(data.topic.createdAt).toLocaleDateString()}
		<h5 class="text-black mt-4">{data.topic.shortDescription}</h5>
		<h5 class="text-black mt-4 mb-2">{data.topic.description}</h5>
	</div>
</section>
