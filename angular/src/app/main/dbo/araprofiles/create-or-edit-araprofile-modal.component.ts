import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { AraprofilesServiceProxy, CreateOrEditAraprofileDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditAraprofileModal',
    templateUrl: './create-or-edit-araprofile-modal.component.html'
})
export class CreateOrEditAraprofileModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    araprofile: CreateOrEditAraprofileDto = new CreateOrEditAraprofileDto();



    constructor(
        injector: Injector,
        private _araprofilesServiceProxy: AraprofilesServiceProxy
    ) {
        super(injector);
    }

    show(araprofileId?: number): void {

        if (!araprofileId) {
            this.araprofile = new CreateOrEditAraprofileDto();
            this.araprofile.id = araprofileId;

            this.active = true;
            this.modal.show();
        } else {
            this._araprofilesServiceProxy.getAraprofileForEdit(araprofileId).subscribe(result => {
                this.araprofile = result.araprofile;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._araprofilesServiceProxy.createOrEdit(this.araprofile)
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
