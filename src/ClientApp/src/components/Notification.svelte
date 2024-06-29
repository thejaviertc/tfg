<script lang="ts">
	// @ts-expect-error Not detected by VSCode
	import { get_current_component } from "svelte/internal";

	import type { IconDefinition } from "@fortawesome/free-solid-svg-icons";
	import { onMount } from "svelte";
	import Fa from "svelte-fa";

	export let type: string;
	export let faIcon: IconDefinition;

	const currentComponent = get_current_component();

	/**
	 * Destroys the component after 5 seconds
	 */
	onMount(() => {
		const timeout = setTimeout(() => {
			currentComponent.$destroy();
		}, 5000);

		return () => {
			clearTimeout(timeout);
		};
	});
</script>

<div class="toast toast-center lg:toast-end z-50">
	<div role="alert" class="alert alert-{type} my-4">
		<Fa icon={faIcon} />
		<slot />
	</div>
</div>
