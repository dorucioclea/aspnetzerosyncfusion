import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { ArausersServiceProxy, CreateOrEditArauserDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { ArauserAraprofileLookupTableModalComponent } from './arauser-araprofile-lookup-table-modal.component';

@Component({
    selector: 'createOrEditArauserModal',
    templateUrl: './create-or-edit-arauser-modal.component.html'
})
export class CreateOrEditArauserModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('arauserAraprofileLookupTableModal', { static: true }) arauserAraprofileLookupTableModal: ArauserAraprofileLookupTableModalComponent;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    arauser: CreateOrEditArauserDto = new CreateOrEditArauserDto();

    araprofileprof_id = '';


    constructor(
        injector: Injector,
        private _arausersServiceProxy: ArausersServiceProxy
    ) {
        super(injector);
    }

    show(arauserId?: number): void {

        if (!arauserId) {
            this.arauser = new CreateOrEditArauserDto();
            this.arauser.id = arauserId;
            this.araprofileprof_id = '';

            this.active = true;
            this.modal.show();
        } else {
            this._arausersServiceProxy.getArauserForEdit(arauserId).subscribe(result => {
                this.arauser = result.arauser;

                this.araprofileprof_id = result.araprofileprof_id;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._arausersServiceProxy.createOrEdit(this.arauser)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

    openSelectAraprofileModal() {
        this.arauserAraprofileLookupTableModal.id = this.arauser.prof_id;
        this.arauserAraprofileLookupTableModal.displayName = this.araprofileprof_id;
        this.arauserAraprofileLookupTableModal.show();
    }


    setprof_idNull() {
        this.arauser.prof_id = null;
        this.araprofileprof_id = '';
    }


    getNewprof_id() {
        this.arauser.prof_id = this.arauserAraprofileLookupTableModal.id;
        this.araprofileprof_id = this.arauserAraprofileLookupTableModal.displayName;
    }


    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
