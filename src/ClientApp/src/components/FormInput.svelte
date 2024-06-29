<script lang="ts">
	import { type IconDefinition } from "@fortawesome/free-brands-svg-icons";
	import Fa from "svelte-fa";

	export let id: string;
	export let label: string;
	export let type: string;
	export let faIcon: IconDefinition;
	export let minlength: number | undefined = undefined;
	export let maxlength: number | undefined = undefined;
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
			placeholder={label}
			{minlength}
			{maxlength}
			{value}
			class="input input-bordered input-accent pl-12 w-full min-w-72 max-w-96"
			required
			disabled={isDisabled}
		/>
	</div>
</div>
