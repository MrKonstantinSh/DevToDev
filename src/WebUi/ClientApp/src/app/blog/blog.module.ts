import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { BlogRoutingModule } from "./blog-routing.module";
import { SharedModule } from "../shared/shared.module";
import { FeedComponent } from "./components/search/search.component";
import { NavBarComponent } from "../shared/components/nav-bar/nav-bar.component";
import { SearchInputComponent } from "./components/search-input/search-input.component";
import { MatCardModule } from "@angular/material/card";
import { CardComponent } from "./components/card/card.component";
import { SearchResultComponent } from './components/search-result/search-result.component';
import { ArticleConstructorComponent } from './components/article-constructor/article-constructor.component';
import { ArticleComponent } from './components/article/article.component';

@NgModule({
  declarations: [FeedComponent, SearchInputComponent, CardComponent, SearchResultComponent, ArticleConstructorComponent, ArticleComponent],
  imports: [CommonModule, BlogRoutingModule, SharedModule, MatCardModule],
  exports: [],
  providers: [NavBarComponent],
})
export class BlogModule {}
