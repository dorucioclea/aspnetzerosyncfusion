import { Directive, AfterViewInit, ElementRef } from '@angular/core';
import { filter } from 'rxjs/operators';
import { NavigationEnd, Router } from '@angular/router';

@Directive({
    selector: '[ktMenuHorizontalOffcanvas]'
})
export class MenuHorizontalOffcanvasDirective implements AfterViewInit {
    menuOffcanvas: any;

    constructor(private el: ElementRef,
        private router: Router) { }

    ngAfterViewInit(): void {
        // init the mOffcanvas plugin
        this.menuOffcanvas = new KTOffcanvas(this.el.nativeElement, {
            overlay: true,
            baseClass: 'kt-aside-header-menu-mobile',
            closeBy: 'kt_aside_header_menu_mobile_close_btn',
            toggleBy: {
                target: 'kt_aside_header_menu_mobile_toggle',
                state: 'kt-brand__toggler--active'
            }
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
