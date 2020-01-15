import { Component, Injector, ViewEncapsulation, ViewChild, OnInit, ElementRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ResourcesServiceProxy, ResourceDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditResourceModalComponent } from './create-or-edit-resource-modal.component';
import { ViewResourceModalComponent } from './view-resource-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';
import { GridComponent, DetailRowService, FilterSettingsModel, SelectionSettingsModel, ToolbarItems, EditSettingsModel, SaveEventArgs, ForeignKeyService, SelectionService, RowSelectEventArgs } from '@syncfusion/ej2-angular-grids';
import { finalize } from 'rxjs/operators';
import { DialogComponent } from '@syncfusion/ej2-angular-popups';
import { EmitType, isNullOrUndefined } from '@syncfusion/ej2-base';

@Component({
    templateUrl: './resources.component.html',
    providers: [DetailRowService, ForeignKeyService, SelectionService],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class ResourcesComponent extends AppComponentBase implements OnInit {

    @ViewChild('createOrEditResourceModal', { static: true }) createOrEditResourceModal: CreateOrEditResourceModalComponent;
    @ViewChild('viewResourceModalComponent', { static: true }) viewResourceModal: ViewResourceModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    @ViewChild('parentGrid', {static: true}) public parentGrid: GridComponent;

    @ViewChild('template', {static: true}) template: DialogComponent;
    // Create element reference for dialog target element.
    @ViewChild('container', {static: true}) container: ElementRef;
    // The Dialog shows within the target element.
    public targetElement: HTMLElement;
    public proxy: any = this;

    advancedFiltersAreShown = false;
    filterText = '';
    nameFilter = '';
    codeFilter = '';
    public parentGridData: object[];
    public filterOptions: FilterSettingsModel;
    public toolbarOptions: ToolbarItems[];
    public editSettings: EditSettingsModel;
    public selectionSettings: SelectionSettingsModel;
    public orderData: object;

    constructor(
        injector: Injector,
        private _resourcesServiceProxy: ResourcesServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    ngOnInit(): void {

        this.initilaizeTarget();

        this.filterOptions = { type: 'Excel' };
        this.toolbarOptions = ['Add', 'Edit', 'Delete', 'Update', 'Cancel', 'Search', 'ColumnChooser'];
        this.editSettings = { allowEditing: true, allowAdding: true, allowDeleting: true, mode: 'Normal', showDeleteConfirmDialog: true };
        this.selectionSettings = { mode: 'Row', enableToggle: true, type: 'Multiple', enableSimpleMultiRowSelection: true };
    }

    dataBound(event) {
        Object.assign((this.parentGrid.filterModule as any).filterOperators, { startsWith: 'contains' });
    }

    actionComplete(event){
        if ((event.action === 'add' || event.action === 'edit') && event.requestType === 'save'){
            this.createResource(event.data);
        }
        else if(event.requestType === 'delete'){
            console.log(event)
            this.deleteResource(event.data[0].id);
        }
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createResource(resource): void {
        this._resourcesServiceProxy.createOrEdit(resource)
            .pipe(finalize(() => false))
            .subscribe(result => {
                console.log(result);
            this.notify.info(this.l('SavedSuccessfully'));
            });
    }

    deleteResource(id: number): void {
        this._resourcesServiceProxy.delete(id)
            .subscribe(result => {
                this.notify.success(this.l('SuccessfullyDeleted'));
            });
    }

    exportToExcel(): void {
        this._resourcesServiceProxy.getResourcesToExcel(
        this.filterText,
            this.nameFilter,
            this.codeFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }

    // showFilters(){
    //     // this.createOrEditResourceModal.show();
    //     this.onOpenDialog();
    //     // this.filter();
    // }

    closeModal(){
        this.template.hide();
    }

    onClose(){
        this.filter();
    }

    filter(){
        let enteredVal = (document.getElementById('inVal') as HTMLInputElement).value;

        console.log(enteredVal);

        this._resourcesServiceProxy.getAll('', enteredVal, '', undefined, 0, 10)
        .subscribe(result => {
            this.parentGridData = result.items.map(item => item.resource);
        });
    }

    public initilaizeTarget: EmitType<object> = () => {
      this.targetElement = this.container.nativeElement.parentElement;
    }

    // public dialogOpen: EmitType<object> = () => {
    //     (document.getElementById('sendButton') as any).keypress = (e: any) => {
    //         if (e.keyCode === 13) { this.updateTextValue(); }
    //     };
    //     (document.getElementById('inVal')as HTMLElement).onkeydown = (e: any) => {
    //         if (e.keyCode === 13) { this.updateTextValue(); }
    //     };
    //     document.getElementById('sendButton').onclick = (): void => {
    //         this.updateTextValue();
    //     };
    // }

    // public updateTextValue: EmitType<object> = () => {
    //     let enteredVal: HTMLInputElement = document.getElementById('inVal') as HTMLInputElement;
    //     let dialogTextElement: HTMLElement = document.getElementsByClassName('dialogText')[0] as HTMLElement;
    //     let dialogTextWrap : HTMLElement = document.getElementsByClassName('dialogContent')[0] as HTMLElement;
    //     if (!isNullOrUndefined(document.getElementsByClassName('contentText')[0])) {
    //         detach(document.getElementsByClassName('contentText')[0]);
    //     }
    //     if (enteredVal.value !== '') {
    //         dialogTextElement.innerHTML = enteredVal.value;
    //     }
    //     enteredVal.value = '';
    // }

    public onOpenDialog = function(event: any): void {
        this.template.show();
    }
}