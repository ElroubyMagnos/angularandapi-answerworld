import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { LoadingService } from '../loading.service';

@Component({
  selector: 'app-add-question',
  templateUrl: './add-question.component.html',
  styleUrls: ['./add-question.component.css']
})
export class AddQuestionComponent implements OnInit {
  Cats: Cat[] = [];

  Email:string | null = '';

  constructor(public Loading: LoadingService ,@Inject('BASE_URL') private BaseURL: string, private http: HttpClient)
  {
    setInterval(()=>
    {
      if (this.Cats.length == 0 || this.Loading.TopQuestions.length == 0)
      {
        this.ngOnInit();
      }
    }, 777);
  }

  ngOnInit(): void {
    this.Loading.LoadTopQuestions();
    this.http.get<Cat[]>(this.BaseURL + 'Cat').subscribe(x => this.Cats = x);
  }

  AddCat(myCat: any)
  {
    this.Email = window.localStorage.getItem('Email');
    
    if (this.Email != null && this.Email.length > 0)
    {
      this.http.get<boolean>(this.BaseURL + 'CatCheck?Cat=' + myCat.value)
      .subscribe(Checked => {
        if (Checked)
        {
          alert('القسم بالفعل موجود');
        }
        else
        {
          this.http.get(this.BaseURL + 'Cat?Email=' + this.Email + '&Cat=' + myCat.value)
      .subscribe();
          this.Cats.push({id:0, catName:myCat.value, catOwnerID:0});
        }
        myCat.value = '';
      });
    }
  }

  AddQues(title: any, content: any, cat: any)
  {
    if (title.value.length < 3)
    {
      alert('العنوان قصير');
      return;
    }

    if (content.value.length == 0)
    {
      content.value = title.value;
    }

    this.http.get(this.BaseURL + 'AddQues?title=' + title.value + '&content=' + content.value + '&cat=' + cat.value + '&email=' + window.localStorage.getItem('Email'))
    .subscribe(x => {
      title.value = content.value = '';
      alert('تم النشر بنجاح');
    });
  }
}

interface Cat
{
  id: number;
  catName: string;
  catOwnerID: number;
}