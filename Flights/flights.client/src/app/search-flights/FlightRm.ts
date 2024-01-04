import { TimePlaceRm } from './search-flights.component';


export interface FlightRm {
    airline: string;
    arrival: TimePlaceRm;
    departure: TimePlaceRm;
    price: number;
    remainingNumberOfSeats: number;
}
