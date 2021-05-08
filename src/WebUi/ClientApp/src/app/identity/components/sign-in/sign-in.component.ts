import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import getBrowserFingerprint from "get-browser-fingerprint";
import { IdentityService } from "../../services/identity.service";
import { SignInDto } from "../../dtos/signInDto";
import { Router } from "@angular/router";

@Component({
  selector: "app-sign-in",
  templateUrl: "./sign-in.component.html",
  styleUrls: ["./sign-in.component.css"],
})
export class SignInComponent implements OnInit {
  signInError: string;
  signInForm: FormGroup;

  constructor(
    private identityService: IdentityService,
    private router: Router
  ) {}

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
          this.router.navigateByUrl("/search");
        } else {
          this.signInError = "Login or password is entered incorrectly.";
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
