<script lang="ts">
	import FormInput from "$components/FormInput.svelte";
	import FormNotification from "$components/FormNotification.svelte";
	import FormTextArea from "$components/FormTextArea.svelte";

	import {
		faAddressCard,
		faBarsStaggered,
		faHeading,
		faPlus,
	} from "@fortawesome/free-solid-svg-icons";
	import Fa from "svelte-fa";
	import type { ActionData } from "./$types";

	export let form: ActionData;
</script>

<FormNotification {form} />

<section class="min-h-screen flex justify-center items-center">
	<div class="bg-secondary p-6 my-10 rounded-xl">
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
			<div class="mt-6 mx-auto flex flex-col">
				<button type="submit" class="btn btn-primary">
					<Fa class="mr-2" icon={faPlus} />
					Crear Tema
				</button>
			</div>
		</form>
	</div>
</section>
