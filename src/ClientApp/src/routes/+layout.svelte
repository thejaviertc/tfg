<script lang="ts">
	import "../app.scss";

	import Button from "$components/Button.svelte";

	import { faBars, faTriangleExclamation, faUser } from "@fortawesome/free-solid-svg-icons";
	import { onMount } from "svelte";
	import Fa from "svelte-fa";

	let navbarColor: string = "accent";

	onMount(() => {
		window.onscroll = () => {
			navbarColor =
				document.body.scrollTop >= 50 || document.documentElement.scrollTop >= 50
					? "primary"
					: "accent";
		};
	});
</script>

<nav class="navbar bg-{navbarColor} fixed">
	<div class="navbar-start">
		<div class="dropdown">
			<button tabindex="-1" class="btn btn-ghost lg:hidden pr-0"><Fa icon={faBars} /></button>
			<ul
				tabindex="-1"
				class="menu menu-sm dropdown-content mt-4 ml-1 p-2 shadow bg-secondary rounded-box w-64"
			>
				<Button class="btn-ghost" faIcon={faTriangleExclamation} link="/">Enlace</Button>
				<Button class="btn-ghost" faIcon={faTriangleExclamation} link="/">Enlace 2</Button>
				<Button class="btn-ghost" faIcon={faUser} link="/login">Iniciar Sesión</Button>
			</ul>
		</div>
		<Button class="btn-ghost pl-2" link="/">
			<img class="w-14" src="./upm.png" alt="Logo UPM" />
			TFG Temporal Name
		</Button>
	</div>
	<div class="navbar-end hidden lg:flex">
		<ul class="menu menu-horizontal px-1">
			<Button class="btn-ghost" faIcon={faTriangleExclamation} link="/">Enlace 1</Button>
			<Button class="btn-ghost" faIcon={faTriangleExclamation} link="/">Enlace 2</Button>
			<Button class="btn-ghost" faIcon={faUser} link="/login">Iniciar Sesión</Button>
		</ul>
	</div>
</nav>
<slot />
<footer class="footer p-6 footer-center bg-accent text-neutral-content">
	<h5>© 2024 - TFG Temporal Name - Hecho por Javier Toribio Couz</h5>
</footer>
