<script lang="ts">
	import "../app.scss";

	import Footer from "$components/Footer.svelte";
	import Navbar from "$components/Navbar.svelte";

	import type { PageData } from "./$types";

	export let data: PageData;
</script>

<svelte:head>
	<!-- TODO: Change -->
	<title>TFG Temporal Name</title>
</svelte:head>

<Navbar user={data.user} />
<slot />
<Footer />
