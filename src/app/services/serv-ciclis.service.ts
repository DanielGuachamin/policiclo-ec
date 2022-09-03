import { Injectable } from '@angular/core';
import { AngularFireDatabase, AngularFireList } from '@angular/fire/compat/database';
import { ciclista } from '../components/dashboard/ciclista.model';
@Injectable({
  providedIn: 'root'
})
export class ServCiclisService {
  private dbciclistas = "/CiclistaModel";
  misciclistas : AngularFireList<ciclista>;
  constructor(private db :AngularFireDatabase) { 
    this.misciclistas=db.list(this.dbciclistas);
  }
  getAll():AngularFireList<ciclista>{
    return  this.misciclistas;
      }
}
