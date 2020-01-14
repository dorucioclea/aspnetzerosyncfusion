import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetAraprofileForViewDto, AraprofileDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewAraprofileModal',
    templateUrl: './view-araprofile-modal.component.html'
})
export class ViewAraprofileModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetAraprofileForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetAraprofileForViewDto();
        this.item.araprofile = new AraprofileDto();
    }

    show(item: GetAraprofileForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
