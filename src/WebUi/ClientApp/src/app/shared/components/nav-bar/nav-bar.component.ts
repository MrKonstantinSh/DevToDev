import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { IdentityService } from "src/app/identity/services/identity.service";
import { User } from "../../models/user";

@Component({
  selector: "app-nav-bar",
  templateUrl: "./nav-bar.component.html",
  styleUrls: ["./nav-bar.component.css"],
})
export class NavBarComponent implements OnInit {
  currentUser$: Observable<User>;

  constructor(
    private identityService: IdentityService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadCurrentUser();
    this.currentUser$ = this.identityService.currentUser$;
  }

  logOut() {
    this.identityService.logOut().subscribe(
      (res) => {
        if (res) {
          this.router.navigateByUrl("/sign-in");
        }
      },
      (error) => {
        // TODO: error handler
      }
    );
  }

  redirectToMyArticlesPage() {
    this.router.navigateByUrl("/my-articles");
  }

  redirectToArticleConstructorPage() {
    this.router.navigateByUrl("/article-constructor");
  }

  redirectToUserProfilePage() {
    this.router.navigateByUrl("/my-profile");
  }

  private loadCurrentUser() {
    if (localStorage.getItem("accessToken")) {
      this.identityService.loadCurrentUser().subscribe(
        () => {},
        (error) => {
          console.log(error);
        }
      );
    }
  }
}
