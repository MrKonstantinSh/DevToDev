import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { BlogRoutingModule } from "./blog-routing.module";
import { SharedModule } from "../shared/shared.module";
import { SearchPageComponent } from "./components/search-page/search-page.component";
import { NavBarComponent } from "../shared/components/nav-bar/nav-bar.component";
import { MatCardModule } from "@angular/material/card";
import { ArticleCardComponent } from "./components/article-card/article-card.component";
import { ArticleConstructorPageComponent } from "./components/article-constructor-page/article-constructor-page.component";
import { ArticlePageComponent } from "./components/article-page/article-page.component";

@NgModule({
  declarations: [
    SearchPageComponent,
    ArticleCardComponent,
    ArticleConstructorPageComponent,
    ArticlePageComponent,
  ],
  imports: [CommonModule, BlogRoutingModule, SharedModule, MatCardModule],
  exports: [],
  providers: [NavBarComponent],
})
export class BlogModule {}
