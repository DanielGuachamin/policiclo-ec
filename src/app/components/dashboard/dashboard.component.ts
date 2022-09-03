import { Component, OnInit } from '@angular/core';
import { AngularFireDatabase, AngularFireList } from '@angular/fire/compat/database';
import { from, map } from 'rxjs';
import { ciclista } from 'src/app/Model/ciclista.model';
import { UserService } from 'src/app/services/user.service';



@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  user$ = this.authService.currentUser$;
// variables
ciclistas?:ciclista[]; 
persona_ciclista?:ciclista;
ciclista_Index=-1;


  constructor(private authService: UserService) {
    
   }

  ngOnInit(): void {
    this.recibirDatos();
  }

 recibirDatos():void{
this.authService.getAll().snapshotChanges().pipe(
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
