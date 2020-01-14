import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ArausersServiceProxy, ArauserDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditArauserModalComponent } from './create-or-edit-arauser-modal.component';
import { ViewArauserModalComponent } from './view-arauser-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';
import { GridComponent, DetailRowService, FilterSettingsModel, ToolbarItems, EditSettingsModel, SaveEventArgs, ForeignKeyService, SelectionService, RowSelectEventArgs } from '@syncfusion/ej2-angular-grids';
// import { arauserData, accessByRegionData, accessByZoneData, araProfileData, regionData, zoneData } from './datasourceBio';

@Component({
    templateUrl: './arausers.component.html',
    providers: [DetailRowService, ForeignKeyService, SelectionService],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class ArausersComponent extends AppComponentBase{

    @ViewChild('createOrEditArauserModal', { static: true }) createOrEditArauserModal: CreateOrEditArauserModalComponent;
    @ViewChild('viewArauserModalComponent', { static: true }) viewArauserModal: ViewArauserModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    maxuser_idFilter : number;
		maxuser_idFilterEmpty : number;
		minuser_idFilter : number;
		minuser_idFilterEmpty : number;
    user_nameFilter = '';
    user_real_nameFilter = '';
    user_emailFilter = '';
        araprofileprof_idFilter = '';

    
    @ViewChild('grid', {static: true}) public grid: GridComponent;
    public araUserGridData: object[];
    // public accessRegionGridData: object[];
    // public accessZoneGridData: object[];
    // public araProfileData: object[];
    // public regionData: object[];
    // public zoneData: object[];
    public filterOptions: FilterSettingsModel;
    public toolbarOptions: ToolbarItems[];
    public editSettings: EditSettingsModel;
    public orderData: object;
    // public key: string = null;
    // public headerText: object = [{ text: 'Access by region' }, { text: 'Access by zone' }];




    constructor(
        injector: Injector,
        private _arausersServiceProxy: ArausersServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getArausers(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._arausersServiceProxy.getAll(
            this.filterText,
            this.maxuser_idFilter == null ? this.maxuser_idFilterEmpty: this.maxuser_idFilter,
            this.minuser_idFilter == null ? this.minuser_idFilterEmpty: this.minuser_idFilter,
            this.user_nameFilter,
            this.user_real_nameFilter,
            this.user_emailFilter,
            this.araprofileprof_idFilter,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            console.log('result', result)
            console.log('result.items', result.items)
            this.araUserGridData = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createArauser(): void {
        this.createOrEditArauserModal.show();
    }

    deleteArauser(arauser: ArauserDto): void {
        this.message.confirm(
            '',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._arausersServiceProxy.delete(arauser.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._arausersServiceProxy.getArausersToExcel(
        this.filterText,
            this.maxuser_idFilter == null ? this.maxuser_idFilterEmpty: this.maxuser_idFilter,
            this.minuser_idFilter == null ? this.minuser_idFilterEmpty: this.minuser_idFilter,
            this.user_nameFilter,
            this.user_real_nameFilter,
            this.user_emailFilter,
            this.araprofileprof_idFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }

    // ngOnInit(): void {
    //     this.getArausers();
    //     // this.araUserGridData = arauserData;
    //     // this.accessRegionGridData = [];
    //     // this.accessZoneGridData = [];
    //     // this.araProfileData = araProfileData;
    //     // this.regionData = regionData;
    //     // this.zoneData = zoneData;

    //     // console.log(this.araUserGridData)

    //     this.filterOptions = { type: 'Excel' };
    //     this.toolbarOptions = ['Add', 'Edit', 'Delete', 'Update', 'Cancel', 'Search', 'ColumnChooser'];
    //     this.editSettings = { allowEditing: true, allowAdding: true, allowDeleting: true, mode: 'Normal', showDeleteConfirmDialog: true };
    // }

    dataBound() {
        console.log('entre');
        this._arausersServiceProxy.getAllNoFilter()
        .subscribe(result => {
            console.log('result', result);
            console.log('result.items', result.items);
            this.araUserGridData = JSON.parse(result.items)
        });
        // this.araUserGridData = arauserData;
        Object.assign((this.grid.filterModule as any).filterOperators, { startsWith: 'contains' });
    }

    // // public onRowSelected(args: RowSelectEventArgs): void {
    // //   console.log('entre')
    // //     this.key = args.data['USER_ID'];
    // //     this.accessRegionGridData = accessByRegionData.filter((data: any) => data.UserId === this.key);
    // //     this.accessZoneGridData = accessByZoneData.filter((data: any) => data.UserId === this.key);
    // // }

    // actionBegin(args: SaveEventArgs) {
    //     if (args.requestType === 'beginEdit' || args.requestType === 'add') {
    //         this.orderData = Object.assign({}, args.rowData);
    //     }
    //     if (args.requestType === 'save') {
    //         const OrderDate = 'OrderDate';
    //         args.data[OrderDate] = this.orderData[OrderDate];
    //     }
    // }
}
