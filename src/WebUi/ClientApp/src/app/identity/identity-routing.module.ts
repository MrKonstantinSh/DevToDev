import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AuthGuard } from "../shared/guards/auth.guard";
import { SignInComponent } from "./components/sign-in/sign-in.component";
import { SignUpComponent } from "./components/sign-up/sign-up.component";
import { UserProfilePageComponent } from "./components/user-profile-page/user-profile-page.component";
import { IdentityGuard } from "./guards/identity.guard";

const routes: Routes = [
  { path: "sign-in", component: SignInComponent, canActivate: [IdentityGuard] },
  { path: "sign-up", component: SignUpComponent, canActivate: [IdentityGuard] },
  {
    path: "my-profile",
    component: UserProfilePageComponent,
    canLoad: [AuthGuard],
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class IdentityRoutingModule {}
