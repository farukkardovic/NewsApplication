import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NewsService } from '../services/news.service';
import { News } from '../models/news';
import { baseUrl } from 'src/environments/environment';

@Component({
  selector: 'app-new-preview-details',
  templateUrl: './new-preview-details.component.html',
  styleUrls: ['./new-preview-details.component.css'],
})
export class NewPreviewDetailsComponent implements OnInit {
  id: number;
  model: News = {} as News;
  constructor(private route: ActivatedRoute, public newsService: NewsService) {
    this.id = this.route.snapshot.params['id'];
  }

  ngOnInit() {
    this.loadNewsDetails();
  }

  loadNewsDetails() {
    return this.newsService.getById(this.id).subscribe((res: News) => {
      this.model = res;
      this.newsService.imageUrl =
        baseUrl + 'download/' + this.id + '?' + new Date().getTime();
    });
  }
}
