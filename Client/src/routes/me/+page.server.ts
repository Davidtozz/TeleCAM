import type { Actions } from "./$types";


const API_URL = 'http://localhost:5180'

export const actions: Actions = {
    register: async ({request, fetch}) =>  {
        
        const data = await request.formData();
        const email = data.get('email');
        const password = data.get('password');

        const result = await fetch(`${API_URL}/user`, {
            method: 'POST',
            credentials: 'include',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({email, password})
        })

        if(result.ok) {
            return new Response('User created', {status: 201});
        }

        console.log("{"+email, password+"}");
    }
} 