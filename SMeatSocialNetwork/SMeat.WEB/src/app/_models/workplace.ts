export class WorkPlace {
  id: string;
  position: string;
  locationId: string;
  location: Location;
  companyName: string;

  constructor(id?: string, position?: string, locationId?: string, location?: Location, companyName?: string) {
    this.id = id;
    this.position = position;
    this.locationId = locationId;
    this.location = location;
    this.companyName = companyName;
  }
}
