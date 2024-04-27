<script lang="ts">
	import type { IconDefinition } from "@fortawesome/free-solid-svg-icons";
	import Fa from "svelte-fa";

	export let faIcon: IconDefinition;
</script>

<div class="badge badge-primary badge-md p-3 text-white">
	<Fa icon={faIcon} class="mr-2" />
	<slot />
</div>
