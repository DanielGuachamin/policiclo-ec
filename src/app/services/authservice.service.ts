import { Injectable } from '@angular/core';
//Importa biblioteca de Firebase Authentication
import { AngularFireAuth} from '@angular/fire/compat/auth';
import { AngularFirestore } from '@angular/fire/compat/firestore';
//Importa biblioteca de Router
import { Router } from '@angular/router';
import { User } from '../entities';


@Injectable({
  providedIn: 'root'
})
export class AuthserviceService {

  constructor(
    private fireauth: AngularFireAuth, 
    private router: Router, 
    private firestore: AngularFirestore,
  ) { }


  getObject( id: string, path: string){
    
    //obtiene playlist segun el id
    return this.firestore//accede a Firestore
    .collection(path)//especifica la colección
    .doc(id)//especifica el documento
    .valueChanges()//obtiene el documento
  }
  //login method
  //Método para Acceder al sistema
  //Necesita parámetro: objeto User
  login(user: any) {
    //método de firebase para acceder al sistema mediante el correo y contraseña
    return this.fireauth.signInWithEmailAndPassword(user.mail,user.password);
    
  }

  //register method
  //Función para Registrarse en el sistema
  //Necesita parámetros: objeto usuario
  register(user: User) {
    //método de firebase para registrarse en el sistema mediante el correo y contraseña
    
    console.log("user value: ", user)
    //método de firebase para registrarse en el sistema mediante el correo y contraseña
    return this.fireauth.createUserWithEmailAndPassword(user.mail, user.password)
  }





 
  logout(){
    //método de firebase para salir del sistema
    this.fireauth.signOut().then(()=>{
      //deshabilitar el token
      localStorage.clear();
      //redieccionar al Inicio de sesión
      this.router.navigate(['/login']);
    }, err=>{
      alert(err.message);

    })
  }

  
  forgotPassword(email : string) {
    //método de firebase para reestablecer contraseña mediante correo
    this.fireauth.sendPasswordResetEmail(email).then(() => {
      //redireccionar al dashboard
      alert("Revisa tu correo");
      
    }, err => {
      console.log("error: ", err)
      alert('Something went wrong');
    })
  }
 
  showUsers(){
    //dirección de las playlists
    const path =  "users";
    //retorna los documentos de la colección segun la dirección
    return this.firestore.collection(path)
    .snapshotChanges();//obtiene los documentos
  }
}
