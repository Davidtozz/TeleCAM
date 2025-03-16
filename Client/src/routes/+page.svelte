<script lang="ts">
  import { goto } from "$app/navigation";
  import { authService } from "$lib/api/services/auth-service.svelte";
  import { contactService } from "$lib/api/services/contact-service";
    import Chatbox from "$lib/components/Chatbox.svelte";
    import ContactItem from "$lib/components/ContactItem.svelte";
    import { connection, init } from "$lib/hub";
    import { Contact } from "$lib/state/chat.svelte";
    import { modal } from "$lib/state/modal.svelte";
    import { user } from "$lib/state/user.svelte";
    import { 
        Menu, 
        MessageCircle, 
        Folder, 
        Sun,
        Moon} from "@lucide/svelte";
    import { ModeWatcher, mode, toggleMode } from "mode-watcher";
    import { onDestroy, onMount } from "svelte";

    const DEFAULT_ICON_SIZE = 30;

    onMount(async ()=>{
        /* await init();   */          

        user.contacts = await contactService.getContacts();

        if(import.meta.env.DEV && user.contacts.length === 0) {
            for(let i = 0; i < 20; i++){
                user.contacts.push(Contact.fromFakeData());
            }
        } 
    })

    let selectedContact = $state<Contact | null>(null);
    
    onDestroy(()=>{
        user.contacts = [];
    })

    function handleEscapeKey(e: KeyboardEvent) {
        if(e.key === "Escape") {
            if(modal.state === "closed") {
                selectedContact = null;
            } else {
                modal.close();
            }
        }
    }
</script>

<svelte:body onkeydown={handleEscapeKey}/>

<div class="flex h-screen antialiased" class:dark={$mode === "dark"}>
    <!-- sidebar -->
    <nav class="flex flex-col w-fit bg-secondary dark:bg-dark-primary">
        <button class="flex items-center justify-center h-16 hover:bg-hover dark:hover:bg-hover">
            <Menu size={DEFAULT_ICON_SIZE-5} class="text-sidebar"/>
        </button> 
        <button class="p-4 flex flex-col items-center justify-center hover:bg-hover dark:hover:bg-hover text-xs text-sidebar">
            <MessageCircle size={DEFAULT_ICON_SIZE} />
            All Chats
        </button>
        <button class="p-4 flex flex-col items-center justify-center hover:bg-hover dark:hover:bg-hover text-sidebar text-sm">
            <Folder size={DEFAULT_ICON_SIZE} />
            Contact
        </button> 
        <button 
        onclick={() => toggleMode()}
        class="p-4 flex flex-col items-center justify-center hover:bg-hover dark:hover:bg-hover text-sidebar text-sm">
            {#if $mode === "dark"}
                <Moon size={DEFAULT_ICON_SIZE} />
            {:else}
                <Sun size={DEFAULT_ICON_SIZE} />
            {/if}
            Theme
        </button> 
    </nav>
    <!-- contact list -->
    <div class="bg-primary dark:bg-dark-secondary flex-[2] border-2 border-primary dark:border-none overflow-y-scroll max-h-screen telegram-scrollbar scrollbar-thin">
        <div class="flex p-4 bg-primary dark:bg-dark-secondary">
            <input class="rounded-full indent-3 border-none outline-none bg-secondary p-2 w-full" placeholder="Search"/>
        </div>
        <div class="flex flex-col hover:bg-secondary dark:hover:bg-dark-secondary ">
            {#each user.contacts as contact}                 
                <ContactItem {contact} onclick={() => selectedContact = contact}/>                
            {/each}
        </div>        
    </div>
    <Chatbox {selectedContact}/>
</div>


<ModeWatcher/>