import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { WeatherService } from '../weather.service';
import { CommonModule } from '@angular/common';
import { SkyBoxModule } from '@skyux/layout';

@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html',
  styleUrls: ['./weather.component.css'],
  standalone: true,
  imports: [CommonModule, SkyBoxModule]
})
export class WeatherComponent implements  OnInit{

  public latitude:any;
  public longitude:any;
  weatherData: any[] = [];
  constructor(private route: ActivatedRoute, private weatherService: WeatherService){
    this.latitude = this.route.snapshot.params['lat'];
    this.longitude = this.route.snapshot.params['long'];

  }

  ngOnInit(): void {
    setTimeout(() => {
      this.weatherService.getWeatherData(this.latitude,this.longitude).subscribe((data: any[]) => {
        this.weatherData = data;
      });
      
    }, 100);
  }

}
