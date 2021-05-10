import {
  AfterViewInit,
  Component,
  ElementRef,
  OnInit,
  ViewChild,
} from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Article } from "src/app/shared/models/article";
import { ArticleService } from "../../services/article.service";

@Component({
  selector: "app-article",
  templateUrl: "./article.component.html",
  styleUrls: ["./article.component.css"],
})
export class ArticleComponent implements OnInit, AfterViewInit {
  @ViewChild("content") content: ElementRef;
  article: Article;

  constructor(
    private activatedRoute: ActivatedRoute,
    private articleService: ArticleService
  ) {}

  ngOnInit(): void {}

  ngAfterViewInit() {
    const articleId = Number.parseInt(
      this.activatedRoute.snapshot.paramMap.get("id")
    );

    this.articleService
      .getArticleById(articleId)
      .subscribe((article: Article) => {
        this.article = article;
        this.content.nativeElement.insertAdjacentHTML(
          "beforeend",
          this.article?.content
        );
      });
  }
}
