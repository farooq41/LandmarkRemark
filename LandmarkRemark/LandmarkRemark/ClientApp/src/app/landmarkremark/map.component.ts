import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { GoogleMapsAPIWrapper } from '@agm/core';

@Component({
  selector: 'app-map',
  template: '',
})
export class MapContentComponent implements OnInit {

  @Output() loadMap: EventEmitter<{}> = new EventEmitter<{}>();

  constructor(public googleMaps: GoogleMapsAPIWrapper) {}

  // on init, access native map and emit it to map component
  ngOnInit() {
    this.googleMaps.getNativeMap().then((map) => {
      this.loadMap.emit(map);
    });
  }
}
