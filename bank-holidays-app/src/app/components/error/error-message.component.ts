import { Component, OnInit, Input } from "@angular/core";

@Component({
    selector: 'bh-error-message',
    templateUrl: './error-message.component.html',
    styleUrls: ['./error-message.component.css']
})
export class ErrorMessageComponent implements OnInit {
    
    @Input() error : string = '';

    constructor() {

    }

    ngOnInit(): void {

    }    
}