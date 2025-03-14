<script lang="ts">
  import { setContext } from "svelte";
  import { modal } from "$lib/state/modal.svelte";
  import { X } from "@lucide/svelte";


    let { children}:
    { 
        children: any, 
    } = $props();


    // @ts-ignore
    let modalBg: HTMLDivElement = $state();
    // @ts-ignore
    let modalContent: HTMLDivElement = $state();

    function handleBackdropClick(e: MouseEvent) {
        if(modalBg && !modalContent.contains(e.target as Node)) {
            modal.close();
        }
    }

</script>

<svelte:body 
onclick={e => (e.target === modalBg) && modal.close()}
onkeydown={e => e.key === 'Escape' && modal.close()} />

{#if modal.state === "open"}
    <!-- svelte-ignore a11y_click_events_have_key_events -->
    <!-- svelte-ignore a11y_no_static_element_interactions -->
    <div 
        bind:this={modalBg}
        onclick={handleBackdropClick}
        class="fixed inset-0 bg-gray-900 bg-opacity-50 flex items-center justify-center">
        <div 
            bind:this={modalContent}
            class="bg-primary dark:bg-dark-primary rounded-lg shadow-lg p-6 w-1/3 relative">
            <button class="absolute top-2 right-2 text-gray-600" onclick={() => modal.close()}>
                <X size={24} />
            </button>
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