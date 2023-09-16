import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LoadingService } from '../loading.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {
  ID: number;
  
  Followed: boolean = false;

  IsSigned()
  {
    return window.localStorage.getItem('Email') != '' && window.localStorage.getItem('Email') != undefined;
  }

  Message()
  {
    this.Router.navigate(['/mc/' + this.ID]);
  }

  Follow()
  {
    this.http.get<boolean>(this.BaseURL + 'AddFollow?wantfollow=' + this.ID + '&email=' + window.localStorage.getItem('Email'))
    .subscribe(x => {this.CheckFollow();});
  }

  EditBio(Content: HTMLParagraphElement, Edit: HTMLButtonElement)
  {
    if (Content.getAttribute('contenteditable') == null)
    {
      Content.setAttribute('contenteditable', 'true');
      Edit.innerText = 'حفظ';
    }
    else
    {
      Content.removeAttribute('contenteditable');
      Edit.innerText = 'تعديل';

      this.http.get(this.BaseURL + 'Bio?New=' + Content.innerText + '&Email=' + window.localStorage.getItem('Email')).subscribe();
    }
  }

  IsOwner() : boolean
  {
    return this.Loading.Account.email == window.localStorage.getItem('Email');
  }

  CheckFollow()
  {
    this.http.get<boolean>(this.BaseURL + 'CheckFollow?wantfollow=' + this.ID + '&email=' + window.localStorage.getItem('Email'))
    .subscribe(x => this.Followed = x);
  }

  constructor(private Router: Router, public Loading: LoadingService, private route: ActivatedRoute, @Inject('BASE_URL') private BaseURL: string, private http: HttpClient)
  {
    this.ID = this.route.snapshot.params['id'];

    Loading.CurrentProf = this.ID;

    this.CheckFollow();
    
    setInterval(()=>
    {
      if (Loading.Account == undefined)
      {
        Loading.LoadProfile();
      }
      if (Loading.ProfileQuestions.length == 0)
      {
        Loading.LoadProfileQues(this.route.snapshot.params['id']);
      }
      if (Loading.ProfileComments.length == 0)
      {
        Loading.LoadProfileCom(this.route.snapshot.params['id']);
      }
    }, 500);
  }
}
