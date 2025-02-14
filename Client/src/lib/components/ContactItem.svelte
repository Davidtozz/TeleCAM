<script lang="ts">
  import { Contact } from "$lib/state/chat.svelte";


  let { contact }: {contact: Contact} = $props();

</script>

<button 
  class="flex flex-1 flex-row items-center hover:bg-gray-100 rounded-xl p-2 gap-x-2">
  <div class={`flex items-center justify-center h-8 w-8 bg-200-white rounded-full`}>
    {contact.name.charAt(0).toUpperCase()}
  </div>
  <div class=" text-sm font-semibold">{contact.name}</div>
<!--   {#if unread}
    <div class="flex items-center justify-center ml-auto text-xs text-white bg-red-500 h-4 w-4 rounded leading-none">
      {unread}
    </div>
  {/if} -->
</button>