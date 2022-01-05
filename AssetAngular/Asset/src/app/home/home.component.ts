import { Component, OnInit,Output } from '@angular/core';

import { Observable } from 'rxjs';
import { HomeService } from './home.service';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],

})
export class HomeComponent implements OnInit {



  constructor( private homeService:HomeService) { }

  assetList: any = [];
  ModalTitle:string | undefined;
  ActivateAddEditComp: boolean=false;
  asset:any;
  editAssetId :any

  
  action = "ADD"
  displayStyle = "none";

  rowsPerPage: number = 5
  totalPages:number =0
  currentPageNumber:number =1
  pageNumberArray:any = []
  totalRecords:number = 0

  ngOnInit(): void {

  this.getTotalRecords()
  this.currentPageNumber = 1
  this.getAssets()
  }

  getTotalRecords() {

   this.homeService.getTotalRecords().subscribe({
      next:(data) => {
       this.totalRecords = parseFloat(data)
        this.totalPages = Math.ceil(this.totalRecords/this.rowsPerPage)
        this.pageNumberArray = new Array(this.totalPages)
        console.log(this.rowsPerPage)
        console.log(this.totalPages)
        console.log(this.totalRecords)
        console.log(this.pageNumberArray)
      }
    })
  }

  onRowsPerPageChange() {
    this.currentPageNumber = 1
    this.getTotalRecords()
    this.getAssets()
  }
  
  onPageNumberChange(pno:number) {
    this.currentPageNumber = pno
    console.log(this.currentPageNumber)
    this.getTotalRecords()
    this.getAssets()
  }
  
  openPopup() {
    this.action = "ADD"
    this.displayStyle = "flex";
       this.asset={
      Name:"",
      ManuFactId:"",
      ModelId:"",
      ColorId:"",
      Price:"",
      InUse:false,
      PurchaseDate:Date,
      Description:"",
      CreatedDateTime:Date  
    }
    this.ModalTitle="Add Asset";
    this.ActivateAddEditComp=true;
  }
  editPopup(assetId: any){
    this.action = "EDIT"
    this.displayStyle = "flex";
    this.editAssetId=assetId;
    this.ModalTitle="Edit Asset";
    this.ActivateAddEditComp=true;
  }

  closePopup() {
    this.displayStyle = "none";
    this.ActivateAddEditComp=false;
  }

  paginationLeftArrowClick() {
    if(this.currentPageNumber>1) {
    this.currentPageNumber-=1
    console.log(this.currentPageNumber)
    this.getAssets()
    }
  }
  paginationRightArrowClick() {
    if(this.currentPageNumber<this.totalPages) {
      this.currentPageNumber +=1
      console.log(this.currentPageNumber)
      this.getAssets()
    }
  }
  getAssets() {
    this.homeService.getAssets(this.rowsPerPage,this.currentPageNumber).subscribe((response:any) => {
      this.assetList = response
    });
    this.closePopup()
  }




}
