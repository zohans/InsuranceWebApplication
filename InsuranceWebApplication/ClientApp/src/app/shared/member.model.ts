export class Member {
  id: number;
  firstName: string;
  lastName: string;
  age: string;
  dateofbirth: string;
  occupation: string;
  occupationId: string;
  deathinsuredsum: string;
  premium: string;

  constructor() {
    this.id = 0;
    this.firstName = "";
    this.lastName = "";
    this.age = "";
    this.dateofbirth = "";
    this.occupation = "";
    this.occupationId = ""
    this.deathinsuredsum = "";
    this.premium = "";
  }
}
