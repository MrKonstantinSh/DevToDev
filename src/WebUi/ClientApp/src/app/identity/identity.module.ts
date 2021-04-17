import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { IdentityRoutingModule } from "./identity-routing.module";
import { SignInComponent } from "./components/sign-in/sign-in.component";
import { SignUpComponent } from "./components/sign-up/sign-up.component";

@NgModule({
  declarations: [SignInComponent, SignUpComponent],
  imports: [CommonModule, IdentityRoutingModule],
  exports: [SignInComponent, SignUpComponent],
})
export class IdentityModule {}
