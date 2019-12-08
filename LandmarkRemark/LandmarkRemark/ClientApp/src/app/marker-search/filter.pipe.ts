import { Pipe, PipeTransform } from '@angular/core';
import { Marker } from '../landmarkremark/Marker';
@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {
  transform(items: Marker[], searchText: string): any[] {
    if(!items) return [];
    if(!searchText) return items;
searchText = searchText.toLowerCase();
return items.filter( it => {
      return it.note.toLowerCase().includes(searchText) || it.user.username.toLowerCase().includes(searchText);
    });
   }
}