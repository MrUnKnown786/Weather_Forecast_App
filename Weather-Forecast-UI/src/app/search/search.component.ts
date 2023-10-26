import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { SkyBoxModule } from '@skyux/layout';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css'],
  standalone: true,
  imports: [CommonModule, SkyBoxModule]
})
export class SearchComponent implements OnInit {

  public searchtext:any;
  public searchresults:any[] = [];
  constructor(private route: ActivatedRoute, private httpclient: HttpClient){
    this.searchtext = this.route.snapshot.params['text'];
  }

  ngOnInit(): void{
    setTimeout(() => {
      var url = 'https://geocoding-api.open-meteo.com/v1/search?name=' + this.searchtext;
    this.httpclient.get(url).subscribe((result) => {
      var results:any = [];
      results = result;
      this.searchresults = results['results']
    });
    }, 150);
  }
}
