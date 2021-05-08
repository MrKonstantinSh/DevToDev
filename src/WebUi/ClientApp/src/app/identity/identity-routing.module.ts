import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { SignInComponent } from "./components/sign-in/sign-in.component";
import { SignUpComponent } from "./components/sign-up/sign-up.component";
import { IdentityGuard } from "./guards/identity.guard";

const routes: Routes = [
  { path: "sign-in", component: SignInComponent, canActivate: [IdentityGuard] },
  { path: "sign-up", component: SignUpComponent, canActivate: [IdentityGuard] },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class IdentityRoutingModule {}
