import { HttpClient } from '@angular/common/http';
import { Component, Inject, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

interface Account
{
  id: number;
  username: string;
  picpath: string;
  email: string;
}

@Component({
  selector: 'app-head',
  templateUrl: './head.component.html',
  styleUrls: ['./head.component.css']
})
export class HeadComponent {
  SignInWindow: any;
  CurrentLogin: Account;
  
  SignOut() : void
  {
    window.localStorage.setItem('Email', '');
  }

  CheckSigned() : boolean
  {
    return window.localStorage.getItem('Email') == null || window.localStorage.getItem('Email') == '';
  }

  SignIn(email: any, password: any)
  {
    this.http.get<boolean>(this.baseurl + 'Check?Email=' + email.value + '&Password=' + password.value)
    .subscribe(signed => {
      if (signed)
      {
        window.localStorage.setItem('Email', email.value);
        alert('تم تسجيل الدخول بنجاح');
        this.route.navigate(['/']);
        email.value = password.value = '';

        this.SignInWindow = document.getElementById('SignIn');
        this.SignInWindow.style.display = 'none';
        
      }
      else
      {
        alert('البريد الالكتروني او كلمة السر خاطئين');
      }
    });
  }

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseurl:string, private route: Router)
  {
    this.http.get<Account[]>(this.baseurl + 'SignedIn?email=' + window.localStorage.getItem('Email'))
    .subscribe(x => this.CurrentLogin = x[0]);
  }

  public checkusername: boolean = true;
  public checkemail: boolean = true;
  public checkpass: boolean = true;
  public checkpassequal: boolean = true;

  AddNewAccount(username: any, email: any, password: any, cpassword: any, sub: any)
  {
    if (this.checkusername || this.checkemail || this.checkpass || this.checkpassequal)
    {
      alert('يرجي معالجة البيانات بالطريقة الصحيحة')
      return;
    }
    else
    {
      sub.setAttribute('disabled', '');
      this.http.get<boolean>(this.baseurl + 'Check?Email=' + email.value)
      .subscribe(checkemailexist => {
        if (checkemailexist)
        {
          alert('الايميل موجود بالفعل');
        }
        else
        {
          this.http.get<boolean>(this.baseurl + 'Check?Username=' + username.value)
          .subscribe(usernameexist => {
            if (usernameexist)
            {
              alert('اسم المستخدم موجود بالفعل');
            }
            else
            {
              this.http.get<boolean>(this.baseurl + 'SignUp?username=' + username.value + '&email=' + email.value + '&Password=' + password.value)
              .subscribe(Signed => {
                if (Signed)
                {
                  alert('لقد قمت بالتسجيل بنجاح')
                  username.value = email.value = password.value = cpassword.value = '';
                }
                else
                {
                  alert('خطأ، يرجي المحاولة مرة اخري');
                }
              });
            }
          })
        }

        sub.removeAttribute('disabled');
      });
    }
  }

}
