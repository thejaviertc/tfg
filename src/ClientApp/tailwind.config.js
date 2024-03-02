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
					"primary": "#FAFAFA",
					"secondary": "#E5E5E5",
					"accent": "#2581C4",
					"neutral": "#202020",
				},
			},
		],
	},
	safelist: ["bg-primary", "bg-secondary", "btn-primary", "btn-secondary"],
};