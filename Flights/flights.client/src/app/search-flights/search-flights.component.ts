import { Time } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FlightService } from './../api/services/flight.service';
import { FlightRm } from '../api/models';

@Component({
  selector: 'app-search-flights',
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css']
})
export class SearchFlightsComponent implements OnInit {

  searchResult: FlightRm[] = [];

  constructor(private flightService: FlightService) { }

  ngOnInit(): void {
  }

  search(): void {

    this.flightService.flightGet({}).subscribe(
      response => {
        this.searchResult = response;
      },
      error => {
        this.handleError(error);
      }
    );
  }

  private handleError(error: any): void {
    console.error(error);
  }
}
