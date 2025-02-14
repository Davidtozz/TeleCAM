<script lang="ts">
    import Chatbox from '$lib/components/Chatbox.svelte';
    import ContactItem from '$lib/components/ContactItem.svelte';
    import { contacts, Contact } from '$lib/state/chat.svelte';
    import { onDestroy, onMount } from 'svelte';

    onMount(() => {
       for(let i = 0; i < 50; i++) {
           contacts.push(Contact.fromFakeData());
       }
       console.log(contacts);
    });

    onDestroy(() => {
        contacts.length = 0;
    })

    let selectedContact = $state<Contact | null>(null);
    function selectContact(contact: Contact) {
        selectedContact = contact;
    }

</script>

<div class="flex h-screen antialiased text-gray-800">
    <div class="flex flex-col py-8 pl-6 pr-2 w-64 bg-white flex-shrink-0 h-screen overflow-hidden">
        <div class="flex p-8 flex-row items-center justify-center w-full">
            <div class="flex items-center justify-center rounded-2xl text-indigo-700 bg-indigo-100 h-10 w-10">
                <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 10h.01M12 10h.01M16 10h.01M9 16H5a2 2 0 01-2-2V6a2 2 0 012-2h14a2 2 0 012 2v8a2 2 0 01-2 2h-5l-5 5v-5z"></path>
                </svg>
            </div>
            <div class="ml-2 font-bold text-2xl">TeleCAM</div>
        </div>
        <div class="flex flex-col overflow-y-scroll overflow-x-hidden">
            <div class="flex flex-row items-center justify-between text-xs">
                <span class="font-bold">Active Conversations</span>
                <span class="flex items-center justify-center bg-gray-300 h-4 w-4 rounded-full">
                    {contacts.length}
                </span>
            </div>
            <div class="flex flex-col space-y-1 mt-4 -mx-2 hover:cursor-pointer">
                {#each contacts as contact}
                    <!-- svelte-ignore a11y_click_events_have_key_events -->
                    <!-- svelte-ignore a11y_no_static_element_interactions -->
                    <button onclick={() => selectContact(contact)} class={selectedContact === contact ? 'bg-gray-200' : ''}>
                        <ContactItem {contact} />
                    </button>
                {/each}
            </div>
        </div>
    </div>
    {#if selectedContact}
        <Chatbox {selectedContact} />
    {/if}
</div>