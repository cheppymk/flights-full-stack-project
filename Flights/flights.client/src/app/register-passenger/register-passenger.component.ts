import { Component, OnInit } from '@angular/core';
import { PassengerService } from './../api/services/passenger.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';
 

@Component({
  selector: 'app-register-passenger',
  templateUrl: './register-passenger.component.html',
  styleUrls: ['./register-passenger.component.css']
})
export class RegisterPassengerComponent implements OnInit {
  form: FormGroup;

  constructor(
    private passengerService: PassengerService,
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.form = this.fb.group({
      email: [''],
      firstName: [''],
      lastName: [''],
      isFemale: [true]  
    });
  }

  ngOnInit(): void { }

  checkPassenger(): void {
    const params = { email: this.form.get('email')?.value }

    this.passengerService
      .findPassenger(params)
      .subscribe(
        this.login, e => {
          if (e.status != 404)
            console.error(e)
        }
      )
  }

  register() {
    const transformedFormValue = this.transformFormValue(this.form.value);
    console.log("Transformed Form Values:", transformedFormValue);

    this.passengerService.registerPassenger({ body: transformedFormValue })
      .subscribe(
        _ =>this.authService.loginUser({email:this.form.get('email')?.value}),
        error => console.error("Error:", error)

      );
  }

  private login = () => {
    this.authService.loginUser({ email: this.form.get('email')?.value })
    this.router.navigate(['/search-flights'])
  }

  private transformFormValue(formValue: any): any {
    return {
      email: formValue.email,
      firstName: formValue.firstName,
      lastName: formValue.lastName,
      gender: formValue.isFemale ? 'Female' : 'Male' // Transform 'isFemale' to 'Gender'
    };
  }
}


