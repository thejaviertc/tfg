<script lang="ts">
	import Fa from "svelte-fa";
	import type { PageData } from "../$types";
	import { faCalendar } from "@fortawesome/free-solid-svg-icons";

	export let data: PageData;
</script>

<section class="min-h-screen hero px-72">
	<div class="bg-secondary p-6 my-10 rounded-xl">
		<h3 class="text-black mb-2">{data.topic.title}</h3>
		<Fa icon={faCalendar}></Fa>
		{new Date(data.topic.createdAt).toLocaleDateString()}
		<h5 class="text-black mt-2">{data.topic.description}</h5>
	</div>
</section>
