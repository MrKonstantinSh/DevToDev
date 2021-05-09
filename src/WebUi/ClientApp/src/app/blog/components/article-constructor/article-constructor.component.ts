import { Component, AfterViewInit, ViewChild, ElementRef } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { ArticleDto } from "../../dtos/articleDto";
import { ArticleService } from "../../services/article.service";
declare const MediumEditor: any;

@Component({
  selector: "app-article-constructor",
  templateUrl: "./article-constructor.component.html",
  styleUrls: ["./article-constructor.component.css"],
})
export class ArticleConstructorComponent implements AfterViewInit {
  addArticleForm: FormGroup;
  @ViewChild("content", { static: true }) content: ElementRef;
  editor: any;

  constructor(private articleService: ArticleService) {
    this.createArticleForm();
  }

  ngAfterViewInit(): void {
    this.editor = new MediumEditor(this.content.nativeElement, {
      paste: {
        cleanPastedHtml: true,
        cleanAttrs: ["style", "class", "name"],
        cleanTags: ["meta", "script"],
      },
      toolbar: {
        buttons: [
          "bold",
          "italic",
          "underline",
          "anchor",
          "quote",
          "image",
          "justifyLeft",
          "justifyCenter",
          "justifyRight",
          "justifyFull",
          "h1",
          "h2",
          "h3",
          "orderedlist",
          "unorderedlist",
          "pre",
        ],
        sticky: true,
      },
    });
  }

  onSubmit() {
    const articleDto = new ArticleDto();
    articleDto.title = this.addArticleForm.controls.title.value;
    articleDto.description = this.addArticleForm.controls.description.value;
    articleDto.content = this.content.nativeElement.innerHTML;

    this.articleService.createArticle(articleDto).subscribe(
      (articleId) => {
        console.log(articleId);
      },
      (error) => {
        // TODO: add error handler
      }
    );
  }

  private createArticleForm() {
    this.addArticleForm = new FormGroup({
      title: new FormControl("", [
        Validators.required,
        Validators.maxLength(100),
      ]),
      description: new FormControl("", [
        Validators.required,
        Validators.maxLength(254),
      ]),
    });
  }
}
