import { HubConnectionBuilder, LogLevel, HttpTransportType } from '@microsoft/signalr';
import { contacts } from './state/chat.svelte';

export const connection = new HubConnectionBuilder()
.withUrl(`http://localhost:5180/chat`, {
    skipNegotiation: true,
    transport: HttpTransportType.WebSockets
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


/* Client methods */
export function typing(toContact: string) {
    connection.invoke("Typing", toContact);
}


export function SetUsername(username: string) {
    connection.invoke("SetUsername", username);
}

export function SendMessage(toContact: string, message: string) {

    if(contacts.find(c => c.name === toContact) === undefined) {
        console.error("Contact not found");
        return;
    }

    connection.invoke("SendMessage", toContact, message);
}