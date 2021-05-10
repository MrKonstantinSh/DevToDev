import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Article } from "src/app/shared/models/article";
import { ArticleService } from "../../services/article.service";

@Component({
  selector: "app-my-articles-page",
  templateUrl: "./my-articles-page.component.html",
  styleUrls: ["./my-articles-page.component.css"],
})
export class MyArticlesPageComponent implements OnInit {
  articles: Article[] = [];

  constructor(private articleService: ArticleService, private router: Router) {}

  ngOnInit(): void {
    this.articleService.getMyArticles().subscribe((articles) => {
      this.articles = articles;
    });
  }
}
