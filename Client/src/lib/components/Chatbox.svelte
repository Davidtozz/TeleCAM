<script lang="ts">
    import type { Contact, Message } from "$lib/state/chat.svelte";
    import MessageItem from "$lib/components/MessageItem.svelte";
    import { Search, Phone, Info, EllipsisVertical, Paperclip } from "@lucide/svelte";
    import Modal from "./Modal.svelte";
    import UserInfo from "./modals/UserInfoModal.svelte";
    import { modal } from "$lib/state/modal.svelte";
    import { onMount, tick } from "svelte";
    import { connection } from "$lib/hub";
    

    const { selectedContact }: {selectedContact: Contact | null} = $props();

    let currentMessage = $state('');

    function autoresize(node: HTMLTextAreaElement) {
        const resize =() => {
            node.style.height ='auto';
            node.style.height = node.scrollHeight + 'px';
        }

        tick().then(resize);

        node.addEventListener('input', resize);

        return {
            update: resize,
            destroy: () => node.removeEventListener('input', resize)
        }
    }

    function getMessagePosition(index: number): "single" | "first" | "middle" | "last" {
        if(!selectedContact) 
            throw Error("No selected contact", {
            cause: "getMessagePosition",
        });

        if(selectedContact?.messages.length === 1) 
            return "single";

        const curr = selectedContact.messages[index];
        const prev = selectedContact.messages[index - 1];
        const next = selectedContact.messages[index + 1];

        const sameAsPrev: boolean = prev?.sender === curr.sender;
        const sameAsNext: boolean = next?.sender === curr.sender;

        if(sameAsPrev && sameAsNext) return "middle";
        if(sameAsPrev) return "last";
        if(sameAsNext) return "first";
        return "single";
    }

    async function handleSubmit() {
        if(currentMessage.trim() === '') return;
        selectedContact?.addTextMessage(currentMessage, true);
        currentMessage = '';
        await tick()
    }

</script>

<main 
    class="flex-col md:flex md:flex-[4] hidden dark:bg-dark-primary bg-primary {!selectedContact && "justify-center items-center"}">
    {#if selectedContact}
    <!-- chat header -->
        <div 
        onclickcapture={() => modal.open()}
        class="p-4 flex justify-between bg-primary text-white dark:bg-dark-secondary
        border-2 border-l-0 border-primary dark:border-dark-primary cursor-pointer">
            <div class="flex flex-col">
                <p class="font-medium text-dark-primary dark:text-primary">
                    {selectedContact?.name}
                </p>
                <small class="text-sidebar">Last seen recently </small>
            </div>
            <div class="flex items-center gap-x-6">
                <button class="hover:text-hover dark:text-dark-hover rounded-lg">
                    <Search size={20} class="text-sidebar dark:hover:text-[#dcdcdc] hover:text-dark-hover"/>
                </button>
                <button class="hover:text-hover dark:text-dark-hover rounded-lg">
                    <Phone size={20} class="text-sidebar dark:hover:text-[#dcdcdc] hover:text-dark-hover"/>
                </button>
                <button class="hover:text-hover dark:text-dark-hover rounded-lg">
                    <Info size={20} class="text-sidebar dark:hover:text-[#dcdcdc] hover:text-dark-hover"/>
                </button>
                <button class="hover:text-hover dark:text-dark-hover rounded-lg">
                    <EllipsisVertical size={20} class="text-sidebar dark:hover:text-[#dcdcdc] hover:text-dark-hover"/>
                </button>
            </div>
        </div>
    <!-- chat body -->
        <div 
        style="background-image: url(https://picsum.photos/920/1200);"
        class="flex-1 flex flex-col-reverse max-h-full overflow-y-scroll dark:bg-dark-secondary bg-accent bg-no-repeat bg-cover telegram-scrollbar scrollbar-thin">
            {#each selectedContact.messages as message, i}
                <MessageItem {message} 
                    position={getMessagePosition(i)}
                    isOwnMessage={message.sender === "me"}/>
            {/each}
        </div>
    <!-- chat footer -->
        <div class="dark:bg-dark-secondary bg-primary border-2 border-l-0 flex items-center p-2 pl-4 pr-4 border-primary dark:border-dark-primary">
            <Paperclip size={25} class="text-sidebar"/>
            <textarea 
            use:autoresize
            maxlength="1000"
            onkeydown={async (e) => e.key === "Enter" && await handleSubmit()}
            class="border-none outline-none bg-transparent w-full focus:ring-0 max-h-[120px] 
                  overflow-y-auto resize-none text-left  dark:text-primary text-dark-primary px-3" 
            placeholder="Write a message..." bind:value={currentMessage}></textarea>
        </div>
    {:else}
        <div class="px-3 py-1 text-sm rounded-xl h-min items-center justify-center bg-dark-primary dark:bg-[#1e2c3a] text-white">
            Select a chat to start messaging
        </div>
    {/if}
</main>

<Modal>
    <UserInfo {selectedContact}/>
</Modal>