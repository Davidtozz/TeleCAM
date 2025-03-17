let faker: any;
let fakerIT: any;

if(import.meta.env.DEV) {
    const importedFaker = await import('@faker-js/faker');
    faker = importedFaker.faker;
    fakerIT = importedFaker.fakerIT;
}

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
        if(import.meta.env.PROD || !fakerIT) {   
            return new Message(crypto.randomUUID(), sender);
        }
        return new Message(fakerIT.lorem.sentence(), sender);
    }
}

export type Status = 'online' | 'offline' | 'away';

export class Contact {
    public name = $state<string>('');
    public messages = $state<Message[]>([]);
    public status = $state<Status>('offline');
    public typing = $state<boolean>(false);

    constructor(name: string) {
        this.name = name;
    }

    public addTextMessage(text: string, sentByMe: boolean) {
        if(sentByMe) {
            this.messages.unshift(new Message(text, "me"));
        } else {
            this.messages.unshift(new Message(text, this));
        }
    }       

    public addMessage(message: Message) {
        this.messages.push(message);
    }


    get lastMessage() {
        return this.messages[this.messages.length - 1];
    }

    get initialToUpper() {
        return this.name[0].toUpperCase();
    }

    static fromFakeData() {
        if(import.meta.env.PROD || !fakerIT || !faker) {
            const contact = new Contact("Sample contact");
            contact.status = "online";
            contact.addMessage(new Message("Hello", contact));
            return contact
        } else {
            const name = fakerIT.person.fullName();
            const contact = new Contact(name);
            contact.status = faker.helpers.arrayElement(['online', 'offline', 'away']);
            for (let i = 0; i < Math.random() * 10; i++) {
                contact.addMessage(Message.fromFakeData(contact));
            }
            return contact;
        }
    }
}