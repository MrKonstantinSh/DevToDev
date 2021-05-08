import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";

import { IdentityModule } from "./identity/identity.module";

import { AppComponent } from "./app.component";
import { BlogModule } from "./blog/blog.module";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    IdentityModule,
    BlogModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([], { relativeLinkResolution: "legacy" }),
    BrowserAnimationsModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
