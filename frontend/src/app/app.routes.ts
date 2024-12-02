import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { AboutComponent } from './pages/about/about.component';
import { ClassesComponent } from './pages/classes/classes.component';
import { ProductsComponent } from './pages/products/products.component';
import { SponsorshipComponent } from './pages/sponsorship/sponsorship.component';

export const routes: Routes = [
    {
        path: "",
        component: HomeComponent
    },
    {
        path: "about",
        component: AboutComponent
    },
    {
        path: "classes",
        component: ClassesComponent
    },
    {
        path: "products",
        component: ProductsComponent
    },
    {
        path: "sponsorship",
        component: SponsorshipComponent
    },
];
