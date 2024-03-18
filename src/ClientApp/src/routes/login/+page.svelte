<script lang="ts">
	import InputForm from "$components/InputForm.svelte";
	import Notification from "$components/Notification.svelte";

	import { faEnvelope, faExclamation, faKey, faUser } from "@fortawesome/free-solid-svg-icons";
	import Fa from "svelte-fa";
	import type { ActionData } from "./$types";

	export let form: ActionData;
</script>

<section class="h-screen hero">
	<div class="bg-secondary p-6 rounded-xl">
		<h3 class="text-black">Iniciar Sesión:</h3>
		{#if form?.message}
			<Notification type="error" faIcon={faExclamation}>
				{form.message}
			</Notification>
		{/if}
		<form method="POST" action="?/login">
			<InputForm
				id="email"
				label="Email"
				type="email"
				placeholder="Email"
				faIcon={faEnvelope}
			/>
			<InputForm id="password" label="Contraseña" type="password" faIcon={faKey} />
			<div class="mt-6 mx-auto flex flex-col">
				<button type="submit" class="btn btn-primary">
					<Fa class="mr-2" icon={faUser} />
					Iniciar Sesión
				</button>
			</div>
		</form>
	</div>
</section>
