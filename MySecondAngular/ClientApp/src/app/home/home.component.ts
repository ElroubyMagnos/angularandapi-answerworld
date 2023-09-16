import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoadingService } from '../loading.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent 
{
  constructor(public Loading: LoadingService,private route: Router)
  {
    setInterval(()=>
    {
      if (this.Loading.Questions.length == 0 
        || this.Loading.Accounts.length == 0 
        || this.Loading.TopAnswers.length == 0)
      {
        this.Loading.LoadHome();
      }
    }, 777);
  }  
}