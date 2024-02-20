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
		include: ["src/**/*.{test,spec}.{js,ts}"],
	},
};

export default config;
