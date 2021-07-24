import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { BehaviorSubject, Observable, of } from "rxjs";
import { catchError, mapTo, map, tap } from "rxjs/operators";
import { environment } from "src/environments/environment";
import { User } from "../../shared/models/user";
import { EditUserInfoDto } from "../dtos/editUserInfoDto";
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

  getCurrentUser() {
    return this.currentUserSource.value;
  }

  loadCurrentUser(): Observable<void> {
    return this.httpClient.get(this.baseUrl + "/identity/user-info").pipe(
      map((user: User) => {
        if (user) {
          this.currentUserSource.next(user);
        }
      })
    );
  }

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
        }),
        mapTo(true),
        catchError(() => {
          return of(false);
        })
      );
  }

  checkEmailAddress(email: string): Observable<boolean> {
    return this.httpClient
      .get(this.baseUrl + `/identity/check-email?email=${email}`)
      .pipe(
        tap((response: any) => {
          return of(response.isEmailAlreadyTaken);
        }),
        catchError(() => {
          return of(false);
        })
      );
  }

  editUserInfo(values: EditUserInfoDto): Observable<boolean> {
    return this.httpClient.put(this.baseUrl + "/identity/update", values).pipe(
      mapTo(true),
      catchError(() => {
        return of(false);
      })
    );
  }

  logOut(): Observable<boolean> {
    return this.httpClient.post(this.baseUrl + "/identity/logout", {}).pipe(
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
