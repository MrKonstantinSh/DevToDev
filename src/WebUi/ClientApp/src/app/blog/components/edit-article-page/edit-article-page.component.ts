import { AfterViewInit, Component, ElementRef, ViewChild } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { ArticleToUpdateDto } from "../../dtos/articleToUpdateDto";
import { ArticleService } from "../../services/article.service";
declare const MediumEditor: any;

@Component({
  selector: "app-edit-article-page",
  templateUrl: "./edit-article-page.component.html",
  styleUrls: ["./edit-article-page.component.css"],
})
export class EditArticlePageComponent implements AfterViewInit {
  editArticleForm: FormGroup;
  @ViewChild("content", { static: true }) content: ElementRef;
  editor: any;

  currentArticleId: number;

  constructor(
    private articleService: ArticleService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {
    this.createArticleForm();
  }

  ngAfterViewInit(): void {
    this.editor = new MediumEditor(this.content.nativeElement, {
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

    const articleId = Number.parseInt(
      this.activatedRoute.snapshot.paramMap.get("id")
    );

    this.articleService.getArticleById(articleId).subscribe((article) => {
      this.editArticleForm.controls.title.setValue(article.title);
      this.editArticleForm.controls.description.setValue(article.description);

      this.editor.selectElement(document.querySelector(".editor"));
      this.editor.pasteHTML(article.content);

      this.currentArticleId = article.id;
    });
  }

  onSubmit() {
    const articleToUpdateDto = new ArticleToUpdateDto();
    articleToUpdateDto.id = this.currentArticleId;
    articleToUpdateDto.title = this.editArticleForm.controls.title.value;
    articleToUpdateDto.description = this.editArticleForm.controls.description.value;
    articleToUpdateDto.content = this.content.nativeElement.innerHTML;

    articleToUpdateDto.content = articleToUpdateDto.content.replaceAll(
      "<img",
      '<img style="width: 100%"'
    );

    this.articleService.updateArticle(articleToUpdateDto).subscribe(
      () => {
        this.router.navigateByUrl(`/article/${this.currentArticleId}`);
      },
      (error) => {
        // TODO: add error handler
      }
    );
  }

  private createArticleForm() {
    this.editArticleForm = new FormGroup({
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
