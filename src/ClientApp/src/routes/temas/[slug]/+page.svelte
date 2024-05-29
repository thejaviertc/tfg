<script lang="ts">
	import Badge from "$components/Badge.svelte";
	import FormNotification from "$components/FormNotification.svelte";

	import TStatus from "$lib/TStatus";
	import { faAt, faCalendar, faUser } from "@fortawesome/free-solid-svg-icons";
	import type { ActionData } from "./$types";

	export let data;
	export let form: ActionData;
</script>

<FormNotification {form} />

<section class="min-h-screen hero px-72">
	<div class="bg-secondary p-6 my-10 rounded-xl">
		<h3 class="text-black mb-2">{data.topic.title}</h3>
		<div class="flex flex-wrap gap-2">
			<Badge class="badge-primary" faIcon={faUser}>
				{data.user.name}
				{data.user.surname}
			</Badge>
			<Badge class="badge-primary" faIcon={faAt}>
				{data.user.email}
			</Badge>
			<Badge class="badge-primary" faIcon={faCalendar}>
				{new Date(data.topic.createdAt).toLocaleDateString()}
			</Badge>
			<Badge
				class={TStatus.getColor(data.topic.status)}
				faIcon={TStatus.getFaIcon(data.topic.status)}
			>
				{TStatus.toText(data.topic.status)}
			</Badge>
		</div>
		<h5 class="text-black mt-4">{data.topic.shortDescription}</h5>
		<h5 class="text-black my-4">{data.topic.description}</h5>
		<!-- <Button class="btn-primary" faIcon={faPaperPlane} link="/">Solicitar Tema</Button> -->
	</div>
</section>
