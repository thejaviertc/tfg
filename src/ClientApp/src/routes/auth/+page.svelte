<script lang="ts">
	import FormButton from "$components/FormButton.svelte";
	import FormInput from "$components/FormInput.svelte";
	import FormNotification from "$components/FormNotification.svelte";

	import { faAddressCard, faEnvelope, faKey, faUser } from "@fortawesome/free-solid-svg-icons";
	import type { ActionData } from "./$types";

	export let form: ActionData;
</script>

<FormNotification {form} successMessage="Te has registrado correctamente!" />

<section class="min-h-screen flex flex-col md:flex-row md:gap-8 justify-center items-center p-10">
	<div class="bg-secondary p-6 max-w-2xl w-full rounded-xl">
		<h3 class="text-black">Regístrate:</h3>
		<form method="POST" action="?/register">
			<FormInput id="name" label="Nombre" type="text" faIcon={faAddressCard} />
			<FormInput id="surname" label="Apellidos" type="text" faIcon={faAddressCard} />
			<FormInput id="email" label="Email" type="email" faIcon={faEnvelope} />
			<FormInput
				id="password"
				label="Contraseña"
				type="password"
				faIcon={faKey}
				minlength={6}
			/>
			<FormButton class="btn-primary" faIcon={faAddressCard}>Crear Cuenta</FormButton>
		</form>
	</div>
	<div class="bg-secondary p-6 mt-10 md:mt-0 max-w-2xl w-full rounded-xl">
		<h3 class="text-black">Iniciar Sesión:</h3>
		<form method="POST" action="?/login">
			<FormInput id="email" label="Email" type="email" faIcon={faEnvelope} />
			<FormInput id="password" label="Contraseña" type="password" faIcon={faKey} />
			<FormButton class="btn-primary" faIcon={faUser}>Iniciar Sesión</FormButton>
		</form>
	</div>
</section>
