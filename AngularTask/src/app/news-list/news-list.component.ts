import { Component, OnInit } from '@angular/core';
import { NewsService } from '../services/news.service';
import { AlertifyService } from '../services/alertify.service';
import { News } from '../models/news';
import { PaginatedResult, Pagination } from '../models/Pagination';
import { baseUrl } from 'src/environments/environment';

@Component({
  selector: 'app-news-list',
  templateUrl: './news-list.component.html',
  styleUrls: ['./news-list.component.css']
})
export class NewsListComponent implements OnInit {
  searchQuery = '';
  pagination: Pagination;
  listOfNews: News[];

  constructor(public newsService: NewsService, private alertify: AlertifyService) { }

  ngOnInit() {
this.pagination = {
currentPage: 1,
itemsPerPage: 3
} as Pagination;

this.loadNews();
  }

  populateForm(item: News) {
    this.newsService.getById(item.Id).subscribe((data) => this.newsService.newsData = data);
    this.newsService.imageUrl = baseUrl + 'download/' + item.Id + '?' + new Date().getTime();
  }
  onDelete(Id: number) {
    if (confirm('Are you sure to delete this record?')) {
      this.newsService.deleteNews(Id).subscribe(
        (res) => {
          this.alertify.warning(
            'Deleted successfully'
          );
          this.newsService.loadNewsByName(this.searchQuery, this.pagination.currentPage, this.pagination.itemsPerPage)
          .subscribe((res: PaginatedResult<News[]>) => {
            this.listOfNews = res.result;
            this.pagination = res.pagination;
          });
        },
        (err) => {
         this.alertify.error(err);
        }
      );
    } 

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
