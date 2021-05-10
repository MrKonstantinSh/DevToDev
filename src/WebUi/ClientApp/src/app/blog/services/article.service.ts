import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { catchError, mapTo, tap } from "rxjs/operators";
import { Article } from "src/app/shared/models/article";
import { environment } from "src/environments/environment";
import { ArticleDto } from "../dtos/articleDto";
import { ArticleToUpdateDto } from "../dtos/articleToUpdateDto";

@Injectable({
  providedIn: "root",
})
export class ArticleService {
  private readonly baseUrl: string = environment.apiUrl;

  constructor(private httpClient: HttpClient) {}

  getArticleById(id: number): Observable<Article> {
    return this.httpClient.get(this.baseUrl + `/article?id=${id}`, {}).pipe(
      tap((article: Article) => {
        return of(article);
      }),
      catchError(() => {
        return of(null);
      })
    );
  }

  searchArticle(searchString: string): Observable<Article[]> {
    return this.httpClient
      .get(this.baseUrl + `/article/search?searchString=${searchString}`, {})
      .pipe(
        tap((articles: Article[]) => {
          return of(articles);
        }),
        catchError(() => {
          return of(null);
        })
      );
  }

  getLatestArticles(): Observable<Article[]> {
    return this.httpClient.get(this.baseUrl + "/article/latest", {}).pipe(
      tap((articles: Article[]) => {
        return of(articles);
      }),
      catchError(() => {
        return of(null);
      })
    );
  }

  getMyArticles(): Observable<Article[]> {
    return this.httpClient.get(this.baseUrl + "/article/my", {}).pipe(
      tap((articles: Article[]) => {
        return of(articles);
      }),
      catchError(() => {
        return of(null);
      })
    );
  }

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

  updateArticle(values: ArticleToUpdateDto): Observable<boolean> {
    return this.httpClient
      .put(this.baseUrl + `/article/update/${values.id}`, values)
      .pipe(
        mapTo(true),
        catchError(() => {
          return of(false);
        })
      );
  }
}
