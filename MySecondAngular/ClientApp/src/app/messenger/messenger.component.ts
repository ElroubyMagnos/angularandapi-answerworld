import { Component, Inject } from '@angular/core';
import { LoadingService } from '../loading.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-messenger',
  templateUrl: './messenger.component.html',
  styleUrls: ['./messenger.component.css']
})
export class MessengerComponent {
  constructor(public Loading: LoadingService, private route: ActivatedRoute)
  {
    setInterval(()=>
    {
      if (Loading.Messages.length == 0)
      {
        Loading.LoadPMessages(route.snapshot.params['id']);
      }
    }, 777);
  }
}
