import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { TestTablesServiceProxy, CreateOrEditTestTableDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditTestTableModal',
    templateUrl: './create-or-edit-testTable-modal.component.html'
})
export class CreateOrEditTestTableModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    testTable: CreateOrEditTestTableDto = new CreateOrEditTestTableDto();



    constructor(
        injector: Injector,
        private _testTablesServiceProxy: TestTablesServiceProxy
    ) {
        super(injector);
    }

    show(testTableId?: number): void {

        if (!testTableId) {
            this.testTable = new CreateOrEditTestTableDto();
            this.testTable.id = testTableId;

            this.active = true;
            this.modal.show();
        } else {
            this._testTablesServiceProxy.getTestTableForEdit(testTableId).subscribe(result => {
                this.testTable = result.testTable;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._testTablesServiceProxy.createOrEdit(this.testTable)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }







    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
