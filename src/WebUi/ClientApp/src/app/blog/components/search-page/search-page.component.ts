import { Component, OnInit } from "@angular/core";
import { Article } from "src/app/shared/models/article";
import { ArticleService } from "../../services/article.service";

@Component({
  selector: "app-search-page",
  templateUrl: "./search-page.component.html",
  styleUrls: ["./search-page.component.css"],
})
export class SearchPageComponent implements OnInit {
  searchInput: string;
  articles: Article[];

  constructor(private articleService: ArticleService) {}

  ngOnInit(): void {}

  search() {
    this.articleService
      .searchArticle(this.searchInput)
      .subscribe((articles) => {
        this.articles = articles;
      });
  }
}
