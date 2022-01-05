import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

 readonly rootURL = "https://localhost:44324/api"


  constructor(private http: HttpClient) { }

  getAllAssets(): Observable<any> {
 return this.http.get(this.rootURL + '/Assets/');
}

getAssets(rowsPerPage:number,pageNumber:number): Observable<any> {
  return this.http.get(this.rootURL + '/Assets/filter/?recordsPerPage=' + rowsPerPage +'&page=' + pageNumber);
}

getTotalRecords():Observable<any> {
  return this.http.get(this.rootURL + "/Assets/totalRecords")
}


}
