import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { IdentityRoutingModule } from "./identity-routing.module";
import { SignInComponent } from "./components/sign-in/sign-in.component";
import { SignUpComponent } from "./components/sign-up/sign-up.component";
import { SharedModule } from "../shared/shared.module";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { IdentityInterceptor } from "./guards/identity.interceptor";
import { UserProfilePageComponent } from './components/user-profile-page/user-profile-page.component';

@NgModule({
  declarations: [SignInComponent, SignUpComponent, UserProfilePageComponent],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: IdentityInterceptor,
      multi: true,
    },
  ],
  imports: [CommonModule, IdentityRoutingModule, SharedModule],
  exports: [SignInComponent, SignUpComponent],
})
export class IdentityModule {}
