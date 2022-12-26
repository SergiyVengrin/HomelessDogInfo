import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Email } from '../Interfaces/email';
import { Response } from '../Interfaces/response';



@Injectable({
  providedIn: 'root'
})
export class EmailService {
  private readonly _endpoint: string = environment.endpoint;
  private readonly _apiUrl: string = this._endpoint + "Email/SendEmail";

  constructor(private _httpClient: HttpClient) { }

  sendEmail(request: Email): Observable<Response> {
    return this._httpClient.post<Response>(this._apiUrl, request);
  }
}
