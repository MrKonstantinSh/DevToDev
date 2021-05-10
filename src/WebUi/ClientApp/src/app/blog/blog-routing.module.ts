import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AuthGuard } from "../shared/guards/auth.guard";
import { ArticleConstructorPageComponent } from "./components/article-constructor-page/article-constructor-page.component";
import { ArticlePageComponent } from "./components/article-page/article-page.component";
import { MyArticlesPageComponent } from "./components/my-articles-page/my-articles-page.component";
import { SearchPageComponent } from "./components/search-page/search-page.component";

const routes: Routes = [
  {
    path: "search",
    component: SearchPageComponent,
    canLoad: [AuthGuard],
    canActivate: [AuthGuard],
  },
  {
    path: "article-constructor",
    component: ArticleConstructorPageComponent,
    canLoad: [AuthGuard],
    canActivate: [AuthGuard],
  },
  {
    path: "article/:id",
    component: ArticlePageComponent,
    canLoad: [AuthGuard],
    canActivate: [AuthGuard],
  },
  {
    path: "my-articles",
    component: MyArticlesPageComponent,
    canLoad: [AuthGuard],
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BlogRoutingModule {}
