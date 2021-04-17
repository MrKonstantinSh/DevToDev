import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";

import { IdentityModule } from "./identity/identity.module";

import { AppComponent } from "./app.component";

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    IdentityModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([], { relativeLinkResolution: "legacy" }),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
