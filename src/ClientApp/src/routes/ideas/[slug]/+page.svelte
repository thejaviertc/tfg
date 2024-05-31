<script lang="ts">
	import Badge from "$components/Badge.svelte";
	import FormButton from "$components/FormButton.svelte";
	import FormNotification from "$components/FormNotification.svelte";

	import TStatus from "$lib/TStatus";
	import TUserRole from "$lib/TUserRole";
	import { faAt, faCalendar, faPaperPlane, faUser } from "@fortawesome/free-solid-svg-icons";
	import type { ActionData } from "./$types";

	export let data;
	export let form: ActionData;
</script>

<FormNotification {form} successMessage="Has solicitado la idea correctamente!" />

<section class="min-h-screen hero px-72">
	<div class="bg-secondary p-6 my-10 rounded-xl">
		<h3 class="text-black mb-2">{data.idea.title}</h3>
		<div class="flex flex-wrap gap-2">
			<Badge class="badge-primary" faIcon={faUser}>
				{data.user.name}
				{data.user.surname}
			</Badge>
			<Badge class="badge-primary" faIcon={faAt}>
				{data.user.email}
			</Badge>
			<Badge class="badge-primary" faIcon={faCalendar}>
				{new Date(data.idea.createdAt).toLocaleDateString()}
			</Badge>
			<Badge
				class={TStatus.getColor(data.idea.status)}
				faIcon={TStatus.getFaIcon(data.idea.status)}
			>
				{TStatus.toText(data.idea.status)}
			</Badge>
		</div>
		<h5 class="text-black mt-4">{data.idea.shortDescription}</h5>
		<h5 class="text-black mt-4 mb-2">{data.idea.description}</h5>
		{#if data.user.role === TUserRole.Profesor}
			<form method="POST" action="?/request">
				<FormButton class="btn-primary" faIcon={faPaperPlane}>Solicitar Idea</FormButton>
			</form>
		{/if}
	</div>
</section>
