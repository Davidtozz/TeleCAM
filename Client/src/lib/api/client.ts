import { goto } from "$app/navigation";

const API_BASE_URL = import.meta.env.DEV ? "http://localhost:5180" : "INSERT_PROD_URL_HERE";

/**
 * Minimalistic wrapper around the Fetch API for making HTTP requests to Telecam's API
 * @see https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API
 */
class ApiClient {
    public async GET<T>(endpoint: string): Promise<T> {
        return this.request<T>(endpoint, {method: "GET"});
    }

    public async POST<T>(endpoint: string, data?: any): Promise<T> {
        return this.request<T>(endpoint, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        });
    }

    public async PUT<T>(endpoint: string, data?: any): Promise<T> {
        return this.request<T>(endpoint, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        });
    }

    public async DELETE<T>(endpoint: string): Promise<T> {
        return this.request<T>(endpoint, {method: "DELETE"});
    }

    /**
     * Sends an HTTP request to the API
     * @param endpoint The API endpoint to send the request to
     * @param options The request options
     * @returns The response data, or an empty object if the response is not JSON
     * @throws If the request fails or the response status is not OK
     */
    private async request<T>(endpoint: string, options: RequestInit): Promise<T> {
        const headers= {
            ...options.headers
        }

        const config = {
            ...options,
            headers,
            credentials: "include" as RequestCredentials
        }

        try {
            const response = await fetch(`${API_BASE_URL}${endpoint}`, config);
            if(response.status === 401) {
                await goto("/auth");    
                console.warn("Unauthorized, redirecting to login page");
            }

            if(!response.ok) {
                throw new Error(`API error: ${response.status} ${response.statusText}`);
            }

            const contentType = response.headers.get("content-type");
            if(contentType && contentType.includes("application/json")) {
                return await response.json();
            }

            return {} as T;
        } catch(error) {
            console.error(`API request failed: ${endpoint}`, error);
            throw error;
        }

    }
}
/**
 * Singleton instance of Telecam's API client
 */
export const apiClient = new ApiClient();