import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-about-modal',
  templateUrl: './about-modal.component.html',
  styleUrls: ['./about-modal.component.scss']
})
export class AboutModalComponent implements OnInit {

  constructor() {
  }

  ngOnInit(): void {
  }

  handleClickKnowMore() {
    window.open("https://www.solidarites-nouvelles-logement.org/")
  }

  handleClickInvest() {
    window.open("https://www.investirsolidaire.fr/")
  }

}
