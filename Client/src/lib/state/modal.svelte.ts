class Modal {

    public state = $state<"open" | "closed">("closed");

    public open() {
        this.state = "open";
    }

    public close() {
        this.state = "closed";
    }
}

export const modal = new Modal();