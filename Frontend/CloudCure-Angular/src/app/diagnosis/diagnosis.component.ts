import { Component, OnInit } from '@angular/core';
import { Clickable } from '../body-clicker/body-clicker.component';

@Component({
  selector: 'app-diagnosis',
  templateUrl: './diagnosis.component.html',
  styleUrls: ['./diagnosis.component.css']
})
export class DiagnosisComponent implements OnInit {

  //variables
  showBodyClicker: boolean;
  bodypartshurt: string = "";

  constructor(){
    this.showBodyClicker = true;
  }

  ngOnInit(): void{

  }

  getClick(bodypart: Clickable) {
    console.log(`Clicked on ${bodypart.name}`)
  }
  onShowBodyClickerClicked(){
    this.showBodyClicker = !this.showBodyClicker;
  }

}
