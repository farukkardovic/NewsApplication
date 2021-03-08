import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { NewsDetailComponent } from './news-detail/news-detail.component';
import { NewsListComponent } from './news-list/news-list.component';
import { NewsComponent } from './news/news.component';
import { NewPreviewDetailsComponent } from './new-preview-details/new-preview-details.component';

export const appRoutes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {path: 'news-details', component: NewsDetailComponent, canActivate: [AuthGuard]},
            {path: 'news-list', component: NewsListComponent, canActivate: [AuthGuard]},
            {path: 'news', component: NewsComponent, canActivate: [AuthGuard]}


        ]
    },
    {path: 'news-view/:id', component: NewPreviewDetailsComponent},
    {path: '**', redirectTo: '', pathMatch: 'full'}
];