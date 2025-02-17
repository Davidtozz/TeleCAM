<script lang="ts">
  import { setContext } from "svelte";


    let { children, close, show }:
    { 
        children: any, 
        close: () => void, 
        show: boolean 
    } = $props();

    setContext('close', close);

</script>

{#if show}
    <div class="fixed inset-0 bg-gray-600 bg-opacity-50 flex items-center justify-center">
        <div class="bg-white rounded-lg shadow-lg p-6 w-1/3 relative">
            <button class="absolute top-2 right-2 text-gray-600" onclick={close}>✖</button>
            {@render children()}
        </div>
    </div>
{/if}

<style>
    .fixed {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        z-index: 50;
    }
</style>