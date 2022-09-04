import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
//Importa Servicio
import { AuthserviceService } from 'src/app/services/authservice.service';
//importación de formulario reactivo
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  //variables correo y contraseña
  email: string= '';
  password: string= '';
  //formulario de usuario
  public userForm: FormGroup;
  geo: any;
  user: any;
  constructor(
    private auth: AuthserviceService,
    private router: Router,
    public formBuilder: FormBuilder) {
      //esecificación de los campos de usuario
    this.userForm= this.formBuilder.group({
      mail: ['', Validators.required],
      password: new FormControl(this.password, [ Validators.required, Validators.minLength(6)]),
    
    })
     }

  ngOnInit(): void {
  
  }
  async register(){
    
    //referencia al método del servicio para Registrar
    //verificación de algun error
    const res = await this.auth.register(this.userForm.value).catch(error =>{
      console.log("los datos del usuario son. ", this.userForm);
      alert("Registro incompleto")
      console.log('error', error);
    })
    //en caso de que exista respuesta
    if(res){
      console.log("Registro en FireAuthentication completo");
      alert("Registro con exito");
      //asignación del id de Fireauthentication al id del formulario
      const id= res.user.uid;
      this.userForm.patchValue({id: id});
      //impresión por consola de los datos del usuario registrado
      console.log("datos usuario", this.userForm.value)
      //redireccionar a la vista principal
      this.auth.sendVerificationEmail();
      this.router.navigate(['/sendEmail'])
    }
  }
  async delay(n: number){
    return new Promise(function(resolve){
        setTimeout(resolve,n*1000);
    });
  }
  async  myAsyncFunction(){
    //Do what you want here 
    console.log("por fa espera")

    await this.delay(2);
    this.router.navigate(['/dashboard'])
  }
 

 
  emptyFields(field: string){
    if (this.userForm.get(field)?.hasError('required')) {
      return 'El campo es obligatorio';
    }
   
    return this.userForm.get(field)? 'Formato incorrecto' : '';
  }
  emptypassword(field: string){
    if (this.userForm.get(field)?.hasError('required')) {
      return 'El campo es obligatorio';
    }
    return this.userForm.get(field)? 'La contraseña debe tener mínimo 6 caracteres' : '';
  }

  get Password(){
    return this.userForm.get('password')?.invalid && this.userForm.get('password')?.touched
  }
  get Email(){
    return this.userForm.get('mail')?.invalid && this.userForm.get('mail')?.touched
  }

}
