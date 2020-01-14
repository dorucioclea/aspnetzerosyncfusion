import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TestTablesComponent } from './dbo/testTables/testTables.component';
import { ArausersComponent } from './dbo/arausers/arausers.component';
import { AraprofilesComponent } from './dbo/araprofiles/araprofiles.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { GridTestComponent } from './savcorTest/gridTest.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'dbo/testTables', component: TestTablesComponent, data: { permission: 'Pages.TestTables' }  },
                    { path: 'dbo/arausers', component: ArausersComponent, data: { permission: 'Pages.Arausers' }  },
                    { path: 'dbo/araprofiles', component: AraprofilesComponent, data: { permission: 'Pages.Araprofiles' }  },
                    { path: 'dashboard', component: DashboardComponent, data: { permission: 'Pages.Tenant.Dashboard' } },
                    { path: 'gridTest', component: GridTestComponent }
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }
