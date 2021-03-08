import { Component, OnInit } from '@angular/core';
import { NewsService } from '../services/news.service';
import { Pagination, PaginatedResult } from '../models/Pagination';
import { News } from '../models/news';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  searchQuery = '';
  pagination: Pagination;
  listOfNews: News[];

  constructor(public newsService: NewsService) { }

  ngOnInit() {
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 3
      } as Pagination;
    this.loadNews();
  }
  loadNews(){
    this.newsService.loadNewsByName(this.searchQuery, this.pagination.currentPage, this.pagination.itemsPerPage)
    .subscribe((res: PaginatedResult<News[]>) => {
      this.listOfNews = res.result;
      this.pagination = res.pagination;
    });
  }
  searchNews(){
    this.newsService.loadNewsByName(this.searchQuery, this.pagination.currentPage, this.pagination.itemsPerPage)
    .subscribe((res: PaginatedResult<News[]>) => {
      this.listOfNews = res.result;
      this.pagination = res.pagination;
    });
  }
    pageChanged(event: any): void {
      this.pagination.currentPage = event.page;
      this.loadNews();
    }
}
