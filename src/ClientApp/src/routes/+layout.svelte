<script lang="ts">
	import "../app.scss";

	import Footer from "$components/Footer.svelte";
	import Navbar from "$components/Navbar.svelte";

	import type { PageData } from "./$types";

	export let data: PageData;
</script>

<Navbar user={data.user} />
<slot />
<Footer />
