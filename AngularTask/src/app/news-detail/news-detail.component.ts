import { Component, OnInit } from '@angular/core';
import { NewsService } from '../services/news.service';
import { AlertifyService } from '../services/alertify.service';
import { NgForm } from '@angular/forms';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category';

@Component({
  selector: 'app-news-detail',
  templateUrl: './news-detail.component.html',
  styleUrls: ['./news-detail.component.css'],
})
export class NewsDetailComponent implements OnInit {
  listofCategories: any[];
  selectedValue: any;
  imageUrl = '';
  isFileSelected = false;
  constructor(
    public newsService: NewsService,
    private categoryService: CategoryService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.resetForm();
    this.getCategories();
  }
  SubmitNews(form: NgForm) {
    if (!this.isFileSelected){
      this.alertify.error('Please select a file.');
      return;
    }
    if (this.selectedValue) {
      this.newsService.newsData.CategoryId = +this.selectedValue;
    }
    if (this.newsService.newsData.Id === 0) {
      this.insertRecord(form);
    } else {
      this.updateRecord(form);
    }
    
    this.isFileSelected = false;
  }

  insertRecord(form: NgForm) {
    this.newsService.postNews().subscribe(
      (res) => {
        this.resetForm(form);
        this.alertify.success('Submitted successfully');
        this.newsService.loadNewsByName('');
      },
      (err) => {
        this.alertify.error(err);
      }
    );
  }

  handleFileInput(files: FileList) {
    this.isFileSelected = true;
    this.newsService.newsData.File = files.item(0);
  }

  updateRecord(form: NgForm) {
    this.newsService.putNews().subscribe(
      (res) => {
        this.resetForm(form);
        this.alertify.success('Submitted successfully');
      },
      (err) => {
        this.alertify.error(err);
      }
    );
  }

  resetForm(form?: NgForm) {
    if (form != null) {
      form.resetForm();
    }
    this.newsService.newsData = {
      Id: 0,
      Name: '',
      Description: '',
      CategoryId: 0,
      File: {} as File,
    };
  }

  getCategories() {
    this.categoryService
      .getAll()
      .toPromise()
      .then((res) => (this.listofCategories = res as Category[]));
  }
}
