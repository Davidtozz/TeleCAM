<script lang="ts">
    import AddContactForm from '$lib/components/forms/AddContactForm.svelte';
    import Chatbox from '$lib/components/Chatbox.svelte';
    import ContactItem from '$lib/components/ContactItem.svelte';
    import Modal from '$lib/components/Modal.svelte';
    import { connection, SetUsername } from '$lib/hub';
    import { contacts, Contact } from '$lib/state/chat.svelte';
    import { onDestroy, onMount } from 'svelte';
    import { User } from '$lib/state/user.svelte';

    let selectedContact = $state<Contact | null>(null);
    let showModal = $state(false);

    function selectContact(contact: Contact) {
        selectedContact = contact;
    }
    
    function openModal() {
        showModal = true;
    }

    function closeModal() {
        showModal = false;
    }

    /* My example user */
    let user: User;

    onMount(async () => {
        try {
            await connection.start();
            user = new User(prompt('Choose your username')!);
            console.log(user);
        } catch(err) {
            console.error(err)
        }        
    });

    
    onDestroy(() => {
        contacts.length = 0;
    })
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
        <!-- Search -->
         <div class="rounded-xl p-3 flex flex-row gap-x-2">
            <input type="text" class="w-full rounded-xl h-10 px-4 bg-gray-200 focus:outline-none focus:border-indigo-300" placeholder="Search" />
            <button 
                onclick={openModal}
                type="button" class="font-bold text-xl antialiased bg-white">
                +
            </button>
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

    <Modal show={showModal} close={closeModal}>
        <AddContactForm />
    </Modal>
</div>