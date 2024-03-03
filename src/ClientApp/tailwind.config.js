/** @type {import('tailwindcss').Config} */
export default {
	content: ["./src/**/*.{html,js,svelte,ts}"],
	theme: {
		extend: {},
	},
	plugins: [require("daisyui")],
	daisyui: {
		themes: [
			{
				upm: {
					primary: "#2581C4",
					secondary: "#E5E5E5",
					accent: "#202020",
				},
			},
		],
	},
	safelist: ["bg-primary", "bg-secondary", "bg-accent", "btn-primary", "btn-secondary"],
};
