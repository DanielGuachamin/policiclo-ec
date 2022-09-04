import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthserviceService } from 'src/app/services/authservice.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  //variables correo y contraseña
  email: string='';
  password: string= '';
  public userForm: FormGroup;
  public user: any;
  
  constructor(private auth: AuthserviceService, 
    public formBuilder: FormBuilder, 
    private router : Router,) {
      this.userForm= this.formBuilder.group({
        
        mail: [''],
        password: [''],
        id: null
      })
     }

  ngOnInit(): void {
    //borrar el almacenamiento interno
    localStorage.clear();
    localStorage.setItem("recarga", "true");
  }
  async login(){
    //referencia al método del servicio para Iniciar sesión
    //verificación de algun error
    const res = await this.auth.login(this.userForm.value).catch(error =>{
      alert("Credenciales Incorrectas")
      console.log('error', error);
    })
   //en caso de que exista respuesta
    if(res && res.user.emailVerified){
        console.log("inicio exitoso");
        //asignación del id de Fireauthentication al id del formulario
        const id:string= res.user.uid;
        console.log("id user: ", id)
        //impresión por consola de los datos del usuario registrado
        console.log("datos usuario", res.user.email)
        localStorage.setItem('usuario',res.user.email)
        this.router.navigate(['/dashboard'])
      } else if(res){
        alert("No ha verificado la cuenta")
        this.router.navigate(['/sendEmail'])
      } else{
        this.router.navigate(['/register'])
      }
  }
  emptyFields(field: string){
    if (this.userForm.get(field)?.hasError('required')) {
      return 'El campo es obligatorio';
    }
   
    return this.userForm.get(field)? 'Formato incorrecto' : '';
  }
  
  get emptyPassword(){
    return this.userForm.get('password')?.invalid && this.userForm.get('password')?.touched
  }
  get emptyemail(){
    return this.userForm.get('mail')?.invalid && this.userForm.get('mail')?.touched
  }

}
