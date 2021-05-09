import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { BlogRoutingModule } from "./blog-routing.module";
import { SharedModule } from "../shared/shared.module";
import { FeedComponent } from "./components/search/search.component";
import { NavBarComponent } from "../shared/components/nav-bar/nav-bar.component";
import { SearchInputComponent } from "./components/search-input/search-input.component";
import { MatChipsModule } from "@angular/material/chips";
import { MatIconModule } from "@angular/material/icon";
import { MatFormFieldModule } from "@angular/material/form-field";
import { CardComponent } from './components/card/card.component';

@NgModule({
  declarations: [FeedComponent, SearchInputComponent, CardComponent],
  imports: [
    CommonModule,
    BlogRoutingModule,
    SharedModule,
    MatChipsModule,
    MatIconModule,
    MatFormFieldModule,
  ],
  exports: [],
  providers: [NavBarComponent],
})
export class BlogModule {}
