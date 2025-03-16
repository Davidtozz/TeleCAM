import type { Contact } from "$lib/state/chat.svelte";
import type { LoginDto, RegisterDto } from "../dtos";

type UserState = "authenticated" | "unauthenticated";
const API_BASE_URL = "http://localhost:5180";


//FIXME: cors issue
class UserService {
    /** 
     * The current authentication state of the user.
     */
    public userState: UserState = $state("unauthenticated");

    public async login(credentials: LoginDto): Promise<void> {
        await this.sendAuthRequest("/user/login", credentials);
        this.userState = "authenticated";
    }
    
    public async register(credentials: RegisterDto): Promise<void> {
        await this.sendAuthRequest("/user/register", credentials);
        this.userState = "authenticated";
    }

    public async fetchContacts(): Promise<Contact[]> {
        throw new Error("Not implemented", {
            cause: "UserService.fetchContacts"
        });
    }

    /**
     * Helper method to send authentication requests
     */
    private async sendAuthRequest(endpoint: string, payload: object): Promise<Response> {
        const response: Response = await fetch(`${API_BASE_URL}${endpoint}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            credentials: "include",
            body: JSON.stringify(payload)
        });

        if(response.status !== 200) {
            throw new Error(response.statusText);
        }
        
        return response;
    }
}



export const userService = new UserService();