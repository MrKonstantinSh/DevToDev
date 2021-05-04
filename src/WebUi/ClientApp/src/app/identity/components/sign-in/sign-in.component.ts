import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import getBrowserFingerprint from "get-browser-fingerprint";
import { IdentityService } from "../../services/identity.service";
import { SignInDto } from "../../dtos/signInDto";

@Component({
  selector: "app-sign-in",
  templateUrl: "./sign-in.component.html",
  styleUrls: ["./sign-in.component.css"],
})
export class SignInComponent implements OnInit {
  signInForm: FormGroup;

  constructor(private identityService: IdentityService) {}

  ngOnInit(): void {
    this.createSignInForm();
  }

  onSubmit() {
    const signInDto = new SignInDto();
    signInDto.usernameOrEmail = this.signInForm.value.usernameOrEmail;
    signInDto.password = this.signInForm.value.password;
    signInDto.fingerprint = getBrowserFingerprint().toString();

    this.identityService.signIn(signInDto).subscribe(
      (res) => {
        if (res) {
          console.log("User logged in");
        } else {
          console.log("ERROR");
        }
      },
      (error) => {
        // TODO: error handler
      }
    );
  }

  private createSignInForm() {
    this.signInForm = new FormGroup({
      usernameOrEmail: new FormControl("", Validators.required),
      password: new FormControl("", Validators.required),
    });
  }
}
