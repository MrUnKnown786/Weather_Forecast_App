import { Component } from '@angular/core';
import { SkyInputBoxModule } from '@skyux/forms';
import { Router } from '@angular/router';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  ValidationErrors,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  standalone: true,
  imports: [ SkyInputBoxModule, FormsModule, ReactiveFormsModule ]
})
export class HomeComponent {

  constructor(private router: Router){

  }

  Form = new FormGroup({
    city: new FormControl('', [Validators.required, Validators.minLength(2)])
  });

  onSubmit(){
    this.router.navigate(['/search', this.Form.value.city])
  }

}
