import { Component, Input, OnInit } from "@angular/core";
import { DatesModel } from "src/app/models/dates.model";
import { SearchModel } from "src/app/models/search.model";
import { Countrys } from "../../models/countrys.model";
import { BankHolidayService } from "../../services/bank-holiday.service";

@Component({
    selector: 'bh-search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
    error: string = '';
    countrys : Countrys[] = [];
    searchModel : SearchModel = new SearchModel();
    dates : DatesModel[];

    constructor(private bankHolidayService: BankHolidayService) {

    }

    ngOnInit(): void {
        this.bankHolidayService
            .getCountrys()
            .subscribe({
                next: (countrys : Countrys[]) => {
                    this.countrys = countrys;
                },
                error: (err) => {
                    this.error = err;
                }
            });
    }

    getDates() {
        if (!this.validateFields()) {
            this.bankHolidayService
                .getDates(this.searchModel)
                .subscribe({
                    next: (dates : DatesModel[]) => {
                        this.dates = dates;
                    },
                    error: (err) => {
                        this.error = err;
                    }
                });
        }
    }

    validateFields() {
        if (this.searchModel.date == null || !this.searchModel.date) {
            this.error = 'Invalid date';
            return true;
        }

        if (this.searchModel.region == null || !this.searchModel.region) {
            this.error = 'Invalid region';
            return true;
        }

        return false;
    }
}