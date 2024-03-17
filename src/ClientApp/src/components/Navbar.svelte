<script lang="ts">
	import Button from "$components/Button.svelte";

	import type { IUser } from "$lib/IUser";
	import {
		faAddressCard,
		faBars,
		faTriangleExclamation,
		faUser,
	} from "@fortawesome/free-solid-svg-icons";
	import { onMount } from "svelte";
	import Fa from "svelte-fa";

	export let user: IUser;

	let navbarColor: string = "accent";

	/**
	 * Controlls the navbar color depending on the scroll
	 */
	onMount(() => {
		window.onscroll = () => {
			navbarColor =
				document.body.scrollTop >= 50 || document.documentElement.scrollTop >= 50
					? "primary"
					: "accent";
		};
	});
</script>

<nav class="navbar bg-{navbarColor} top-0 z-50 sticky">
	<div class="navbar-start">
		<div class="dropdown">
			<button tabindex="-1" class="btn btn-ghost lg:hidden pr-0"><Fa icon={faBars} /></button>
			<ul
				tabindex="-1"
				class="menu menu-sm dropdown-content mt-4 ml-1 p-2 shadow bg-secondary rounded-box w-64"
			>
				<Button class="btn-ghost" faIcon={faTriangleExclamation} link="/">Enlace</Button>
				<Button class="btn-ghost" faIcon={faTriangleExclamation} link="/">Enlace 2</Button>
				<Button class="btn-ghost" faIcon={faTriangleExclamation} link="/">Enlace 3</Button>
			</ul>
		</div>
		<Button class="btn-ghost pl-2" link="/">
			<img class="w-14" src="./upm.png" alt="Logo UPM" />
			TFG Temporal Name
		</Button>
	</div>
	<div class="navbar-end hidden lg:flex">
		<ul class="menu menu-horizontal px-1">
			{#if user}
				<Button class="btn-ghost" faIcon={faAddressCard} link="/perfil">
					Bienvenido, {user.name}
					{user.surname}
				</Button>
				<form action="/logout" method="POST">
					<button type="submit" class="btn btn-ghost">
						<Fa icon={faUser} />
						Cerrar Sesión
					</button>
				</form>
			{:else}
				<Button class="btn-ghost" faIcon={faAddressCard} link="/registro">
					Crear Cuenta
				</Button>
				<Button class="btn-ghost" faIcon={faUser} link="/login">Iniciar Sesión</Button>
			{/if}
		</ul>
	</div>
</nav>
