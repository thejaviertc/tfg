<script lang="ts">
	import { faAddressCard } from "@fortawesome/free-solid-svg-icons";
	import Fa from "svelte-fa";

	import type { ActionData } from "./$types";

	export let form: ActionData;
</script>

<section>
	<div class="min-h-screen hero">
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
				<label for="name" class="label">Nombre:</label>
				<input
					id="name"
					name="name"
					type="text"
					placeholder="Nombre"
					class="input input-bordered input-accent"
					value="Javier"
					required
				/>
				<label for="surname" class="label">Apellidos:</label>
				<input
					id="surname"
					name="surname"
					type="text"
					placeholder="Apellidos"
					class="input input-bordered input-accent"
					value="Toribio Couz"
					required
				/>
				<label for="email" class="label">Email:</label>
				<input
					id="email"
					name="email"
					type="email"
					placeholder="Email"
					class="input input-bordered input-accent"
					value="javier.toribio@alumnos.upm.es"
					required
				/>
				<label for="password" class="label">Contraseña:</label>
				<input
					id="password"
					name="password"
					type="password"
					minlength="6"
					placeholder="Contraseña"
					class="input input-bordered input-accent"
					value="12345678"
					required
				/>
				<div class="mt-6 mx-auto flex flex-col gap-2">
					<button type="submit" class="btn btn-primary">
						<Fa class="mr-2" icon={faAddressCard} />
						Crear Cuenta
					</button>
				</div>
			</form>
		</div>
	</div>
</section>
