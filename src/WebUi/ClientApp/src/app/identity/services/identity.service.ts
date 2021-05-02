import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { BehaviorSubject, Observable, of, Subscription } from "rxjs";
import { catchError, mapTo, tap } from "rxjs/operators";
import { environment } from "src/environments/environment";
import { User } from "../../shared/models/user";
import { SignInDto } from "../dtos/signInDto";
import { SignUpDto } from "../dtos/signUpDto";

@Injectable({
  providedIn: "root",
})
export class IdentityService {
  private readonly baseUrl: string = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<User>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private httpClient: HttpClient, private router: Router) {}

  signUp(values: SignUpDto): Observable<boolean> {
    return this.httpClient
      .post(this.baseUrl + "/identity/register", values)
      .pipe(
        mapTo(true),
        catchError(() => {
          return of(false);
        })
      );
  }

  signIn(values: SignInDto): Observable<boolean> {
    return this.httpClient.post(this.baseUrl + "/identity/login", values).pipe(
      tap((user: User) => {
        if (user) {
          localStorage.setItem("accessToken", user.accessToken);
          this.currentUserSource.next(user);
        }
      }),
      mapTo(true),
      catchError(() => {
        return of(false);
      })
    );
  }

  refreshTokens(fingerprint: string): Observable<boolean> {
    return this.httpClient
      .post(this.baseUrl + "/identity/refresh-tokens", {
        fingerprint: fingerprint,
      })
      .pipe(
        tap((response: any) => {
          localStorage.setItem("accessToken", response.accessToken);
          console.log(response);
        }),
        mapTo(true),
        catchError(() => {
          return of(false);
        })
      );
  }

  logOut(): Observable<boolean> {
    return this.httpClient.post(this.baseUrl + "/identity/logout", null).pipe(
      tap(() => {
        localStorage.removeItem("accessToken");
        this.currentUserSource.next(null);
        this.router.navigateByUrl("/");
      }),
      mapTo(true),
      catchError(() => {
        return of(false);
      })
    );
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem("accessToken");
  }
}
