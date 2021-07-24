import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { EditUserInfoDto } from "../../dtos/editUserInfoDto";
import { IdentityService } from "../../services/identity.service";
import getBrowserFingerprint from "get-browser-fingerprint";

@Component({
  selector: "app-user-profile-page",
  templateUrl: "./user-profile-page.component.html",
  styleUrls: ["./user-profile-page.component.css"],
})
export class UserProfilePageComponent implements OnInit {
  userProfileForm: FormGroup;
  editError: string;

  constructor(private identityService: IdentityService) {}

  ngOnInit(): void {
    this.createUserProfileForm();
    this.fillFields();
  }

  onSubmit() {
    const editUserInfoDto = new EditUserInfoDto();
    editUserInfoDto.username = this.userProfileForm.controls.username.value;
    editUserInfoDto.firstName = this.userProfileForm.controls.firstName.value;
    editUserInfoDto.lastName = this.userProfileForm.controls.lastName.value;

    this.identityService.editUserInfo(editUserInfoDto).subscribe((result) => {
      if (!result) {
        this.editError = "This Username is already taken.";
      }
    });
  }

  private fillFields() {
    this.identityService.loadCurrentUser().subscribe(() => {
      const user = this.identityService.getCurrentUser();

      this.userProfileForm.controls.username.setValue(user.username);
      this.userProfileForm.controls.firstName.setValue(user.firstName);
      this.userProfileForm.controls.lastName.setValue(user.lastName);
    });
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
