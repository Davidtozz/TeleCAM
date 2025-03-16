<script lang="ts">

    import {enhance} from '$app/forms';
  import { goto } from '$app/navigation';
    import { authService } from '$lib/api/services/auth-service.svelte';
  import { mode, toggleMode } from 'mode-watcher';

    let username: string = $state('');
    let email: string = $state('');
    let password: string = $state('');

    let formType: 'register' | 'login' = $state('register');

    async function handleSubmit() {

        if(formType === 'register') {
            await authService.register({username, email, password});
        } else {
            await authService.login({username, password});
        }
        console.warn("Redirecting...")
        await goto("/")
    }

    function toggleform() {
        formType = formType === 'register' ? 'login' : 'register';
    }
</script>

<main class="h-screen flex flex-col items-center justify-center bg-primary dark:bg-dark-primary">
    <div class="block max-w-sm p-6 border border-gray-200 shadow-md rounded-lg  dark:bg-gray-800 dark:border-gray-700 ">
        <form id={formType + "Form"} class="max-w-sm mx-auto">
            <fieldset>
                <label for="name" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Your name</label>
                <input 
                bind:value={username}
                name="name"
                type="text" id="name" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="John Doe" required />
            </fieldset>
            {#if formType === 'register'}
            <fieldset class="mb-5">
                <label for="email" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Your email</label>
                <input 
                bind:value={email}
                name="email"
                type="email" id="email" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="name@flowbite.com" required />
            </fieldset>
            {/if}

            <fieldset class="mb-5">
                <label for="password" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Your password</label>
                <input 
                    bind:value={password}
                    type="password" id="password" name="password" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" required />
            </fieldset>
            <div>
                <button 
                    onclick={handleSubmit}
                    class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">
                    Submit
                </button>
            </div>
            
        </form>
    </div>
    <button
        onclick={toggleform}
        class="text-blue-700 hover:underline dark:text-blue-400">
        Switch to {formType === 'register' ? 'Login' : 'Register'}
    </button>
</main>
