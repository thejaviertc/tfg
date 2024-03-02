import { sveltekit } from "@sveltejs/kit/vite";

/** @type {import('vite').UserConfig} */
const config = {
	plugins: [sveltekit()],
	server: {
		proxy: {
			"/api": "http://localhost:5195",
		},
	},
	test: {
		environment: "jsdom",
		reporter: "verbose",
		coverage: {
			all: true,
			include: ["src/**/*.{js,ts,svelte}"],
		},
	},
};

export default config;
