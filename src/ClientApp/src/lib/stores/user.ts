import type { IUser } from "$lib/IUser";
import { writable } from "svelte/store";

const user = writable<IUser | null>(null);

export default user;
