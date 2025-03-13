<script lang="ts">
  import type { Message } from "$lib/state/chat.svelte";
  import { mode } from "mode-watcher";

    let {message, isOwnMessage, position}: {
        message: Message,
        position: "single" | "first" | "middle" | "last",
        isOwnMessage: boolean
    } = $props();

    function getRoundedStyle() {
        switch(position) {
            case "last":
                return "rounded-lg " + ((message.sender === "me") ? "rounded-br-none" : "rounded-tr-none");
            case "middle":
                return (message.sender === "me") ? "rounded-l-lg" : "rounded-r-lg";
            case "first":
                return "rounded-lg " + ((message.sender === "me") ? "rounded-tr-none" : "rounded-bl-none");
            default:
                return "rounded-lg";
        }
    }

    function getPaddingStyle() {
        switch(position) {
            case "last":
                return "pt-3";
            case "middle":
                return "";
            case "first":
                return "pb-3";
            default:
                return "py-3";
        }
    }

    const shadow = (() =>{
        if(position === "middle") return "";
        return "shadow-md"
    })(); 

    let editMode = $state(false);
    const showAvatar = position === "single" || position === "first";
</script>

<!-- TODO fix group styling to make it look more like Telegram's grouped messages -->
<div class={`col-start-${isOwnMessage ? '6' : '1'} col-end-${isOwnMessage ? '13' : '8'}  ${getPaddingStyle()}`}>
    <div class={`flex ${isOwnMessage ? 'justify-start flex-row-reverse' : ''}`}>
        {#if showAvatar}
            <div class="flex items-center justify-center h-10 w-10 rounded-full bg-indigo-500 flex-shrink-0">
                {message.senderName[0].toUpperCase()}
            </div>
        {:else}
            <!-- Placeholder for correct alignment -->
            <div class="w-10 flex-shrink-0"></div>
        {/if}
        <div class={`relative ${isOwnMessage ? 'mr-3' : 'ml-3'} text-sm ${isOwnMessage ? 'bg-[#2b5278]' : `bg-[#182533]`} py-2 px-4 ${shadow} ${getRoundedStyle()}`}>
            <div>{message.text}</div>
        </div>
    </div>
</div>