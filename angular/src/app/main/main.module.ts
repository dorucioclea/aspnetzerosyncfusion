import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppCommonModule } from '@app/shared/common/app-common.module';
import { ResourcesComponent } from './dbo/resources/resources.component';
import { ViewResourceModalComponent } from './dbo/resources/view-resource-modal.component';
import { CreateOrEditResourceModalComponent } from './dbo/resources/create-or-edit-resource-modal.component';

import { TestComponent } from './dbo/test/test.component';
import { ViewTestModalComponent } from './dbo/test/view-test-modal.component';
import { CreateOrEditTestModalComponent } from './dbo/test/create-or-edit-test-modal.component';

import { TestTablesComponent } from './dbo/testTables/testTables.component';
import { ViewTestTableModalComponent } from './dbo/testTables/view-testTable-modal.component';
import { CreateOrEditTestTableModalComponent } from './dbo/testTables/create-or-edit-testTable-modal.component';

import { ArausersComponent } from './dbo/arausers/arausers.component';
import { ViewArauserModalComponent } from './dbo/arausers/view-arauser-modal.component';
import { CreateOrEditArauserModalComponent } from './dbo/arausers/create-or-edit-arauser-modal.component';
import { ArauserAraprofileLookupTableModalComponent } from './dbo/arausers/arauser-araprofile-lookup-table-modal.component';

import { AraprofilesComponent } from './dbo/araprofiles/araprofiles.component';
import { ViewAraprofileModalComponent } from './dbo/araprofiles/view-araprofile-modal.component';
import { CreateOrEditAraprofileModalComponent } from './dbo/araprofiles/create-or-edit-araprofile-modal.component';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { PaginatorModule } from 'primeng/paginator';
import { EditorModule } from 'primeng/editor';
import { InputMaskModule } from 'primeng/inputmask';import { FileUploadModule } from 'primeng/fileupload';
import { TableModule } from 'primeng/table';

import { UtilsModule } from '@shared/utils/utils.module';
import { CountoModule } from 'angular2-counto';
import { ModalModule, TabsModule, TooltipModule, BsDropdownModule, PopoverModule } from 'ngx-bootstrap';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MainRoutingModule } from './main-routing.module';
import { NgxChartsModule } from '@swimlane/ngx-charts';

import { BsDatepickerModule, BsDatepickerConfig, BsDaterangepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxBootstrapDatePickerConfigService } from 'assets/ngx-bootstrap/ngx-bootstrap-datepicker-config.service';

import { TabModule } from '@syncfusion/ej2-angular-navigations';
import { GridModule, PageService, SortService, FilterService, GroupService, SearchService, ToolbarService, ReorderService, ResizeService, ColumnChooserService, EditService } from '@syncfusion/ej2-angular-grids';
import { GridTestComponent } from './savcorTest/gridTest.component';
import { DialogModule } from '@syncfusion/ej2-angular-popups';

NgxBootstrapDatePickerConfigService.registerNgxBootstrapDatePickerLocales();

@NgModule({
    imports: [
		FileUploadModule,
		AutoCompleteModule,
		PaginatorModule,
		EditorModule,
		InputMaskModule,		TableModule,

        CommonModule,
        FormsModule,
        ModalModule,
        TabsModule,
        TooltipModule,
        AppCommonModule,
        UtilsModule,
        MainRoutingModule,
        CountoModule,
        NgxChartsModule,
        BsDatepickerModule.forRoot(),
        BsDropdownModule.forRoot(),
        PopoverModule.forRoot(),
        TabModule,
        GridModule,
        DialogModule
    ],
    declarations: [
		ResourcesComponent,
		ViewResourceModalComponent,		CreateOrEditResourceModalComponent,
		TestComponent,
		ViewTestModalComponent,		CreateOrEditTestModalComponent,
		TestTablesComponent,
		ViewTestTableModalComponent,		CreateOrEditTestTableModalComponent,
		ArausersComponent,
		ViewArauserModalComponent,		CreateOrEditArauserModalComponent,
    ArauserAraprofileLookupTableModalComponent,
		AraprofilesComponent,
		ViewAraprofileModalComponent,		CreateOrEditAraprofileModalComponent,
        DashboardComponent,
        GridTestComponent
    ],
    providers: [
        { provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig },
        { provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig },
        { provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale },
        PageService,
    SortService,
    FilterService,
    GroupService,
    SearchService,
    ToolbarService,
    ReorderService,
    ResizeService,
    ColumnChooserService,
    EditService
    ]
})
export class MainModule { }
