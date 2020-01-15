import { Component, Injector, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
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
import { GridComponent, DetailRowService, FilterSettingsModel, SelectionSettingsModel, ToolbarItems, EditSettingsModel, SaveEventArgs, ForeignKeyService, SelectionService, RowSelectEventArgs } from '@syncfusion/ej2-angular-grids';
// import { arauserData, accessByRegionData, accessByZoneData, araProfileData, regionData, zoneData } from './datasourceBio';
import { finalize } from 'rxjs/operators';

@Component({
    templateUrl: './arausers.component.html',
    providers: [DetailRowService, ForeignKeyService, SelectionService],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class ArausersComponent extends AppComponentBase implements OnInit {

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
    public selectionSettings: SelectionSettingsModel;
    public orderData: object;
    // public key: string = null;
    // public headerText: object = [{ text: 'Access by region' }, { text: 'Access by zone' }];
    saving = false;

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

    // getArausers(event?: LazyLoadEvent) {
    //     if (this.primengTableHelper.shouldResetPaging(event)) {
    //         this.paginator.changePage(0);
    //         return;
    //     }

    //     this.primengTableHelper.showLoadingIndicator();

    //     this._arausersServiceProxy.getAll(
    //         this.filterText,
    //         this.maxuser_idFilter == null ? this.maxuser_idFilterEmpty: this.maxuser_idFilter,
    //         this.minuser_idFilter == null ? this.minuser_idFilterEmpty: this.minuser_idFilter,
    //         this.user_nameFilter,
    //         this.user_real_nameFilter,
    //         this.user_emailFilter,
    //         this.araprofileprof_idFilter,
    //         this.primengTableHelper.getSorting(this.dataTable),
    //         this.primengTableHelper.getSkipCount(this.paginator, event),
    //         this.primengTableHelper.getMaxResultCount(this.paginator, event)
    //     ).subscribe(result => {
    //         this.primengTableHelper.totalRecordsCount = result.totalCount;
    //         this.primengTableHelper.records = result.items;
    //         this.araUserGridData = result.items;
    //         this.primengTableHelper.hideLoadingIndicator();
    //     });
    // }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    // createArauser(): void {
    //     this.createOrEditArauserModal.show();
    // }

    // deleteArauser(arauser: ArauserDto): void {

    //     this.message.confirm(
    //         '',
    //         this.l('AreYouSure'),
    //         (isConfirmed) => {
    //             if (isConfirmed) {
    //                 this._arausersServiceProxy.delete(arauser.id)
    //                     .subscribe(() => {
    //                         this.reloadPage();
    //                         this.notify.success(this.l('SuccessfullyDeleted'));
    //                     });
    //             }
    //         }
    //     );
    // }

    // exportToExcel(): void {
    //     this._arausersServiceProxy.getArausersToExcel(
    //     this.filterText,
    //         this.maxuser_idFilter == null ? this.maxuser_idFilterEmpty: this.maxuser_idFilter,
    //         this.minuser_idFilter == null ? this.minuser_idFilterEmpty: this.minuser_idFilter,
    //         this.user_nameFilter,
    //         this.user_real_nameFilter,
    //         this.user_emailFilter,
    //         this.araprofileprof_idFilter,
    //     )
    //     .subscribe(result => {
    //         this._fileDownloadService.downloadTempFile(result);
    //      });
    // }

    ngOnInit(): void {

        // this.createOrEditArauserModal.show();

        this._arausersServiceProxy.getAll('', undefined, undefined, '', '', '', '', undefined, 0, 10)
        .subscribe(result => {
            this.araUserGridData = result.items.map(item => item.arauser);
        });

        this.filterOptions = { type: 'Excel' };
        this.toolbarOptions = ['Add', 'Edit', 'Delete', 'Update', 'Cancel', 'Search', 'ColumnChooser'];
        this.editSettings = { allowEditing: true, allowAdding: true, allowDeleting: true, mode: 'Normal', showDeleteConfirmDialog: true };
        this.selectionSettings = { mode: 'Row', enableToggle: true, type: 'Multiple', enableSimpleMultiRowSelection: true };
    }

    dataBound(event) {
        Object.assign((this.grid.filterModule as any).filterOperators, { startsWith: 'contains' });
    }

    actionComplete(event){
        console.info('entre updateArauser');
        console.log(event);
        console.log(event.data);

        if ((event.action === 'add' || event.action === 'edit') && event.requestType === 'save'){
            this.createArauser(event.data);
        }
        else if(event.requestType === 'delete'){
            this.deleteArauser(event.data[0].id)
        }
    }

    createArauser(arauser){
        this.saving = true;
        this._arausersServiceProxy.createOrEdit(arauser)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(result => {
                 console.log(result);
                this.notify.info(this.l('SavedSuccessfully'));
             });
    }

    deleteArauser(id: number): void {
        this._arausersServiceProxy.delete(id)
                        .subscribe(result => {
                            console.log(result);
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
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
