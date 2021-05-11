import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
})
export class AppComponent implements OnInit {
  title = "DevToDev";

  constructor(private router: Router) {}

  ngOnInit(): void {
    if (localStorage.getItem("accessToken")) {
      this.router.navigateByUrl("/search");
    }
  }
}
