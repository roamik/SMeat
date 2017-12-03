export class Location {
  id: string;
  city: string;
  street: string;
  country: string;

  constructor(id?: string, city?: string, street?: string, country?: string) {
    this.id = id;
    this.city = city;
    this.street = street;
    this.country = country;
  }
}
