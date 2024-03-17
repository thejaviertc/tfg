<script lang="ts">
	import InputForm from "$components/InputForm.svelte";

	import { faAddressCard, faEnvelope, faKey } from "@fortawesome/free-solid-svg-icons";
	import Fa from "svelte-fa";
	import type { ActionData } from "./$types";

	export let form: ActionData;
</script>

<section class="h-screen hero">
	<div class="bg-secondary p-6 rounded-xl">
		<h3 class="text-black">Regístrate:</h3>
		{#if form?.success}
			<div role="alert" class="alert alert-success mt-5">
				Te has registrado correctamente!
			</div>
		{/if}
		{#if form?.message}
			<div role="alert" class="alert alert-error mt-5">
				{form.message}
			</div>
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
