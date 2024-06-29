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

<FormNotification {form} successMessage="Has solicitado el tema correctamente!" />

<section class="min-h-screen hero">
	<div class="bg-secondary p-6 my-10 mx-10 md:mx-28 rounded-xl">
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
		{#if data.user.role === TUserRole.Alumno && data.topic.status === TStatus.Available}
			<form method="POST" action="?/request">
				<FormButton class="btn-primary" faIcon={faPaperPlane}>Solicitar Tema</FormButton>
			</form>
		{/if}
	</div>
</section>
