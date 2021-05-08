import { Injectable } from "@angular/core";
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from "@angular/common/http";
import { BehaviorSubject, Observable, throwError } from "rxjs";
import getBrowserFingerprint from "get-browser-fingerprint";
import { IdentityService } from "../services/identity.service";
import { catchError, filter, switchMap, take } from "rxjs/operators";

@Injectable()
export class IdentityInterceptor implements HttpInterceptor {
  private isRefreshing = false;
  private refreshAccessTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(
    null
  );

  constructor(private identityService: IdentityService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const accessToken = localStorage.getItem("accessToken");

    if (accessToken) {
      request = this.addAccessTokenToRequest(request, accessToken);
    }

    return next.handle(request).pipe(
      catchError((error: any) => {
        if (error instanceof HttpErrorResponse && error.status === 401) {
          return this.handle401Error(request, next);
        } else {
          return throwError(error);
        }
      })
    );
  }

  private addAccessTokenToRequest(
    request: HttpRequest<any>,
    accessToken: string
  ) {
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${accessToken}`,
      },
    });
  }

  private handle401Error(request: HttpRequest<any>, next: HttpHandler) {
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.refreshAccessTokenSubject.next(null);

      const browserFingerprint: string = getBrowserFingerprint().toString();

      return this.identityService.refreshTokens(browserFingerprint).pipe(
        switchMap((response: any) => {
          this.isRefreshing = false;
          this.refreshAccessTokenSubject.next(response.accessToken);
          return next.handle(
            this.addAccessTokenToRequest(request, response.accessToken)
          );
        })
      );
    } else {
      return this.refreshAccessTokenSubject.pipe(
        filter((accessToken) => accessToken != null),
        take(1),
        switchMap((accessToken) => {
          return next.handle(
            this.addAccessTokenToRequest(request, accessToken)
          );
        })
      );
    }
  }
}
