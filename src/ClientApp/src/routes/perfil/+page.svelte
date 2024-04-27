<script lang="ts">
	import Button from "$components/Button.svelte";
	import FormButton from "$components/FormButton.svelte";
	import FormInput from "$components/FormInput.svelte";
	import FormNotification from "$components/FormNotification.svelte";

	import {
		faAddressCard,
		faEnvelope,
		faEye,
		faKey,
		faPencil,
		faTrash,
	} from "@fortawesome/free-solid-svg-icons";
	import Fa from "svelte-fa";
	import type { ActionData } from "./$types";

	export let data;
	export let form: ActionData;
</script>

<FormNotification {form} successMessage="La información ha sido cambiada correctamente!" />

<section class="min-h-screen flex gap-8 justify-center items-center">
	<div class="bg-secondary p-6 my-10 rounded-xl">
		<h3 class="text-black">Perfil de {data.user.name} {data.user.surname}:</h3>
		<div class="divider divider-accent mt-6">Información Básica</div>
		<form method="POST" action="?/update-info">
			<FormInput
				id="name"
				label="Nombre"
				type="text"
				faIcon={faAddressCard}
				value={data.user.name}
			/>
			<FormInput
				id="surname"
				label="Apellidos"
				type="text"
				faIcon={faAddressCard}
				value={data.user.surname}
			/>
			<FormInput
				id="email"
				label="Email"
				type="email"
				faIcon={faEnvelope}
				value={data.user.email}
				isDisabled={true}
			/>
			<FormButton class="btn-primary" faIcon={faAddressCard}>
				Actualizar Información
			</FormButton>
		</form>
		<div class="divider divider-accent mt-10">Cambiar Contraseña</div>
		<form method="POST" action="?/update-password">
			<FormInput
				id="password"
				label="Contraseña Actual"
				type="password"
				faIcon={faKey}
				minlength={6}
			/>
			<FormInput
				id="newPassword"
				label="Nueva Contraseña"
				type="password"
				faIcon={faKey}
				minlength={6}
			/>
			<FormButton class="btn-primary" faIcon={faKey}>Actualizar Contraseña</FormButton>
		</form>
		<div class="divider divider-accent mt-10">Zona Peligrosa</div>
		<form method="POST" action="?/delete">
			<FormInput
				id="password"
				label="Contraseña Actual"
				type="password"
				faIcon={faKey}
				minlength={6}
			/>
			<FormButton class="btn-error" faIcon={faTrash}>Eliminar Cuenta</FormButton>
		</form>
	</div>
	<div class="bg-secondary p-6 my-10 rounded-xl">
		<h3 class="text-black">Mis Temas:</h3>
		<div class="flex flex-wrap gap-4 justify-center mt-4">
			<div class="overflow-x-auto">
				<table class="table">
					<thead>
						<tr>
							<th>Título</th>
							<th>Descripcíon Corta</th>
							<th>Acciones</th>
						</tr>
					</thead>
					<tbody>
						{#each data.topics as topic}
							<tr>
								<th>{topic.title}</th>
								<td>{topic.shortDescription}</td>
								<td class="flex gap-2">
									<Button
										class="btn-primary"
										faIcon={faEye}
										link="/temas/{topic.topicId}"
									/>
									<Button
										class="btn-warning"
										faIcon={faPencil}
										link="/temas/{topic.topicId}/editar"
									/>
									<form method="POST" action="/temas/{topic.topicId}?/delete">
										<button type="submit" class="btn btn-error">
											<Fa icon={faTrash} />
										</button>
									</form>
								</td>
							</tr>
						{/each}
					</tbody>
				</table>
			</div>
		</div>
	</div>
</section>
