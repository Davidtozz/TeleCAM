<script lang="ts">
    import { Contact, contacts, type Status } from "$lib/state/chat.svelte";
    import { getContext } from "svelte";

    let name = $state('');
    let status = $state<Status>('offline');

    let close: () => any = getContext('close');

    function addContact(e: SubmitEvent) {
        e.preventDefault();
        const newContact = new Contact(name)
        newContact.status = status;
        contacts.push(newContact);
        close();
    }

</script>

<form onsubmit={addContact}>
    <div class="mb-4">
        <label class="block text-gray-700 text-sm font-bold mb-2" for="name">Name</label>
        <input type="text" id="name" bind:value={name} class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" required>
    </div>
    <div class="mb-4">
        <label class="block text-gray-700 text-sm font-bold mb-2" for="status">Status</label>
        <select id="status" bind:value={status} class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline">
            <option value="online">Online</option>
            <option value="offline">Offline</option>
            <option value="away">Away</option>
        </select>
    </div>
    <div class="flex items-center justify-between">
        <button type="submit" class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline">Add Contact</button>
    </div>
</form>