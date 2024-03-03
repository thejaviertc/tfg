<script lang="ts">
	import "../app.scss";

	import Button from "$components/Button.svelte";
	import { faBars, faTriangleExclamation } from "@fortawesome/free-solid-svg-icons";
	import Fa from "svelte-fa";
</script>

<nav class="navbar bg-accent fixed">
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
			<Button class="btn-ghost" faIcon={faTriangleExclamation} link="/">Enlace 1</Button>
			<Button class="btn-ghost" faIcon={faTriangleExclamation} link="/">Enlace 2</Button>
			<Button class="btn-ghost" faIcon={faTriangleExclamation} link="/">Enlace 3</Button>
		</ul>
	</div>
</nav>
<slot />
<footer class="footer p-6 footer-center bg-accent text-neutral-content">
	<h5>© 2024 - TFG Temporal Name - Hecho por Javier Toribio Couz</h5>
</footer>
