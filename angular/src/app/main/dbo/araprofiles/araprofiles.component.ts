import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AraprofilesServiceProxy, AraprofileDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditAraprofileModalComponent } from './create-or-edit-araprofile-modal.component';
import { ViewAraprofileModalComponent } from './view-araprofile-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './araprofiles.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class AraprofilesComponent extends AppComponentBase {

    @ViewChild('createOrEditAraprofileModal', { static: true }) createOrEditAraprofileModal: CreateOrEditAraprofileModalComponent;
    @ViewChild('viewAraprofileModalComponent', { static: true }) viewAraprofileModal: ViewAraprofileModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    maxprof_idFilter : number;
		maxprof_idFilterEmpty : number;
		minprof_idFilter : number;
		minprof_idFilterEmpty : number;
    prof_descriptionFilter = '';




    constructor(
        injector: Injector,
        private _araprofilesServiceProxy: AraprofilesServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getAraprofiles(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._araprofilesServiceProxy.getAll(
            this.filterText,
            this.maxprof_idFilter == null ? this.maxprof_idFilterEmpty: this.maxprof_idFilter,
            this.minprof_idFilter == null ? this.minprof_idFilterEmpty: this.minprof_idFilter,
            this.prof_descriptionFilter,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createAraprofile(): void {
        this.createOrEditAraprofileModal.show();
    }

    deleteAraprofile(araprofile: AraprofileDto): void {
        this.message.confirm(
            '',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._araprofilesServiceProxy.delete(araprofile.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._araprofilesServiceProxy.getAraprofilesToExcel(
        this.filterText,
            this.maxprof_idFilter == null ? this.maxprof_idFilterEmpty: this.maxprof_idFilter,
            this.minprof_idFilter == null ? this.minprof_idFilterEmpty: this.minprof_idFilter,
            this.prof_descriptionFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
