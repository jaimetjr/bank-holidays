import { Component, OnInit } from "@angular/core";
import { environment } from "src/environments/environment";

@Component({
    selector: 'bh-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

    constructor() {
        console.log(environment.url);
    }

    ngOnInit(): void {
        
    }
}