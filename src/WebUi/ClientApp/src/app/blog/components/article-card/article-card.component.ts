import { Component, Input, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Article } from "src/app/shared/models/article";

@Component({
  selector: "app-article-card",
  templateUrl: "./article-card.component.html",
  styleUrls: ["./article-card.component.css"],
})
export class ArticleCardComponent implements OnInit {
  @Input() article: Article;
  imageUrl: string;
  dateOfCreation: string;
  description: string;
  isMyArticlesPage = false;

  constructor(private router: Router) {
    if (router.url === "/my-articles") {
      this.isMyArticlesPage = true;
    }
  }

  ngOnInit(): void {
    this.dateOfCreation = new Date(
      this.article.dateOfCreation
    ).toLocaleDateString("en-US");

    this.description =
      this.article.description.length > 150
        ? this.article.description.substring(0, 150) + "..."
        : this.article.description;

    this.imageUrl = this.article.content.match(/src="http.*"/g)
      ? this.article.content.match(/src="http.*"/g)[0].split('"')[1]
      : "https://i.stack.imgur.com/y9DpT.jpg";
  }

  viewArticle(articleId) {
    this.router.navigateByUrl(`/article/${articleId}`);
  }

  editArticle(articleId) {
    this.router.navigateByUrl(`/edit-article/${articleId}`);
  }
}
