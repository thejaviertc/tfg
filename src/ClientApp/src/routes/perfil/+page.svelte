<script lang="ts">
	import Button from "$components/Button.svelte";
	import InputForm from "$components/InputForm.svelte";
	import Notification from "$components/Notification.svelte";

	import {
		faAddressCard,
		faBook,
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

<section class="min-h-screen flex gap-8 justify-center items-center">
	<div class="bg-secondary p-6 my-10 rounded-xl">
		<h3 class="text-black">Perfil de {data.user.name} {data.user.surname}:</h3>
		<div class="divider divider-accent mt-6">Información Básica</div>
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
		<div class="divider divider-accent mt-10">Peligro</div>
		<form method="POST" action="?/delete">
			<InputForm
				id="password"
				label="Contraseña Actual"
				type="password"
				faIcon={faKey}
				minlength={6}
			/>
			<div class="mt-6 mx-auto flex flex-col">
				<button type="submit" class="btn btn-error">
					<Fa class="mr-2" icon={faKey} />
					Eliminar Cuenta
				</button>
			</div>
		</form>
	</div>
	<div class="bg-secondary p-6 my-10 rounded-xl">
		<h3 class="text-black">Mis Temas:</h3>
		<div class="flex flex-wrap gap-4 justify-center mt-4">
			{#each data.topics as topic}
				<Button class="btn-primary" faIcon={faBook} link="/temas/{topic.topicId}">
					{topic.title}
				</Button>
			{/each}
		</div>
	</div>
</section>
