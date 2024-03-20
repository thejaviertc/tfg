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
	import type { PageData } from "../$types";
	import type { ActionData } from "./$types";

	export let data: PageData;
	export let form: ActionData;
</script>

<section class="min-h-screen hero">
	<div class="bg-secondary p-6 my-10 rounded-xl">
		<h3 class="text-black">Perfil de {data.user.name} {data.user.surname}:</h3>
		<div class="divider divider-accent mt-6">Información Básica</div>
		{#if form?.success}
			<Notification type="success" faIcon={faThumbsUp}>
				La información ha sido cambiada correctamente
			</Notification>
		{/if}
		{#if form?.message}
			<Notification type="error" faIcon={faExclamation}>
				{form.message}
			</Notification>
		{/if}
		<form method="POST" action="?/update-info">
			<InputForm
				id="name"
				label="Nombre"
				type="text"
				faIcon={faAddressCard}
				value={data.user.name}
			/>
			<InputForm
				id="surname"
				label="Apellidos"
				type="text"
				faIcon={faAddressCard}
				value={data.user.surname}
			/>
			<InputForm
				id="email"
				label="Email"
				type="email"
				faIcon={faEnvelope}
				value={data.user.email}
				isDisabled={true}
			/>
			<div class="mt-6 mx-auto flex flex-col">
				<button type="submit" class="btn btn-primary">
					<Fa class="mr-2" icon={faAddressCard} />
					Actualizar Información
				</button>
			</div>
		</form>
		<div class="divider divider-accent mt-10">Cambiar Contraseña</div>
		<form method="POST" action="?/update-password">
			<InputForm
				id="password"
				label="Contraseña Actual"
				type="password"
				faIcon={faKey}
				minlength={6}
			/>
			<InputForm
				id="newPassword"
				label="Nueva Contraseña"
				type="password"
				faIcon={faKey}
				minlength={6}
			/>
			<div class="mt-6 mx-auto flex flex-col">
				<button type="submit" class="btn btn-primary">
					<Fa class="mr-2" icon={faKey} />
					Actualizar Contraseña
				</button>
			</div>
		</form>
	</div>
</section>
