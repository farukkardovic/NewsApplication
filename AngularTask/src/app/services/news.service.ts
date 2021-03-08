import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { News } from '../models/news';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../models/Pagination';
import { map } from 'rxjs/operators';
import { baseUrl } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class NewsService {
  newsData: News = {} as News;
  imageUrl: string;

  constructor(private http: HttpClient) {}

  postNews() {
    return this.http.post(baseUrl + 'News', this.getFormData());
  }

  putNews() {
    return this.http.put(baseUrl + 'News', this.getFormData());
  }

  deleteNews(id: number) {
    return this.http.delete(baseUrl + 'News/' + id);
  }

  loadNewsByName(
    searchQuery: string,
    page?,
    itemsPerPage?
  ): Observable<PaginatedResult<News[]>> {
    const paginatedResult: PaginatedResult<News[]> = new PaginatedResult<
      News[]
    >();

    let params = new HttpParams();
    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }
    if (searchQuery != null) {
      params = params.append('searchQuery', searchQuery);
    }
    return this.http
      .get<News[]>(baseUrl + 'News/GetByName', { observe: 'response', params })
      .pipe(
        map((response) => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(
              response.headers.get('Pagination')
            );
            return paginatedResult;
          }
        })
      );
  }

  getById(id: number): Observable<News> {
    const news = this.http.get<News>(baseUrl + 'News/GetById?id=' + id);
    return news;
  }

  private getFormData(): FormData {
    const formData: FormData = new FormData();


    formData.append('File', this.newsData.File, this.newsData.File.name);
    formData.append('CategoryId', this.newsData.CategoryId.toString());
    formData.append('Name', this.newsData.Name);
    formData.append('Id', this.newsData.Id.toString());
    formData.append('Description', this.newsData.Description);

    return formData;
  }
}
