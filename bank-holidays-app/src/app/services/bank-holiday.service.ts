import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { environment } from "src/environments/environment";
import { Countrys } from "../../app/models/countrys.model";
import { Observable } from "rxjs";
import { SearchModel } from "../models/search.model";
import { DatesModel } from "../models/dates.model";

@Injectable({
    providedIn: 'root'
})
export class BankHolidayService {
    
    constructor(private httpClient: HttpClient) {

    }

    getCountrys() : Observable<Countrys[]> {
        return this.httpClient.get<Countrys[]>(environment.url + 'BankHoliday/GetCountrys');
    }

    getDates(search : SearchModel) {
        return this.httpClient.post<DatesModel[]>(environment.url + 'BankHoliday/GetBankHolidayByRegionAndDate', {
            Date: search.date,
            Region: search.region
        });
    }
}