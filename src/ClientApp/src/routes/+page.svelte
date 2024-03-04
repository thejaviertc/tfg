<script lang="ts">
	import { faAddressCard, faUser } from "@fortawesome/free-solid-svg-icons";
	import Fa from "svelte-fa";

	let registerModal: HTMLElement;
</script>

<svelte:head>
	<!-- TODO: Change -->
	<title>TFG Temporal Name</title>
</svelte:head>

<div class="hero min-h-screen bg-primary">
	<div class="hero-content flex-row">
		<div class="text-center lg:text-left">
			<h1>TFG Temporal Name</h1>
			<p class="mt-6">
				Mi idea consiste en desarrollar una aplicación web destinada a facilitar la búsqueda
				de tutores y temas para TFGs en nuestra universidad, ya que en mi opinión,
				actualmente entre que los temas ofrecidos por los profesores no estuvieron
				disponibles hasta hace unas pocas semanas (dejando poco margen de tiempo) y que la
				universidad no nos ha enviado ninguna información para explicar todo el proceso del
				TFG ni para ayudarnos a la búsqueda de tutores, se puede dificultar la búsqueda de
				tutor, sobre todo si no tienes ideas para un TFG.
			</p>
			<p class="mt-4">
				La aplicación web consistiría en un espacio donde los profesores puedan ofrecer sus
				temas de TFG (como se hace actualmente en el tablero de la ETSISI que hay en el
				Bloque IV) y que los alumnos a su vez puedan publicar ideas para TFGs en búsqueda de
				un tutor al que le guste la idea (de forma privada, sin que otros alumnos puedan ver
				la idea publicada para evitar plagios/robos de ideas).
			</p>
		</div>
		<div class="card bg-secondary ml-10">
			<form class="card-body">
				<label for="email" class="label">Email</label>
				<input
					id="email"
					type="email"
					placeholder="Email"
					class="input input-bordered input-accent"
					required
				/>
				<label for="password" class="label">Contraseña</label>
				<input
					id="password"
					type="password"
					placeholder="Contraseña"
					class="input input-bordered input-accent"
					required
				/>
				<div class="mt-6 mx-auto flex flex-col gap-2">
					<button type="submit" class="btn btn-primary">
						<Fa class="mr-2" icon={faUser} />
						Iniciar Sesión
					</button>
					<button on:click={registerModal.showModal()} class="btn btn-primary">
						<Fa class="mr-2" icon={faAddressCard} />
						Crear Cuenta
					</button>
				</div>
			</form>
		</div>
	</div>
</div>
<dialog bind:this={registerModal} class="modal">
	<div class="modal-box bg-secondary">
		<form method="dialog">
			<button class="btn btn-sm btn-circle btn-ghost absolute right-2 top-2">✕</button>
		</form>
		<h3 class="text-black">Regístrate:</h3>
		<form class="card-body" action="/api/auth/register" method="POST">
			<label for="name" class="label">Nombre:</label>
			<input
				id="name"
				name="name"
				type="text"
				placeholder="Nombre"
				class="input input-bordered input-accent"
				required
			/>
			<label for="surname" class="label">Apellidos:</label>
			<input
				id="surname"
				name="surname"
				type="text"
				placeholder="Apellidos"
				class="input input-bordered input-accent"
				required
			/>
			<label for="email" class="label">Email:</label>
			<input
				id="email"
				name="email"
				type="email"
				placeholder="Email"
				class="input input-bordered input-accent"
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
</dialog>
