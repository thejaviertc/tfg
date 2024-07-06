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

<FormNotification {form} successMessage="Has actualizado el tema correctamente!" />

<section class="min-h-screen flex justify-center items-center">
	<div class="bg-secondary p-6 m-10 max-w-3xl w-full rounded-xl">
		<h3 class="text-black">Editar Tema:</h3>
		<form method="POST" action="?/update">
			<input type="hidden" id="topicId" name="topicId" value={data.topic.topicId} />
			<FormInput
				id="title"
				label="Título"
				type="text"
				faIcon={faHeading}
				value={data.topic.title}
				minlength={20}
				maxlength={100}
			/>
			<FormTextArea
				id="shortDescription"
				label="Descripción Corta"
				faIcon={faBarsStaggered}
				value={data.topic.shortDescription}
				minlength={20}
				maxlength={255}
			/>
			<FormTextArea
				id="description"
				label="Descripción"
				faIcon={faAddressCard}
				value={data.topic.description}
				minlength={50}
			/>
			<FormButton class="btn-primary" faIcon={faPencil}>Editar Tema</FormButton>
		</form>
	</div>
</section>
