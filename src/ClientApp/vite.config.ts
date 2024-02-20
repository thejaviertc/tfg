import { sveltekit } from '@sveltejs/kit/vite';
import { defineConfig } from 'vitest/config';

export default defineConfig({
	plugins: [sveltekit()],
	server: {
		proxy: {
			'/api': 'http://localhost:5195'
		}
	},
	test: {
		include: ['src/**/*.{test,spec}.{js,ts}']
	}
});
