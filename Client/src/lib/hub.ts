import { HubConnectionBuilder, LogLevel, HttpTransportType } from '@microsoft/signalr';
import { user } from '$lib/state/user.svelte';

export const connection = new HubConnectionBuilder()
.withUrl("http://localhost:5180/chat", {
    skipNegotiation: true,
    transport: HttpTransportType.WebSockets,
    withCredentials: true,
})
.withAutomaticReconnect()
.configureLogging(LogLevel.Debug)
.build();

/* Hub events */
connection.on("Typing", (user: string) => {

})

connection.on("UserConnected", (connectionId: string) => {
    console.log("User connected: ", connectionId);
})

export async function init() {
    try {
        console.log(connection.state);
        await connection.start();
    } catch(err) {
        console.log(err)
    }
}


/* Client methods */
export function typing(toContact: string) {
    connection.invoke("Typing", toContact);
}


export function SetUsername(username: string) {
    connection.invoke("SetUsername", username);
}

export function SendMessage(toContact: string, message: string) {

    if(user.contacts.find(c => c.name === toContact) === undefined) {
        console.error("Contact not found");
        return;
    }

    connection.invoke("SendMessage", toContact, message);
}