import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AuthGuard } from "../shared/guards/auth.guard";
import { ArticleConstructorComponent } from "./components/article-constructor/article-constructor.component";
import { ArticleComponent } from "./components/article/article.component";
import { FeedComponent } from "./components/search/search.component";

const routes: Routes = [
  {
    path: "search",
    component: FeedComponent,
    canLoad: [AuthGuard],
    canActivate: [AuthGuard],
  },
  {
    path: "article-constructor",
    component: ArticleConstructorComponent,
    canLoad: [AuthGuard],
    canActivate: [AuthGuard],
  },
  {
    path: "article/:id",
    component: ArticleComponent,
    canLoad: [AuthGuard],
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BlogRoutingModule {}
