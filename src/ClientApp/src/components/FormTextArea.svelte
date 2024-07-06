<script lang="ts">
	import { type IconDefinition } from "@fortawesome/free-brands-svg-icons";
	import Fa from "svelte-fa";

	export let id: string;
	export let label: string;
	export let faIcon: IconDefinition;
	export let minlength: number | undefined = undefined;
	export let maxlength: number | undefined = undefined;
	export let value: string | null = null;
	export let isDisabled: boolean = false;
</script>

<div class="form-control">
	<label for={id} class="label">{label}:</label>
	<div class="label relative">
		<Fa icon={faIcon} class="absolute pl-5" />
		<textarea
			{id}
			name={id}
			placeholder={label}
			{minlength}
			{maxlength}
			{value}
			class="textarea textarea-bordered textarea-accent pl-12 w-full"
			required
			disabled={isDisabled}
		></textarea>
	</div>
</div>
