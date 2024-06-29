<script lang="ts">
	import FormButton from "$components/FormButton.svelte";
	import FormInput from "$components/FormInput.svelte";
	import FormNotification from "$components/FormNotification.svelte";
	import FormTextArea from "$components/FormTextArea.svelte";

	import {
		faAddressCard,
		faBarsStaggered,
		faHeading,
		faPlus,
	} from "@fortawesome/free-solid-svg-icons";
	import type { ActionData } from "./$types";

	export let form: ActionData;
</script>

<FormNotification {form} />

<section class="min-h-screen flex justify-center items-center">
	<div class="bg-secondary p-6 my-10 max-w-96 w-full rounded-xl">
		<h3 class="text-black">Crear Tema:</h3>
		<form method="POST" action="?/add">
			<FormInput
				id="title"
				label="Título"
				type="text"
				faIcon={faHeading}
				minlength={6}
				maxlength={50}
			/>
			<FormTextArea
				id="shortDescription"
				label="Descripción Corta"
				faIcon={faBarsStaggered}
				minlength={20}
				maxlength={255}
			/>
			<FormTextArea
				id="description"
				label="Descripción"
				faIcon={faAddressCard}
				minlength={50}
			/>
			<FormButton class="btn-primary" faIcon={faPlus}>Crear Tema</FormButton>
		</form>
	</div>
</section>
