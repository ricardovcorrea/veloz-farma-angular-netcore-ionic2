import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'leftZeroes',
  pure: true
})
export class LeftZeroesPipe implements PipeTransform {

  transform(value: string, length: number): string {
    
    if(!value)
      return "";

    if(!length)
      return value;

    if(value.toString().length === 1)  
      return "0" + value;
    else
      return value;
  }

}
