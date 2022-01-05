import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { API_BASE_URL } from 'src/constants';
@Injectable({
  providedIn: 'root'
})
export class AddeditService {
 constructor(private http:HttpClient) { }

  getManufacturers = () => {
    return this.http.get( API_BASE_URL + "Assets/manufacturers")
  }

  getModels = () => {
    return this.http.get( API_BASE_URL + "Assets/models")
  }

  getColors = () => {
    return this.http.get( API_BASE_URL + "Assets/colors")
  }

  addAsset(asset:any){
    return this.http.post(API_BASE_URL + 'Assets/',asset);
  }

  editAsset(asset:any){
    return this.http.put(API_BASE_URL + 'Assets/' + asset.id,asset);
  }

  
  getAssetById(assetId:any){
    return this.http.get(API_BASE_URL + 'Assets/' + assetId);
  }

}
