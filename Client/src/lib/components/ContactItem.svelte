<script lang="ts">
  import { Contact } from "$lib/state/chat.svelte";


  let { contact, onclick }: {contact: Contact, onclick: () => void} = $props();

</script>

<button 
 onclick={onclick}
  class="flex flex-1 flex-row items-center hover:bg-hover hoverdark:bg-dark-hover p-2 gap-x-2">
  <div class={`flex items-center justify-center h-8 w-8 bg-blue-500 rounded-full`}>
    {contact.name.charAt(0).toUpperCase()}
  </div>
  <div class=" text-sm font-semibold">{contact.name}</div>
<!--   {#if unread}
    <div class="flex items-center justify-center ml-auto text-xs text-white bg-red-500 h-4 w-4 rounded leading-none">
      {unread}
    </div>
  {/if} -->
</button>