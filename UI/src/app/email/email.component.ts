import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Email } from '../Interfaces/email';
import { EmailService } from '../Services/email.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';


@Component({
  selector: 'app-email',
  templateUrl: './email.component.html',
  styleUrls: ['./email.component.css']
})


export class EmailComponent implements OnInit {
  title: string = "Email";

  public emailForm = this.formBuilder.group({
    receiver: ['', [Validators.required,Validators.email]],
    subject: ['', [Validators.required,Validators.minLength(5), Validators.maxLength(50)]],
    body: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(200)]]
  });


  constructor(private formBuilder: FormBuilder,
    private emailService: EmailService,
  ) { }

  ngOnInit(): void {
  }

  get receiver(){
    return this.emailForm.get('receiver');
  }

  get subject(){
    return this.emailForm.get('subject');
  }

  get body(){
    return this.emailForm.get('body');
  }


  onSubmit() {

    const model:Email = {
      receiver: this.emailForm.value.receiver!,
      subject: this.emailForm.value.subject!,
      body: this.emailForm.value.body!
    }

    this.emailService.sendEmail(model).subscribe({
      next: (response)=>{
        if(response.statusCode==200){
          console.log(this.emailForm.value);
        }
        else{
          console.log("email not sent");
        }
      },
      error:(error)=>{}
    });
  }
}
