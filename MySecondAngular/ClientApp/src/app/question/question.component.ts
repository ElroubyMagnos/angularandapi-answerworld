import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { LoadingService } from '../loading.service';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css']
})
export class QuestionComponent {
  UVote: boolean = false;
  DVote: boolean = false;

  UpAnswer(id: number, UP: HTMLImageElement, Down: HTMLImageElement, Score: any)
  {
    if (!this.SignedIn())
    {
      alert('يجب عليك تسجيل الدخول اولاً');
    }

    this.http.get<number>(this.BaseURL + 'VottingAns?UP=1' + '&id=' + id + '&email=' + window.localStorage.getItem('Email')).subscribe(
      x => {
        Score.innerText = x;
        this.UPAns(id, UP, Down);
      });
  }

  
  DownAnswer(id: number, UP: HTMLImageElement, Down: HTMLImageElement, Score: any)
  {
    if (!this.SignedIn())
    {
      alert('يجب عليك تسجيل الدخول اولاً');
    }

    this.http.get<number>(this.BaseURL + 'VottingAns?Down=1' + '&id=' + id + '&email=' + window.localStorage.getItem('Email')).subscribe(
      x => 
      {
        Score.innerText = x;
        this.DownAns(id, UP, Down);
      });
  }

  UPAns(id: number, UP: HTMLImageElement, Down: HTMLImageElement)
  {
    this.http.get<boolean>(this.BaseURL + 'AnsVote?getupans=1' + '&id=' + id + '&email=' + window.localStorage.getItem('Email'))
    .subscribe(
      x=>
      {
        UP.src = x ? 'assets/Images/arrow-up-clicked.png' : 'assets/Images/arrow-up.png';
        this.DownAns(id, UP, Down);
      });
  }

  DownAns(id: number, UP: HTMLImageElement, Down: HTMLImageElement)
  {
    this.http.get<boolean>(this.BaseURL + 'AnsVote?getdownans=1' + '&id=' + id + '&email=' + window.localStorage.getItem('Email'))
    .subscribe(
      x=>
      {
        Down.src = x ? 'assets/Images/arrow-down-clicked.png' : 'assets/Images/arrow-down.png';
        this.UPAns(id, UP, Down);
      });
  }

  UpVoted()
  {
    this.http.get<boolean>(this.BaseURL + 'QuesVote?getupques=1' + '&id=' + this.ID + '&email=' + window.localStorage.getItem('Email'))
    .subscribe(
      x=>
      {
        this.UVote = x;

        this.DVote = false;
      });
  }

  DownVoted()
  {
    this.http.get<boolean>(this.BaseURL + 'QuesVote?getdownques=1' + '&id=' + this.ID + '&email=' + window.localStorage.getItem('Email'))
    .subscribe(
      x=>
      {
        this.DVote = x;

        this.UVote = false;
      });
  }

  UpQues()
  {
    if (!this.SignedIn())
    {
      alert('يجب عليك تسجيل الدخول اولاً');
    }

    this.http.get<number>(this.BaseURL + 'VottingQues?UP=1' + '&id=' + this.ID + '&email=' + window.localStorage.getItem('Email')).subscribe(
      x => {
        this.Loading.QuesVote = x;
        this.UpVoted();
      });
  }

  
  DownQues()
  {
    if (!this.SignedIn())
    {
      alert('يجب عليك تسجيل الدخول اولاً');
    }

    this.http.get<number>(this.BaseURL + 'VottingQues?Down=1' + '&id=' + this.ID + '&email=' + window.localStorage.getItem('Email')).subscribe(
      x => 
      {
        this.Loading.QuesVote = x;
        this.DownVoted();
      });
  }

  SignedIn() : boolean
  {
    return window.localStorage.getItem('Email') != null && window.localStorage.getItem('Email') != '';
  }
  
  ID: string;
  constructor(private AcRout: ActivatedRoute, public Loading: LoadingService, private Route: Router, private route: ActivatedRoute, @Inject('BASE_URL') private BaseURL: string, private http: HttpClient)
  {
    this.route.params.subscribe(x => {
      this.Loading.Comments = [];
      this.Loading.RandQues = [];
      this.Loading.Question = null;
    });
    setInterval(()=>
    {
      if (this.ID != this.route.snapshot.params['id'])
          this.ID = this.Loading.CurrentQues = this.route.snapshot.params['id'];

      if (Loading.CurrentQues != 0)
      {
        if (this.Loading.Comments == undefined || this.Loading.Comments.length == 0)
        {
          this.Loading.LoadComments();
        }
        if (this.Loading.RandQues == undefined || this.Loading.RandQues.length == 0)
        {
          this.Loading.LoadRandomQues();
        }
        if (this.Loading.Question == undefined || this.Loading.Question == null)
        {
          this.Loading.LoadQuestion();
        }

        this.VoteLoad();
      }
    }, 777);
  }

  VoteLoad()
  {
    this.UpVoted();
    this.DownVoted();
  }

  PostAComment(commentta: HTMLTextAreaElement)
  {
    this.http.get(this.BaseURL + 'AddComment?Text=' + commentta.value + '&QuesID=' + this.ID + '&Email=' + window.localStorage.getItem('Email'))
    .subscribe();

    commentta.value = '';

    alert('تم نشر التعليق');

    this.Loading.LoadComments();
  }
}


interface Question
{
    id: number;
    title: string;
    content: string;
    category: string;
    views: string;
    ownerName: string;
    scoreUP: string;
    scoreDown: string;
}