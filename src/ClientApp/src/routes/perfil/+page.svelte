<script lang="ts">
	import Button from "$components/Button.svelte";
	import FormButton from "$components/FormButton.svelte";
	import FormInput from "$components/FormInput.svelte";
	import FormNotification from "$components/FormNotification.svelte";

	import TStatus from "$lib/TStatus";
	import TUserRole from "$lib/TUserRole";
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

<FormNotification
	{form}
	successMessage="¡La información ha sido cambiada correctamente!"
/>

<section class="min-h-screen flex flex-col xl:flex-row xl:gap-8 justify-center items-center">
	<div class="bg-secondary p-6 mt-10 xl:mb-10 mx-4 rounded-xl">
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
	{#if data.user.role === TUserRole.Profesor}
		<div class="bg-secondary p-6 my-10 mx-10 rounded-xl">
			<h3 class="text-black">Mis Temas:</h3>
			<table class="table mt-4">
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
							<th class="p-3">{topic.title}</th>
							<td class="p-3">{topic.shortDescription}</td>
							<td class="p-3">
								<div class="flex flex-wrap gap-2 items-center justify-center">
									<Button
										class="btn-primary"
										faIcon={faEye}
										link="/temas/{topic.topicId}"
									>
										Ver
									</Button>
									<Button
										class="btn-warning"
										faIcon={faPencil}
										link="/temas/{topic.topicId}/editar"
									>
										Editar
									</Button>
									{#if topic.status === TStatus.WaitingResponse}
										<Button
											class="btn-success"
											faIcon={faEnvelope}
											link="/temas/{topic.topicId}/peticion"
										>
											Responder
										</Button>
									{/if}
									<form method="POST" action="/temas/{topic.topicId}?/delete">
										<button type="submit" class="btn btn-error">
											<Fa icon={faTrash} />
											Eliminar
										</button>
									</form>
								</div>
							</td>
						</tr>
					{/each}
				</tbody>
			</table>
		</div>
	{/if}
	{#if data.user.role === TUserRole.Alumno}
		<div class="bg-secondary p-6 my-10 mx-10 rounded-xl">
			<h3 class="text-black">Mis Ideas:</h3>
			<table class="table mt-4">
				<thead>
					<tr>
						<th>Título</th>
						<th>Descripción Corta</th>
						<th>Acciones</th>
					</tr>
				</thead>
				<tbody>
					{#each data.ideas as idea}
						<tr>
							<th class="p-3">{idea.title}</th>
							<td class="p-3">{idea.shortDescription}</td>
							<td class="p-3">
								<div class="flex flex-wrap gap-2 items-center justify-center">
									<Button
										class="btn-primary"
										faIcon={faEye}
										link="/ideas/{idea.ideaId}"
									>
										Ver
									</Button>
									<Button
										class="btn-warning"
										faIcon={faPencil}
										link="/ideas/{idea.ideaId}/editar"
									>
										Editar
									</Button>
									{#if idea.status === TStatus.WaitingResponse}
										<Button
											class="btn-success"
											faIcon={faEnvelope}
											link="/ideas/{idea.ideaId}/peticion"
										>
											Responder
										</Button>
									{/if}
									<form method="POST" action="/ideas/{idea.ideaId}?/delete">
										<button type="submit" class="btn btn-error">
											<Fa icon={faTrash} />
											Eliminar
										</button>
									</form>
								</div>
							</td>
						</tr>
					{/each}
				</tbody>
			</table>
		</div>
	{/if}
</section>
