<script lang="ts">
	import FormInput from "$components/FormInput.svelte";
	import FormNotification from "$components/FormNotification.svelte";
	import FormTextArea from "$components/FormTextArea.svelte";

	import {
		faAddressCard,
		faBarsStaggered,
		faHeading,
		faPencil,
	} from "@fortawesome/free-solid-svg-icons";
	import Fa from "svelte-fa";
	import type { ActionData } from "./$types";

	export let data;
	export let form: ActionData;
</script>

<FormNotification {form} successMessage="Has actualizado el tema correctamente!" />

<section class="min-h-screen flex justify-center items-center">
	<div class="bg-secondary p-6 my-10 rounded-xl">
		<h3 class="text-black">Editar Tema:</h3>
		<form method="POST" action="?/update">
			<input type="hidden" id="topicId" name="topicId" value={data.topic.topicId} />
			<FormInput
				id="title"
				label="Título"
				type="text"
				faIcon={faHeading}
				value={data.topic.title}
				minlength={6}
				maxlength={50}
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
			<div class="mt-6 mx-auto flex flex-col">
				<button type="submit" class="btn btn-primary">
					<Fa class="mr-2" icon={faPencil} />
					Editar Tema
				</button>
			</div>
		</form>
	</div>
</section>
