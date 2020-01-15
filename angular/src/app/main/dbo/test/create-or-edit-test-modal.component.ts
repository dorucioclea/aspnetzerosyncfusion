import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { TestServiceProxy, CreateOrEditTestDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditTestModal',
    templateUrl: './create-or-edit-test-modal.component.html'
})
export class CreateOrEditTestModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    test: CreateOrEditTestDto = new CreateOrEditTestDto();



    constructor(
        injector: Injector,
        private _testServiceProxy: TestServiceProxy
    ) {
        super(injector);
    }

    show(testId?: number): void {

        if (!testId) {
            this.test = new CreateOrEditTestDto();
            this.test.id = testId;

            this.active = true;
            this.modal.show();
        } else {
            this._testServiceProxy.getTestForEdit(testId).subscribe(result => {
                this.test = result.test;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._testServiceProxy.createOrEdit(this.test)
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
