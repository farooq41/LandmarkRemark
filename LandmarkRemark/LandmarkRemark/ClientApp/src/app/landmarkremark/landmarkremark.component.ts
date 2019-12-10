import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from '../login/login.component';
import { Marker } from './Marker';

declare var google: any;

@Component({
  selector: 'app-landmarkremark',
  templateUrl: './landmarkremark.component.html',
  styleUrls: ['./landmarkremark.component.css']
})
export class LandmarkremarkComponent implements OnInit {

  lat: number; // current map location in view
  lng: number; // current map location in view
  zoom: 14;
  map: any;
  user: User = JSON.parse(localStorage.getItem('user'));
  markers: Marker[];
  currentMarker ={latitude:0, longitude:0, note:"", user:this.user, id:0, createdDate:null, userId:0, open:false};
  currentMarkerExists: boolean;
  constructor(private route: ActivatedRoute, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router) { }

  ngOnInit() {
    this.getCurrentLocation(); // Get current location set to current view on map
    this.getMarkers();
    
  }
  getCurrentLocation() {
    var options = {
      enableHighAccuracy: true,
      maximumAge: 0
    };
    if (navigator.geolocation) {
      navigator.geolocation.watchPosition(location => {
        this.lat = parseFloat(location.coords.latitude.toFixed(4));
        this.lng = parseFloat(location.coords.longitude.toFixed(4));
        this.currentMarker ={latitude:this.lat, longitude:this.lng, note:"", user:this.user, id:0, createdDate:null, userId:0, open:false};
      },error=>{},options);
    } else {
    }
  }
  getMarkers(){
    const httpOptions = {

      headers: new HttpHeaders({

          'Content-Type': 'application/json'

      })
    };
    this.http.get<any>(this.baseUrl + 'api/remark/allRemarks',httpOptions).subscribe(result => {

      this.markers = result;
      this.filterMarkers();

  }, error => console.error(error));
  }

  // filter any current user location marker
  filterMarkers() {
    // find markers in this location for current user
    const idx = this.markers.findIndex(m => m.user.username === this.user.username && m.latitude === this.lat && m.longitude === this.lng);
    if (idx + 1) {
      // if there is one, save that info
      this.currentMarkerExists = true;
      this.markers[idx]['current'] = true;
    } else {
      this.currentMarkerExists = false;
    }
  }
  addNote(){
    const httpOptions = {

      headers: new HttpHeaders({

          'Content-Type': 'application/json'

      })
    };
    this.currentMarker.createdDate = new Date();
    this.http.post<Marker>(this.baseUrl + 'api/remark/remark', this.currentMarker, httpOptions).subscribe(result => {

      this.markers.push(this.currentMarker);
      this.markers[this.markers.length-1]['current'] = true;
      this.currentMarker.open = false;
      this.currentMarkerExists = true;
   

  }, error => console.error(error));    
  }

  loadMap(map:any){
    this.map=map;
  }

  openMarker(marker: Marker){
    this.markers.forEach(m => m.open = false);
    marker.open = true;
  }

  onChoseSearchedMarker(marker: Marker) {
    // get target position
    const position = new google.maps.LatLng(marker.latitude, marker.longitude);
    // pan to marker and open it
    this.map.panTo(position);
    this.openMarker(marker);
  }
}
