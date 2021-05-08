import { Component, OnInit } from "@angular/core";
import {
  AsyncValidatorFn,
  FormControl,
  FormGroup,
  ValidatorFn,
  Validators,
} from "@angular/forms";
import { Router } from "@angular/router";
import { of, timer } from "rxjs";
import { map, switchMap } from "rxjs/operators";
import { SignUpDto } from "../../dtos/signUpDto";
import { IdentityService } from "../../services/identity.service";

@Component({
  selector: "app-sign-up",
  templateUrl: "./sign-up.component.html",
  styleUrls: ["./sign-up.component.css"],
})
export class SignUpComponent implements OnInit {
  signUpForm: FormGroup;

  constructor(
    private identityService: IdentityService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.createSignUpForm();

    this.signUpForm.controls.password.valueChanges.subscribe(() => {
      this.signUpForm.controls.rePassword.updateValueAndValidity();
    });
  }

  onSubmit() {
    const signUpDto = new SignUpDto();
    signUpDto.email = this.signUpForm.value.email;
    signUpDto.password = this.signUpForm.value.password;
    signUpDto.rePassword = this.signUpForm.value.rePassword;

    this.identityService.signUp(signUpDto).subscribe(
      () => {
        this.router.navigateByUrl("/sign-in");
      },
      (error) => {
        // TODO: add error handler
      }
    );
  }

  validateEmailNotTaken(): AsyncValidatorFn {
    return (control: FormControl) => {
      return timer(500).pipe(
        switchMap(() => {
          if (!control.value) {
            return of(null);
          }
          return this.identityService.checkEmailAddress(control.value).pipe(
            map((response: any) => {
              return response.isEmailAlreadyTaken
                ? { isEmailExists: true }
                : null;
            })
          );
        })
      );
    };
  }

  validatePasswordMatch(): ValidatorFn {
    return (control: FormControl) => {
      if (!control || !control.parent) {
        return null;
      }

      return control.parent.get("password").value !== control.value
        ? { isPasswordMismatch: true }
        : null;
    };
  }

  private createSignUpForm() {
    this.signUpForm = new FormGroup({
      email: new FormControl(
        "",
        [
          Validators.required,
          Validators.pattern("[a-z0-9._%-]+@[a-z0-9._%-]+\\.[a-z]{2,4}"),
        ],
        [this.validateEmailNotTaken()]
      ),
      password: new FormControl("", Validators.required),
      rePassword: new FormControl("", [
        Validators.required,
        this.validatePasswordMatch(),
      ]),
    });
  }
}
