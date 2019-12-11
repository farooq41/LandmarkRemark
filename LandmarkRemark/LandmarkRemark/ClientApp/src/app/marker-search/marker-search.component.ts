import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Marker } from '../landmarkremark/Marker';
import { User } from '../login/login.component';

@Component({
  selector: 'app-marker-search',
  templateUrl: './marker-search.component.html',
  styleUrls: ['./marker-search.component.css']
})
export class MarkerSearchComponent implements OnInit {
  @Input() markers: Marker[];
  @Output() selectedMarker = new EventEmitter<Marker>();
  searchText: string;
  user: User = JSON.parse(localStorage.getItem('user'));
  constructor() { }

  ngOnInit() {
  }

  //selected marker is sent back to the parent component to be highlighted on the map
  selectMarker(marker: Marker) {

    //emit selecetd marker
    this.selectedMarker.emit(marker);
  }

}
