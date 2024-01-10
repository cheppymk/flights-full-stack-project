import { Component, OnInit } from '@angular/core';
import { PassengerService } from './../api/services/passenger.service';
import {FormBuilder} from '@angular/forms'; 

@Component({
  selector: 'app-register-passenger',
  templateUrl: './register-passenger.component.html',
  styleUrl: './register-passenger.component.css'
})
export class RegisterPassengerComponent implements OnInit{

  constructor(private passengerService: PassengerService,
    private fb: FormBuilder) { }

  form = this.fb.group({
    email: [''],
    firstName: [''],
    lastName: [''],
    isFemale: [true]
  })

  ngOnInit(): void {
  }

  register() {
    console.log("Form Values:", this.form.value);

    this.passengerService.registerPassenger()
      
  }


}