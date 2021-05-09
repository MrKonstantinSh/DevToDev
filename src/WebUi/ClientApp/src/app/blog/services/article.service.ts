import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError, tap } from "rxjs/operators";
import { environment } from "src/environments/environment";
import { ArticleDto } from "../dtos/articleDto";

@Injectable({
  providedIn: "root",
})
export class ArticleService {
  private readonly baseUrl: string = environment.apiUrl;

  constructor(private httpClient: HttpClient, private router: Router) {}

  createArticle(values: ArticleDto): Observable<number | boolean> {
    return this.httpClient.post(this.baseUrl + "/article/create", values).pipe(
      tap((articleId: number) => {
        if (articleId) {
          return of(articleId);
        }
      }),
      catchError(() => {
        return of(false);
      })
    );
  }
}
