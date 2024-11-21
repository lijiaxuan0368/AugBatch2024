import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './account/login/login.component';
import { TopRatedComponent } from './movie/top-rated/top-rated.component';

export const routes: Routes = [
{path: "", component: HomeComponent},
{path: "account/login", component:LoginComponent},
{path: "movie/toprated", component: TopRatedComponent}

];
