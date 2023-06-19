import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CategoriesComponent } from './categories/categories.component';
import { FiltersComponent } from './filters/filters.component';
import { SubcategoryFiltersComponent } from './subcategory-filters/subcategory-filters.component';
import { FilterValuesComponent } from './filter-values/filter-values.component';
import { PackageComponent } from './package/package.component';
import { AdvertismentComponent } from './advertisment/advertisment.component';

const routes: Routes = [
  {path:'dashboardLogin',component:DashboardComponent},
  {path:'dashboard/categories',component:CategoriesComponent},
  {path:'dashboard/filters',component:FiltersComponent},
  {path:'dashboard/subCategoryFilters',component:SubcategoryFiltersComponent},
  {path:'dashboard/filterValues',component:FilterValuesComponent},
  {path:'dashboard/advertisments',component:AdvertismentComponent},
  {path:'dashboard/packages',component:PackageComponent}

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
