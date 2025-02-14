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


<style>
    .slim-scrollbar {
        scrollbar-width: thin;
        scrollbar-color: #d4d4d4 transparent;
    }
</style>

<div class="flex h-screen antialiased text-gray-800">
    <div class="flex flex-col bg-slate-700 p-2 justify-between">
        <div class="flex items-center justify-center h-16 text-white font-bold">TeleCAM</div>
        <button 
        onclick={() => alert('Opened settings')}
        class="font-sans text-white p-2 hover:bg-slate-600 rounded-xl">
            Settings
        </button>
    </div>
    <div class="flex flex-col w-64 bg-gray-400 flex-shrink-0 h-screen overflow-hidden">
        <!-- <div class="flex p-4 flex-row items-center justify-center w-full gap-x-2">
            <div class="flex items-center justify-center rounded-2xl text-indigo-700 bg-indigo-100 h-10 w-10">
                <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 10h.01M12 10h.01M16 10h.01M9 16H5a2 2 0 01-2-2V6a2 2 0 012-2h14a2 2 0 012 2v8a2 2 0 01-2 2h-5l-5 5v-5z"></path>
                </svg>
            </div>
            <div class="font-bold text-2xl">TeleCAM</div>
        </div> -->
        <!-- Search -->
         <div class="rounded-xl p-3">
            <input type="text" class="w-full rounded-xl h-10 px-4 bg-gray-200 focus:outline-none focus:border-indigo-300" placeholder="Search" />
         </div>
        <div class="flex flex-col  slim-scrollbar overflow-hidden hover:overflow-y-auto bg-gray-200">
            <div class="flex flex-col space-y-1 hover:cursor-pointer">
                {#each contacts as contact}

                {@const hoverClass = selectedContact === contact ? 'bg-gray-200' : ''}
                    <!-- svelte-ignore a11y_click_events_have_key_events -->
                    <!-- svelte-ignore a11y_no_static_element_interactions -->
                    <button onclick={() => selectContact(contact)} 
                        
                        class={`flex flex-row flex-1 items-center p-2 ${hoverClass}`}>
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