<script lang="ts">
    import { Contact } from "$lib/state/chat.svelte";
    import { 
        Menu, 
        MessageCircle, 
        Paperclip,
        Folder, 
        Search,
        Phone,
        Info,
        EllipsisVertical} from "@lucide/svelte";
    import { ModeWatcher, mode, toggleMode, userPrefersMode } from "mode-watcher";

    let clicked = $state(false);

    const LIGHTMODE_GRAY_FONT = "#8393a3";
    const DEFAULT_ICON_SIZE = 30;

    const selectedContact = $state<Contact | null>(new Contact("Pippo"));
</script>

<div class="flex h-screen antialiased" class:dark={$mode === "dark"}>
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
    </nav>
    <div class="bg-primary dark:bg-secondary flex-[2] border-2 border-primary dark:border-none">
        
    </div>
    <main class="flex-col md:flex md:flex-[4] hidden {!selectedContact && "justify-center items-center"}">
        {#if selectedContact}
        <!-- chat header -->
            <div class="p-4 flex justify-between bg-primary text-white dark:bg-dark-secondary
            border-2 border-l-0 border-primary dark:border-dark-primary 
            ">
                <div class="flex flex-col">
                    <p class="font-medium text-dark-primary dark:text-primary">{selectedContact.name}</p>
                    <small class="text-sidebar">Last seen recently</small>
                </div>
                <div class="flex items-center gap-2">
                    <button class="hover:bg-hover dark:hover:bg-hover p-2 rounded-lg">
                        <Search size={20} class="text-sidebar"/>
                    </button>
                    <button class="hover:bg-hover dark:hover:bg-hover p-2 rounded-lg">
                        <Phone size={20} class="text-sidebar"/>
                    </button>
                    <button class="hover:bg-hover dark:hover:bg-hover p-2 rounded-lg">
                        <Info size={20} class="text-sidebar"/>
                    </button>
                    <button class="hover:bg-hover dark:hover:bg-hover p-2 rounded-lg">
                        <EllipsisVertical size={20} class="text-sidebar"/>
                    </button>
                </div>
            </div>
        <!-- chat body -->
            <div class="flex-1 items-center justify-center dark:bg-dark-secondary bg-primary">
                {#each selectedContact.messages as message}
                    <div>
                        {message.text}
                    </div>
                {/each}
            </div>
        <!-- chat footer -->
            <div class="dark:bg-dark-secondary bg-primary border-2 border-l-0 flex items-center p-2 pl-4 pr-4 border-primary dark:border-dark-primary">
                <Paperclip size={DEFAULT_ICON_SIZE-5} class="text-sidebar"/>
                <input class="border-none outline-none bg-transparent flex-1 focus:ring-0"  placeholder="Write a message..."/>
            </div>
        {:else}
            <div class="p-2 rounded-xl h-min items-center justify-center dark:bg-dark-primary bg-gray-500 text-white">
                Select a contact to start chatting
            </div>
        {/if}
    </main>
</div>

<button onclick={toggleMode} class:dark={$mode === "dark"} class="fixed bottom-5 right-5 p-2 bg-secondary text-white rounded-lg">
    Switch to {$mode === "dark" ? "Light" : "Dark"} Mode
</button>

<ModeWatcher/>