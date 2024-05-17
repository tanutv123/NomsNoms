import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'truncate'
})
export class TruncatePipe implements PipeTransform {

  transform(value: string, maxLength: number): string {
    if (!value) return '';

    const words = value.split(' ');
    if (words.length <= maxLength) {
      return value;
    } else {
      return words.slice(0, maxLength).join(' ') + '...';
    }
  }

}
