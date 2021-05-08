import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from "@angular/forms";
import { NavBarComponent } from "./components/nav-bar/nav-bar.component";
import { RouterModule } from "@angular/router";
import { MatMenuModule } from "@angular/material/menu";

const materials = [MatMenuModule];

@NgModule({
  declarations: [NavBarComponent],
  imports: [CommonModule, ReactiveFormsModule, RouterModule, MatMenuModule],
  exports: [ReactiveFormsModule, NavBarComponent, MatMenuModule],
})
export class SharedModule {}
