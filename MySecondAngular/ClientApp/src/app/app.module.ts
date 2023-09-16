import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { CommonModule } from "@angular/common";
import { HomeComponent } from './home/home.component';
import { FootComponent } from './foot/foot.component';
import { HeadComponent } from './head/head.component';
import { AddQuestionComponent } from './add-question/add-question.component';
import { ProfileComponent } from './profile/profile.component';
import { QuestionComponent } from './question/question.component';
import { NotavComponent } from './notav/notav.component';
import { MessengerComponent } from './messenger/messenger.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    FootComponent,
    HeadComponent,
    AddQuestionComponent,
    ProfileComponent,
    QuestionComponent,
    NotavComponent,
    MessengerComponent
  ],
  imports: [
    CommonModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent  },
      { path: 'aqc', component: AddQuestionComponent  },
      { path: 'qc/:id', component: QuestionComponent  },
      { path: 'pc/:id', component: ProfileComponent  },
      { path: 'mc/:id', component: MessengerComponent  },
      { path: '**', component: NotavComponent  },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
