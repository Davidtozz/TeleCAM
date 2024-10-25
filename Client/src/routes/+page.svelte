<script lang="ts">
    import { onMount } from 'svelte';
    import { writable } from 'svelte/store';
    import MessageBubble from '$lib/components/MessageBubble.svelte';

    export let author = 'System';
    
    let message = '';
    const messages = writable<string[]>([]);

    function sendMessage() {
        if (message.trim()) {
            messages.update((msgs) => [...msgs, message]);
            message = '';
        }
    }

    onMount(() => {
        // Simulate receiving messages
        setTimeout(() => {
            messages.update((msgs) => [...msgs, 'Welcome to the chat!']);
        }, 1000);
    });
</script>

<main class="h-screen flex flex-col bg-gray-100">
    <div class="flex-1 overflow-y-auto p-4">
        {#each $messages as msg, index}
            <MessageBubble>
                {msg}
            </MessageBubble>
        {/each}
    </div>
    <div class="border-t p-4 bg-white flex">
        <input
                type="text"
                bind:value={message}
                class="flex-1 p-2 border rounded-l-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
                placeholder="Type a message..."
                on:keydown={(e) => e.key === 'Enter' && sendMessage()}
        />
        <button
                on:click={sendMessage}
                class="p-2 bg-blue-500 text-white rounded-r-lg hover:bg-blue-600"
        >
            Send
        </button>
    </div>
</main>