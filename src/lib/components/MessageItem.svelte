<script lang="ts">
  import type { Message } from "$lib/state/chat.svelte";

    let {message, isOwnMessage}: {
        message: Message,
        isOwnMessage: boolean
    } = $props();



    let editMode = $state(false);

</script>

<div class={`col-start-${isOwnMessage ? '6' : '1'} col-end-${isOwnMessage ? '13' : '8'} p-3 rounded-lg`}>
    <div class={`flex items-center ${isOwnMessage ? 'justify-start flex-row-reverse' : ''}`}>
        <div class="flex items-center justify-center h-10 w-10 rounded-full bg-indigo-500 flex-shrink-0">
            {message.sender === "me" ? "me" : message.sender.name.charAt(0).toUpperCase()}
        </div>
        <div class={`relative ${isOwnMessage ? 'mr-3' : 'ml-3'} text-sm ${isOwnMessage ? 'bg-indigo-100' : 'bg-white'} py-2 px-4 shadow rounded-xl`}>
            <div>{message.text}</div>
        </div>
    </div>
</div>