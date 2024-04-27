<script lang="ts">
	import FormInput from "$components/FormInput.svelte";
	import FormNotification from "$components/FormNotification.svelte";

	import { faAddressCard, faEnvelope, faKey, faUser } from "@fortawesome/free-solid-svg-icons";
	import Fa from "svelte-fa";
	import type { ActionData } from "./$types";

	export let form: ActionData;
</script>

<FormNotification {form} successMessage="Te has registrado correctamente!" />

<section class="min-h-screen flex gap-8 justify-center items-center">
	<div class="bg-secondary p-6 my-10 rounded-xl">
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
			<div class="mt-6 mx-auto flex flex-col">
				<button type="submit" class="btn btn-primary">
					<Fa class="mr-2" icon={faAddressCard} />
					Crear Cuenta
				</button>
			</div>
		</form>
	</div>
	<div class="bg-secondary p-6 my-10 rounded-xl">
		<h3 class="text-black">Iniciar Sesión:</h3>
		<form method="POST" action="?/login">
			<FormInput id="email" label="Email" type="email" faIcon={faEnvelope} />
			<FormInput id="password" label="Contraseña" type="password" faIcon={faKey} />
			<div class="mt-6 mx-auto flex flex-col">
				<button type="submit" class="btn btn-primary">
					<Fa class="mr-2" icon={faUser} />
					Iniciar Sesión
				</button>
			</div>
		</form>
	</div>
</section>
