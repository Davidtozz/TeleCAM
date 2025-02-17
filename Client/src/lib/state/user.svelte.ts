import { SetUsername } from "$lib/hub";

export class User {
    name: string = $state('');
    /* email: string = $state(''); */
    constructor(name: string) {
        this.name = name;
        /* this.email = email; */
    }
}