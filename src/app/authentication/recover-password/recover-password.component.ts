import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthserviceService } from 'src/app/services/authservice.service';

@Component({
  selector: 'app-recover-password',
  templateUrl: './recover-password.component.html',
  styleUrls: ['./recover-password.component.css']
})
export class RecoverPasswordComponent implements OnInit {

  //variables correo y contraseña
  email: string='';
  public recoverForm: FormGroup;
  public user: any;
  
  constructor(private auth: AuthserviceService, 
    public formBuilder: FormBuilder, 
    private router : Router,) {
      this.recoverForm= this.formBuilder.group({
        email: ['']
      })
    }

  ngOnInit(): void {
  }

  async recoverPassword(){
    const email = this.recoverForm.get('email').value;
    console.log(email);
    const res = await this.auth.forgotPassword(email)
    this.router.navigate(['/login'])
  }

  emptyFields(field: string){
    if (this.recoverForm.get(field)?.hasError('required')) {
      return 'El campo es obligatorio';
    }
   
    return this.recoverForm.get(field)? 'Formato incorrecto' : '';
  }

  get emptyemail(){
    return this.recoverForm.get('mail')?.invalid && this.recoverForm.get('mail')?.touched
  }
  

}
