<script lang="ts">
	import Notification from "./Notification.svelte";

	import { faExclamation, faThumbsUp } from "@fortawesome/free-solid-svg-icons";
	import type { ActionData } from "../routes/auth/$types";

	export let form: ActionData;
	export let successMessage: string | undefined = undefined;
</script>

{#if form?.success && successMessage}
	<Notification type="success" faIcon={faThumbsUp}>{successMessage}</Notification>
{/if}
{#if form?.message}
	<Notification type="error" faIcon={faExclamation}>
		{form.message}
	</Notification>
{/if}
