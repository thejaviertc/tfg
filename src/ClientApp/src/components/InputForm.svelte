<script lang="ts">
	import { type IconDefinition } from "@fortawesome/free-brands-svg-icons";
	import Fa from "svelte-fa";

	export let id: string;
	export let label: string;
	export let type: string;
	export let placeholder: string | undefined = undefined;
	export let faIcon: IconDefinition;
	export let minlength: number | undefined = undefined;
	export let value: string | null = null;
	export let isDisabled: boolean = false;
</script>

<div class="form-control">
	<label for={id} class="label">{label}:</label>
	<div class="label">
		<Fa icon={faIcon} class="absolute pl-5" />
		<input
			{id}
			name={id}
			{type}
			{placeholder}
			{minlength}
			{value}
			class="input input-bordered input-accent pl-12"
			required
			disabled={isDisabled}
		/>
	</div>
</div>
