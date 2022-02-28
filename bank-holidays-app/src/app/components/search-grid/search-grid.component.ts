import { Component, Input, OnInit, Output } from "@angular/core";
import { DatesModel } from "src/app/models/dates.model";
import { registerLocaleData } from '@angular/common';
import localeES from "@angular/common/locales/es";
registerLocaleData(localeES, "es");
import { formatDate } from "@angular/common";

@Component({
    selector: 'bh-search-grid',
    templateUrl: './search-grid.component.html',
    styleUrls: ['./search-grid.component.css']
})
export class SearchGridComponent implements OnInit {
    @Input() dates: DatesModel[];
    constructor() {

    }

    ngOnInit(): void {
    }

    customFormatDate(date : Date) : string {
        const format = 'dd/MM/yyyy';
        const locale = 'en-US';
        return formatDate(date, format, locale);
    }

    setDayOfWeek(dayOfWeek: number): string {
        switch (dayOfWeek) {
            case 0:
                return 'Sunday';
            case 1:
                return 'Monday';
            case 2:
                return 'Tuesday';
            case 3:
                return 'Thursday';
            case 4:
                return 'Thursday';
            case 5:
                return 'Friday';
            case 6:
                return 'Saturday';
        }

        return '';
    }
}