import { ConfigurationProvider } from './../../providers/configuration/configuration';
import { Component, ViewChild, trigger, transition, style, state, animate, keyframes } from '@angular/core';
import { NavController, Slides, IonicPage } from 'ionic-angular';

@IonicPage({ name: 'page-tutorial' })
@Component({
  selector: 'page-tutorial',
  templateUrl: 'tutorial.html',
  animations: [

    trigger('bounce', [
      state('*', style({
        transform: 'translateX(0)'
      })),
      transition('* => rightSwipe', animate('700ms ease-out', keyframes([
        style({transform: 'translateX(0)', offset: 0}),
        style({transform: 'translateX(-65px)', offset: .3}),
        style({transform: 'translateX(0)', offset: 1})
      ]))),
      transition('* => leftSwipe', animate('700ms ease-out', keyframes([
        style({transform: 'translateX(0)', offset: 0}),
        style({transform: 'translateX(65px)', offset: .3}),
        style({transform: 'translateX(0)', offset: 1})
      ])))
    ])
  ]
})
export class TutorialPage {
  @ViewChild(Slides) slides: Slides;
  skipMsg: string = "Skip";
  state: string = 'x';

  constructor(public navCtrl: NavController, private configurationProvider: ConfigurationProvider) {
    this.configurationProvider.DebugLogAction("Page tutorial created");

    this.configurationProvider.menuController.enable(false);
  }

  skip() {

    this.configurationProvider.DebugLogAction("Skip tutorial action");
    this.navCtrl.setRoot("page-location", {CanClose: false});

  }

  slideChanged() {
    if (this.slides.isEnd()) {
      this.configurationProvider.DebugLogAction("Tutorial last step");
      this.skipMsg = "Ok, entendi!";
    }
      
  }

  slideMoved() {
    if (this.slides.getActiveIndex() >= this.slides.getPreviousIndex()) {

      this.configurationProvider.DebugLogAction("Slide swipe right");

      this.state = 'rightSwipe';
    } else {
      this.configurationProvider.DebugLogAction("Slide swipe left");

      this.state = 'leftSwipe';
    }
      
  }

  animationDone() {
    this.state = 'x';
  }

}