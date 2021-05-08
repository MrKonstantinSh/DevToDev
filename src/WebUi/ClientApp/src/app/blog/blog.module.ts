import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { BlogRoutingModule } from "./blog-routing.module";
import { SharedModule } from "../shared/shared.module";
import { FeedComponent } from "./components/search/search.component";
import { NavBarComponent } from "../shared/components/nav-bar/nav-bar.component";

@NgModule({
  declarations: [FeedComponent],
  imports: [CommonModule, BlogRoutingModule, SharedModule],
  exports: [],
  providers: [NavBarComponent],
})
export class BlogModule {}
