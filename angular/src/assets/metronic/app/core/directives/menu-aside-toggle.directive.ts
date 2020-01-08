import { Directive, ElementRef, AfterViewInit } from '@angular/core';

@Directive({
    selector: '[ktMenuAsideToggle]'
})
export class MenuAsideToggleDirective implements AfterViewInit {
    toggle: any;
    constructor(private el: ElementRef) { }

    ngAfterViewInit(): void {
        this.toggle = new KTToggle(this.el.nativeElement, {
            target: 'body',
            targetState: 'kt-brand--minimize m-aside-left--minimize',
            togglerState: 'kt-brand__toggler--active'
        });
    }
}
