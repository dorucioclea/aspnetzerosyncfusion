import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { GridComponent, DetailRowService, FilterSettingsModel, ToolbarItems, EditSettingsModel, SaveEventArgs, ForeignKeyService, SelectionService, RowSelectEventArgs } from '@syncfusion/ej2-angular-grids';
import { arauserData, accessByRegionData, accessByZoneData, araProfileData, regionData, zoneData } from './datasourceBio';

@Component({
    selector: 'app-gridTest',
    templateUrl: './gridTest.component.html',
    providers: [DetailRowService, ForeignKeyService, SelectionService],
    animations: [appModuleAnimation()]
})
export class GridTestComponent extends AppComponentBase implements OnInit {

    @ViewChild('grid', {static: true}) public grid: GridComponent;
    public araUserGridData: object[];
    public accessRegionGridData: object[];
    public accessZoneGridData: object[];
    public araProfileData: object[];
    public regionData: object[];
    public zoneData: object[];
    public filterOptions: FilterSettingsModel;
    public toolbarOptions: ToolbarItems[];
    public editSettings: EditSettingsModel;
    public orderData: object;
    public key: string = null;
    public headerText: object = [{ text: 'Access by region' }, { text: 'Access by zone' }];

    constructor(injector: Injector, private router: Router) {
        super(injector);
    }

    ngOnInit(): void {
        this.araUserGridData = arauserData;
        this.accessRegionGridData = [];
        this.accessZoneGridData = [];
        this.araProfileData = araProfileData;
        this.regionData = regionData;
        this.zoneData = zoneData;

        console.log(this.araUserGridData)

        this.filterOptions = { type: 'Excel' };
        this.toolbarOptions = ['Add', 'Edit', 'Delete', 'Update', 'Cancel', 'Search', 'ColumnChooser'];
        this.editSettings = { allowEditing: true, allowAdding: true, allowDeleting: true, mode: 'Normal', showDeleteConfirmDialog: true };
    }

    dataBound() {
        this.araUserGridData = arauserData;
        Object.assign((this.grid.filterModule as any).filterOperators, { startsWith: 'contains' });
    }

    public onRowSelected(args: RowSelectEventArgs): void {
      console.log('entre')
        this.key = args.data['USER_ID'];
        this.accessRegionGridData = accessByRegionData.filter((data: any) => data.UserId === this.key);
        this.accessZoneGridData = accessByZoneData.filter((data: any) => data.UserId === this.key);
    }

    actionBegin(args: SaveEventArgs) {
        if (args.requestType === 'beginEdit' || args.requestType === 'add') {
            this.orderData = Object.assign({}, args.rowData);
        }
        if (args.requestType === 'save') {
            const OrderDate = 'OrderDate';
            args.data[OrderDate] = this.orderData[OrderDate];
        }
    }
}