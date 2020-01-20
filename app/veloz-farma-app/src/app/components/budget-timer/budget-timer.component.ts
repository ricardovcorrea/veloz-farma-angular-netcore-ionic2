import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';

import { LeftZeroesPipe } from '../../pipes/left-zeroes-pipe';

@Component({
  selector: 'budget-timer',
  templateUrl: '/budget-timer.component.html',
  styleUrls: ['/budget-timer.component.scss'],
  providers:[ LeftZeroesPipe ]
})
export class BudgetTimerComponent implements OnInit{
  private baseInterval: number;
  private toCount: number;
  private counted: number;
  private countedString: string;
  
  public countChartData:number[];
  public options = { cutoutPercentage: 50 };


  @Input() secsTickSize: number;
  @Input() secsTotalTime: number;
  @Input() itsCountDown: boolean;

  @Output() tickEvent : EventEmitter<any> = new EventEmitter();
  @Output() finishedEvent : EventEmitter<any> = new EventEmitter();

  constructor(private leftPad: LeftZeroesPipe) {}

  ngOnInit()
  {
    this.secsTickSize = this.secsTickSize || 1;
    this.secsTotalTime = this.secsTotalTime || 300;
    this.itsCountDown = this.itsCountDown || true;
    this.reset();
  }

  public reset() : void
  {
    clearInterval(this.baseInterval);
    this.toCount = this.secsTotalTime;
    this.counted = this.itsCountDown ? this.secsTotalTime : 0;
    this.refreshCountedString();
    this.baseInterval = setInterval(this.tick.bind(this), this.secsTickSize * 1000);
  }

  private tick() : void
  {
      this.counted = this.itsCountDown ? this.decreaseCount() : this.increaseCount();
      
      this.refreshCountedString();

      if(this.itsCountDown && this.counted <= 0)
      {
        this.finishedEvent.emit();
        clearInterval(this.baseInterval);
      }
      else if(!this.itsCountDown && this.counted >= this.secsTotalTime)
      {
        this.finishedEvent.emit();
        clearInterval(this.baseInterval);
      }
  }

  private refreshCountedString()
  {
    let minutes = this.counted > 60 ? Math.floor(this.counted / 60) : 0;
    let seconds = this.counted - minutes * 60;
    this.countedString = this.leftPad.transform(minutes.toString(), 1) + ":" + this.leftPad.transform(seconds.toString(), 1);
    this.countChartData = [this.toCount - this.counted, this.counted];
  }

  private getPercetageProgress() : Number
  {
    return 100 - (100 * this.counted) / this.toCount;
  }

  private increaseCount() : number
  {
    this.counted += this.secsTickSize;;
    this.tickEvent.emit();
    return this.counted;
  }

  private decreaseCount() : number
  {
    this.counted -= this.secsTickSize; 
    this.tickEvent.emit();
    return this.counted;
  }
}
