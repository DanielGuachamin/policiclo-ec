import { Injectable } from '@angular/core';
import { Auth, authState, createUserWithEmailAndPassword, signInWithEmailAndPassword, updateProfile } from '@angular/fire/auth';
import { AngularFireDatabase, AngularFireList } from '@angular/fire/compat/database';
import { from, switchMap } from 'rxjs';
import { ciclista } from '../Model/ciclista.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  currentUser$ = authState(this.auth);
  private dbciclistas = "/CiclistaModel";

 misciclistas : AngularFireList<ciclista>;
  constructor(private auth: Auth,private db :AngularFireDatabase) { 
    this.misciclistas=db.list(this.dbciclistas);
  }

  login(username: string, password: string){
    return from(signInWithEmailAndPassword(this.auth, username, password));
  }

  signUp(name: string, email: string, password: string){
    return from(createUserWithEmailAndPassword(this.auth, email, password)).pipe
    (switchMap(({user}) => updateProfile(user, {displayName: name})))
  }

  logout(){
    return from(this.auth.signOut());
  }

  // read information of  firebase (Realtime)
  
  getAll():AngularFireList<ciclista>{
    return  this.misciclistas;
      }


}
