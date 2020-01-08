import { Directive, AfterViewInit, ElementRef } from '@angular/core';
import { filter } from 'rxjs/operators';
import { NavigationEnd, Router } from '@angular/router';

@Directive({
    selector: '[ktMenuAsideOffcanvas]'
})
export class MenuAsideOffcanvasDirective implements AfterViewInit {
    menuOffcanvas: any;

    constructor(
        private el: ElementRef,
        private router: Router
    ) {

    }

    ngAfterViewInit(): void {
        const offcanvasClass = KTUtil.hasClass(this.el.nativeElement, 
            'kt-aside-left--offcanvas-default') ? 'kt-aside-left--offcanvas-default' : 'kt-aside-left';

        this.menuOffcanvas = new KTOffcanvas(this.el.nativeElement, {
            baseClass: offcanvasClass,
            overlay: true,
            closeBy: 'kt_aside_left_close_btn',
            toggleBy: [{
                target: 'kt_aside_left_offcanvas_toggle',
                state: 'kt-brand__toggler--active'
            },
            {
                target: 'kt_aside_left_offcanvas_mobile_toggle',
                state: 'kt-brand__toggler--active'
            }]
        });

        this.router.events
            .pipe(filter(event => event instanceof NavigationEnd))
            .subscribe(event => {
                if (KTUtil.isMobileDevice()) {
                    this.menuOffcanvas.hide();
                }
            });
    }
}
