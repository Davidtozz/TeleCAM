import type { Contact } from "./chat.svelte";

class User {
    name: string = $state('');
    contacts = $state<Contact[]>([]);
}

export const user = new User();