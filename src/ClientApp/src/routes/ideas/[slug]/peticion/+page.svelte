<script lang="ts">
	import FormButton from "$components/FormButton.svelte";
	import FormInput from "$components/FormInput.svelte";

	import { faAddressCard, faBan, faCheck, faEnvelope } from "@fortawesome/free-solid-svg-icons";

	export let data;
</script>

<section class="min-h-screen flex justify-center items-center">
	<div class="bg-secondary p-6 my-10 rounded-xl">
		<h3 class="text-black">
			Petición de {data.userRequested.name}
			{data.userRequested.surname}:
		</h3>
		<FormInput
			id="name"
			label="Nombre"
			type="text"
			faIcon={faAddressCard}
			value={data.userRequested.name}
			isDisabled={true}
		/>
		<FormInput
			id="surname"
			label="Apellidos"
			type="text"
			faIcon={faAddressCard}
			value={data.userRequested.surname}
			isDisabled={true}
		/>
		<FormInput
			id="email"
			label="Email"
			type="email"
			faIcon={faEnvelope}
			value={data.userRequested.email}
			isDisabled={true}
		/>
		<div class="flex gap-2 justify-center">
			<form method="POST" action="?/accept">
				<FormButton class="btn-success" faIcon={faCheck}>Aceptar</FormButton>
			</form>
			<form method="POST" action="?/deny">
				<FormButton class="btn-error" faIcon={faBan}>Denegar</FormButton>
			</form>
		</div>
	</div>
</section>
