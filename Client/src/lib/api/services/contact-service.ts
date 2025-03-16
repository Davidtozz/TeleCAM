import { apiClient } from "$lib/api/client";
import { Contact } from "$lib/state/chat.svelte";
import type { NewContactDto } from "$lib/api/dtos/index.ts";
import { user } from "$lib/state/user.svelte";

/** 
 * Service for managing the user's contact list
*/
class ContactService {
    /** 
     * Fetches the user's contact list from API
     * @returns The user's contact list, or an empty array if the request fails
    */
    async getContacts(): Promise<Contact[]> {
        try {
            return await apiClient.GET<Contact[]>("/contacts/all");
        } catch(error) {
            console.error("Failed to get contacts: ", error);
            return [];
        }
    }

    /** 
     * Adds a new contact to the user's contact list
     * @param newContact The contact to add
     * @returns True if the contact was added successfully, false otherwise
    */
    async addContact(payload: NewContactDto): Promise<boolean> {
        try {
            await apiClient.POST("/contact/new", payload);
            const newContact = new Contact(payload.name)
            user.contacts.push(newContact);
            return true;
        } catch(error) {
            console.error("Failed to add contact: ", error);
            return false;
        }
    }
}

/**
 * Singleton instance of the ContactService
 */
export const contactService = new ContactService();