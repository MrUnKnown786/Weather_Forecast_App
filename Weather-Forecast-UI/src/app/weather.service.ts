import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {
  private apiUrl = 'https://localhost:44394/WeatherForecast/';

  constructor(private http: HttpClient) {}

  getWeatherData(lat:string, long:string): Observable<any> {
    return this.http.get(this.apiUrl+lat+"/"+long);
  }
}
