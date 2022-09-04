import { Component, OnInit, NgZone } from '@angular/core';
import { map } from 'rxjs';
import { User } from 'src/app/entities';
import { AuthserviceService } from 'src/app/services/authservice.service';
import { ServCiclisService } from 'src/app/services/serv-ciclis.service';
import { ciclista } from './ciclista.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
 public ciclistaUsuario:any;
  // variables
  ciclistas?:ciclista[]; 
  persona_ciclista?:ciclista;
  ciclista_Index=-1;
  //
  public collectionName="geolocation";
  public Users: User[];
  geo: any;
  constructor(
    private auth: AuthserviceService,
    private zone: NgZone,
    private servCiclismo :ServCiclisService) { }

  ngOnInit(): void {
  
    this.recibirDatos();
    this.ciclistaUsuario = localStorage.getItem('usuario');
    
  }

  logout(){
    
    this.auth.logout();
    
  }

  recibirDatos():void{
    this.servCiclismo.getAll().snapshotChanges().pipe(
      map(leer=>
        leer.map(c=>({
          id:c.payload.key ,...c.payload.val()
        }))
        )
    ).subscribe(data =>{
      this.ciclistas=data;
    })
     }



}
