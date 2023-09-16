import { Component } from '@angular/core';
import { LoadingService } from './loading.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  providers: [LoadingService]
})
export class AppComponent {
  title = 'Elrouby Asks';
}


