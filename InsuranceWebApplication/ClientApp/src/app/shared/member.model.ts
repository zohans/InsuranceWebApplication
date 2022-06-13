export class Member {
  id: number;
  firstName: string;
  lastName: string;
  age: string;
  dateOfBirth: string;
  occupation: string;
  occupationId: string;
  deathInsuredSum: string;
  premium: string;

  constructor() {
    this.id = 0;
    this.firstName = "";
    this.lastName = "";
    this.age = "";
    this.dateOfBirth = "";
    this.occupation = "";
    this.occupationId = ""
    this.deathInsuredSum = "";
    this.premium = "";
  }
}
