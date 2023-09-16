import { HttpClient } from '@angular/common/http';
import { Inject, Injectable, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoadingService  {

  TopAnswers: Comment[] = [];
  Questions: Question[] = [];
  Comments: Comment[] = [];
  ProfileComments: Comment[] = [];
  Accounts: Account[] = [];
  CurrentQues: any = 0;
  CurrentProf: any = 0;
  Question: Question | null;
  QuesVote: number;
  RandQues: Question[];
  TopQuestions: Question[];
  ProfileQuestions: Question[] = [];
  Account: Account;
  Messages: Message[] = [];

  constructor(private Route: Router, @Inject('BASE_URL') private BaseURL: string, private http: HttpClient)
  {
    
  }

  LoadProfileCom(ID: string)
  {
    this.http.get<Comment[]>(this.BaseURL + 'GetPComments?id=' + ID).subscribe(xy => 
      {
        this.ProfileComments = xy;
      });
  }

  LoadProfileQues(ID: string)
  {
    this.http.get<Question[]>(this.BaseURL + 'ProfileQuestions?proid=' + ID).subscribe(xy => 
      {
        this.ProfileQuestions = xy;
      });
  }

  LoadPMessages(recid: string)
  {
    this.http.get<Message[]>(this.BaseURL + 'GetPMessages?email=' + window.localStorage.getItem('Email') + '&recid=' + recid)
    .subscribe(x => this.Messages = x);
  }

  LoadProfile()
  {
    if (this.CurrentProf == 0)
        return;
    this.http.get<Account[]>(this.BaseURL + 'Account?id=' + this.CurrentProf).subscribe(x => this.Account = x[0]);
  }

  LoadTopQuestions()
  {
    this.http.get<Question[]>(this.BaseURL + 'GetQuestions')
    .subscribe(x => this.TopQuestions = x);
  }

  LoadRandomQues()
  {
    this.http.get<Question[]>(this.BaseURL + 'RandomQues').subscribe(xy => 
      {
        this.RandQues = xy;
      });
  }

  LoadComments()
  {
    if (this.CurrentQues == 0)
        return;
    this.http.get<Comment[]>(this.BaseURL + 'GetComments?id=' + this.CurrentQues).subscribe(com => 
      {
        this.Comments = com;
      });
  }

  LoadQuestion()
  {
    if (this.CurrentQues == 0)
        return;
    this.http.get<Question>(this.BaseURL + 'GetOneQues?id=' + this.CurrentQues).subscribe(ques => 
      {
        this.Question = ques;
        this.QuesVote = (this.Question.scoreUP.split(',').length - 1) - (this.Question.scoreDown.split(',').length - 1);
      });
  }

  LoadHome()
  {
    this.http.get<Question[]>(this.BaseURL + 'GetQuestions')
      .subscribe(x =>
      {
        this.Questions = x;

        this.http.get<Account[]>(this.BaseURL + 'Account')
          .subscribe(x =>
            {
              this.Accounts = x
              this.http.get<Comment[]>(this.BaseURL + 'TopAnswers').subscribe(xy => 
                {
                  this.TopAnswers = xy;
                });
            }
          );
      });
  }
}

interface Account
{
   id: number;
   email: string;
   password: string;
   score: number; 
   viewsData: string;
   emailActiveCode: string
   banned: number;
   admin: number;
   username: string;
   pictureURL: string;
   bio: string;
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

interface Comment
{
  id: number;
  text: string;
  ownerID: string;
  ownerName: string;
  views: string;
  scoreUP: string;
  scoreDown: string;
  question: number;
  piPath: string;
}

interface Account
{
   id: number;
   email: string;
   password: string;
   score: number; 
   viewsData: string;
   emailActiveCode: string
   banned: number;
   admin: number;
   username: string;
   pictureURL: string;
   bio: string;
   coverURL: string;
}

interface Message
{
  id: number;
  messageSent: string;
  sender: string;
  receiver: string;
}