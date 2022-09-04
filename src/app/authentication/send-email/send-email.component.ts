import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthserviceService } from 'src/app/services/authservice.service';

@Component({
  selector: 'app-send-email',
  templateUrl: './send-email.component.html',
  styleUrls: ['./send-email.component.css'],
})
export class SendEmailComponent implements OnInit {

  user: string;
  constructor(private auth: AuthserviceService, private router: Router) { }

  ngOnInit(): void {
    this.user = localStorage.getItem('usuario');
  }

  onSendEmail(){
    this.auth.sendVerificationEmail();
    alert("Se envió un correo de verificación de cuenta");
    this.router.navigate(['/login'])
  }

}
