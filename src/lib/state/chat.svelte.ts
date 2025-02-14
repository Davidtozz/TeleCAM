import { fakerIT } from "@faker-js/faker";

export class Message {
    public text = $state<string>('');
    readonly sender: Contact | "me" = $state("me");
    readonly time: Date = new Date();
    constructor(text: string, sender: Contact | "me") {
        this.text = text;
        this.sender = sender;
    }

    get timeString() {
        return this.time.toTimeString();
    }

    get senderName() {
        
        if (this.sender === "me") {
            return "Me";
        }
        return this.sender.name;
    }

    static fromFakeData(sender: Contact | "me") {
        return new Message(fakerIT.lorem.sentence(), sender);
    }
}


export class Contact {
    public name = $state<string>('');
    public messages = $state<Message[]>([]);
    private status = $state<'online' | 'offline'>('offline');
    public selected: boolean = $state<boolean>(false);

    public addTextMessage(text: string, sentByMe: boolean) {
        if(sentByMe) {
            this.messages.push(new Message(text, "me"));
        } else {
            this.messages.push(new Message(text, this));
        }
    }       

    public addMessage(message: Message) {
        this.messages.push(message);
    }


    get lastMessage() {
        return this.messages[this.messages.length - 1];
    }

    static fromFakeData() {
        const contact = new Contact();
        contact.name = fakerIT.person.fullName();
        for (let i = 0; i < Math.random() * 10; i++) {
            contact.addMessage(Message.fromFakeData(contact));
        }
        return contact;
    }
}

export const contacts = $state<Contact[]>([]);