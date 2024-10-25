<script lang="ts">
    import { User } from 'lucide-svelte';
    let dateSent: Date = new Date();
    let delivered: boolean = false;
    let read: boolean = false;    
    let author: string = 'System';

    let avatar: Blob | null = null;
</script>




<div class="flex items-start gap-2.5">
    {#if avatar}
        <img src={URL.createObjectURL(avatar)} class="w-8 h-8 rounded-full" alt="Jese" />
    {:else}
        <User class="w-8 h-8 rounded-full" />
    {/if}
    <div class="flex flex-col w-full max-w-[320px] leading-1.5 p-4 border-gray-200 bg-gray-100 rounded-e-xl rounded-es-xl dark:bg-gray-700">
        <div class="flex items-center space-x-2 rtl:space-x-reverse">
            <span class="text-sm font-semibold text-gray-900 dark:text-white">{author}</span>
            <span class="text-sm font-normal text-gray-500 dark:text-gray-400">
                {dateSent.getHours()}:{dateSent.getMinutes()} {dateSent.getHours() >= 12 ? 'PM' : 'AM'}
            </span>
        </div>
        <p class="text-sm font-normal py-2.5 text-gray-900 dark:text-white">
            <slot />    
        </p>
        <span class="text-sm font-normal text-gray-500 dark:text-gray-400">Delivered</span>
    </div>
</div>