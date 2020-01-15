import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetResourceForViewDto, ResourceDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewResourceModal',
    templateUrl: './view-resource-modal.component.html'
})
export class ViewResourceModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetResourceForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetResourceForViewDto();
        this.item.resource = new ResourceDto();
    }

    show(item: GetResourceForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
