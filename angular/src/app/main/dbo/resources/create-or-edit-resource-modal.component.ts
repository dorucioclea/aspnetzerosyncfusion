import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { ResourcesServiceProxy, CreateOrEditResourceDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditResourceModal',
    templateUrl: './create-or-edit-resource-modal.component.html'
})
export class CreateOrEditResourceModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    resource: CreateOrEditResourceDto = new CreateOrEditResourceDto();



    constructor(
        injector: Injector,
        private _resourcesServiceProxy: ResourcesServiceProxy
    ) {
        super(injector);
    }

    show(resourceId?: number): void {
        this.active = true;
        this.modal.show();
        // if (!resourceId) {
        //     this.resource = new CreateOrEditResourceDto();
        //     this.resource.id = resourceId;

        //     this.active = true;
        //     this.modal.show();
        // } else {
        //     this._resourcesServiceProxy.getResourceForEdit(resourceId).subscribe(result => {
        //         this.resource = result.resource;


        //         this.active = true;
        //         this.modal.show();
        //     });
        // }
    }

    save(): void {
            this.saving = true;
            this.saving = false;
            this.close();
            this.modalSave.emit(null);
			
            // this._resourcesServiceProxy.createOrEdit(this.resource)
            //  .pipe(finalize(() => { this.saving = false;}))
            //  .subscribe(() => {
            //     this.notify.info(this.l('SavedSuccessfully'));
            //     this.close();
            //     this.modalSave.emit(null);
            //  });
    }







    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
