import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";

@Component({
  selector: "app-user-profile-page",
  templateUrl: "./user-profile-page.component.html",
  styleUrls: ["./user-profile-page.component.css"],
})
export class UserProfilePageComponent implements OnInit {
  userProfileForm: FormGroup;

  constructor() {}

  ngOnInit(): void {
    this.createUserProfileForm();
  }

  onSubmit() {
    console.log(123);
  }

  private createUserProfileForm() {
    this.userProfileForm = new FormGroup({
      username: new FormControl("", [
        Validators.required,
        Validators.maxLength(25),
      ]),
      firstName: new FormControl("", [
        Validators.required,
        Validators.maxLength(255),
      ]),
      lastName: new FormControl("", [
        Validators.required,
        Validators.maxLength(255),
      ]),
    });
  }
}
