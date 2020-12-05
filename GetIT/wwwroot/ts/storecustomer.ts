export class StoreCustomer {

    constructor(private firstName: string, private lastName: string) {
        this.ourName = firstName + lastName;
    }
       
    public visits: number = 10;

    private ourName: string;

    public ShowName(name: string): boolean {
        alert(name);
        return true;
    }
}
