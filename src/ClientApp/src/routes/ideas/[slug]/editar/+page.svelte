<script lang="ts">
	import FormButton from "$components/FormButton.svelte";
	import FormInput from "$components/FormInput.svelte";
	import FormNotification from "$components/FormNotification.svelte";
	import FormTextArea from "$components/FormTextArea.svelte";

	import {
		faAddressCard,
		faBarsStaggered,
		faHeading,
		faPencil,
	} from "@fortawesome/free-solid-svg-icons";
	import type { ActionData } from "./$types";

	export let data;
	export let form: ActionData;
</script>

<FormNotification {form} successMessage="Has actualizado la idea correctamente!" />

<section class="min-h-screen flex justify-center items-center">
	<div class="bg-secondary p-6 my-10 rounded-xl">
		<h3 class="text-black">Editar Idea:</h3>
		<form method="POST" action="?/update">
			<input type="hidden" id="ideaId" name="ideaId" value={data.idea.ideaId} />
			<FormInput
				id="title"
				label="Título"
				type="text"
				faIcon={faHeading}
				value={data.idea.title}
				minlength={6}
				maxlength={50}
			/>
			<FormTextArea
				id="shortDescription"
				label="Descripción Corta"
				faIcon={faBarsStaggered}
				value={data.idea.shortDescription}
				minlength={20}
				maxlength={255}
			/>
			<FormTextArea
				id="description"
				label="Descripción"
				faIcon={faAddressCard}
				value={data.idea.description}
				minlength={50}
			/>
			<FormButton class="btn-primary" faIcon={faPencil}>Editar Idea</FormButton>
		</form>
	</div>
</section>
