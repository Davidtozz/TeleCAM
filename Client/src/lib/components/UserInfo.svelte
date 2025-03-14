<script lang="ts">
    import type { Contact } from "$lib/state/chat.svelte";
  import { X, EllipsisVertical, Phone } from "@lucide/svelte";

    let {selectedContact}: {selectedContact: Contact | null} = $props();
</script>


<div>
    <!-- hero section -->
    <section>
        <div class="flex items-center justify-between">
            <p>User Info</p>
            <div>
                <Phone size={20} />
                <EllipsisVertical size={20} />
                <X size={20} />
            </div>
        </div>
        <div></div>
    </section>
    <!-- contact info -->
    <section></section>
    <!-- media summary -->
    <section></section>
    <!-- actions -->
    <section></section>
</div>