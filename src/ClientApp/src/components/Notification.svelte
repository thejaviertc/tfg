<script lang="ts">
	import type { IconDefinition } from "@fortawesome/free-solid-svg-icons";
	import Fa from "svelte-fa";

	export let type: string;
	export let faIcon: IconDefinition;
</script>

<div role="alert" class="alert alert-{type} my-4">
	<Fa icon={faIcon} />
	<slot />
</div>
