<script lang="ts">
	import type { IconDefinition } from "@fortawesome/free-solid-svg-icons";
	import Fa from "svelte-fa";

	export let faIcon: IconDefinition;
</script>

<div class="mt-6 mx-auto flex flex-col">
	<button type="submit" class="btn {$$props.class}">
		<Fa icon={faIcon} />
		<slot />
	</button>
</div>
