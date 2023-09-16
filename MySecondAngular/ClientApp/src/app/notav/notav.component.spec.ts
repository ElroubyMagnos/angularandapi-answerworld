import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NotavComponent } from './notav.component';

describe('NotavComponent', () => {
  let component: NotavComponent;
  let fixture: ComponentFixture<NotavComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NotavComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NotavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
