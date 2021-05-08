import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { IdentityService } from "src/app/identity/services/identity.service";

@Injectable({
  providedIn: "root",
})
export class AuthGuard implements CanActivate {
  constructor(
    private identityService: IdentityService,
    private router: Router
  ) {}

  canLoad() {
    if (!this.identityService.isLoggedIn()) {
      this.router.navigate(["/sign-in"]);
    }
    return this.identityService.isLoggedIn();
  }

  canActivate() {
    return this.canLoad();
  }
}
