import { Component, AfterViewInit, ViewChild, ElementRef } from "@angular/core";
declare const MediumEditor: any;

@Component({
  selector: "app-article-constructor",
  templateUrl: "./article-constructor.component.html",
  styleUrls: ["./article-constructor.component.css"],
})
export class ArticleConstructorComponent implements AfterViewInit {
  editor: any;
  @ViewChild("title", { static: true }) title: ElementRef;
  @ViewChild("description", { static: true }) description: ElementRef;
  @ViewChild("content", { static: true }) content: ElementRef;

  constructor() {}

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
    console.log(this.title.nativeElement.value);
    console.log(this.description.nativeElement.value);
    console.log(this.content.nativeElement);
  }
}
