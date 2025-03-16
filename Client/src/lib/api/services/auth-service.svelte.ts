import { browser } from "$app/environment";
import { goto } from "$app/navigation";
import { apiClient } from "$lib/api/client";
import type { LoginDto, RegisterDto } from "$lib/api/dtos/index.ts";
import { redirect } from "@sveltejs/kit";

type UserState = "authenticated" | "unauthenticated";

/**
 * Service for managing the user's authentication state
 */
class AuthService {
    /** 
     * The current authentication state of the user.
     */
    public userState: UserState = $state("unauthenticated");

    public async login(credentials: LoginDto): Promise<void> {
        try {
            await apiClient.POST("/user/login", credentials);
            this.userState = "authenticated";
        } catch(error) {
            console.error("Login failed: ", error);
        }
    }
    
    async register(credentials: RegisterDto): Promise<void> {
        try {
            await apiClient.POST('/user/register', credentials);
            this.userState = "authenticated";
        } catch (error) {
            console.error('Registration failed:', error);
            throw error;
        }
    }

}

export const authService = new AuthService();