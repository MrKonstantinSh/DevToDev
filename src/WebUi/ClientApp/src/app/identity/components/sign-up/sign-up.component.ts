import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
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
        console.log(error);
      }
    );
  }

  private createSignUpForm() {
    this.signUpForm = new FormGroup({
      email: new FormControl("", [
        Validators.required,
        Validators.pattern("[a-z0-9._%-]+@[a-z0-9._%-]+\\.[a-z]{2,4}"),
      ]),
      password: new FormControl("", Validators.required),
      rePassword: new FormControl("", Validators.required),
    });
  }
}
