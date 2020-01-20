import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'limitTextSize',
  pure: true
})
export class LimitTextSizePipe implements PipeTransform {

  transform(value: string, maxLength: number): string {
    if(!value)
      return "";

    if(!maxLength)
      return value;
    
    if(value.length <= maxLength)
      return value;

    return value.substring(0, maxLength) + " ...";
  }

}
