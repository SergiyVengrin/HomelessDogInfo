import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmailComponent } from './email.component';
import { ReactiveFormsModule } from "@angular/forms";
import { MaterialModule } from 'src/material.module';
import{BrowserAnimationsModule} from '@angular/platform-browser/animations'


@NgModule({
  declarations: [
    EmailComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MaterialModule,
    BrowserAnimationsModule
  ],
  exports:[
    EmailComponent
  ]
})
export class EmailModule { }
