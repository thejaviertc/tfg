<script lang="ts">
	import "../app.scss";

	import Navbar from "$components/Navbar.svelte";

	import type { PageData } from "./$types";

	export let data: PageData;
</script>

<Navbar user={data.user} />
<slot />
<footer class="footer p-6 footer-center bg-accent text-neutral-content">
	<h5>© 2024 - TFG Temporal Name - Hecho por Javier Toribio Couz</h5>
</footer>
