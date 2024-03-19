<script lang="ts">
	import InputForm from "$components/InputForm.svelte";
	import Notification from "$components/Notification.svelte";

	import {
		faAddressCard,
		faEnvelope,
		faExclamation,
		faKey,
		faThumbsUp,
	} from "@fortawesome/free-solid-svg-icons";
	import Fa from "svelte-fa";
	import type { ActionData } from "./$types";

	export let form: ActionData;
</script>

<section class="min-h-screen hero">
	<div class="bg-secondary p-6 rounded-xl">
		<h3 class="text-black">Regístrate:</h3>
		{#if form?.success}
			<Notification type="success" faIcon={faThumbsUp}>
				Te has registrado correctamente!
			</Notification>
		{/if}
		{#if form?.message}
			<Notification type="error" faIcon={faExclamation}>
				{form.message}
			</Notification>
		{/if}
		<form method="POST" action="?/register">
			<InputForm
				id="name"
				label="Nombre"
				type="text"
				placeholder="Nombre"
				faIcon={faAddressCard}
			/>
			<InputForm
				id="surname"
				label="Apellidos"
				type="text"
				placeholder="Apellidos"
				faIcon={faAddressCard}
			/>
			<InputForm
				id="email"
				label="Email"
				type="email"
				placeholder="Email"
				faIcon={faEnvelope}
			/>
			<InputForm
				id="password"
				label="Contraseña"
				type="password"
				placeholder="Contraseña"
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
</section>
