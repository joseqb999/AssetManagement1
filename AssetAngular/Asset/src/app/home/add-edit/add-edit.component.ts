import { Component, Input, OnInit ,Output,EventEmitter} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AddeditService } from '../../services/addedit.service';
// import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ThisReceiver } from '@angular/compiler';
@Component({
  selector: 'app-add-edit',
  templateUrl: './add-edit.component.html',
  styleUrls: ['./add-edit.component.css']
})
export class AddEditComponent implements OnInit {

  @Input() action:any
  @Input() assetId:any;

  @Output() someEvent = new EventEmitter<string>();
  @Output() closePopup = new EventEmitter<string>();

  msgInvalid = ""
  msgmanInvalid = ""

  AddEditForm = new FormGroup({
    Name: new FormControl('',[Validators.required]),
    ManuFactId: new FormControl('',[Validators.required]),
    ModelId: new FormControl(''),
    ColorId: new FormControl(''),
    Price: new FormControl(''),
    InUse: new FormControl(false),
    PurchaseDate: new FormControl(''),
    Description: new FormControl('')
  });


clearValidationMessages () {
  this.msgInvalid = ""
  this.msgmanInvalid = ""
}

validateForm () {

  this.clearValidationMessages()

var name = this.AddEditForm.controls["Name"] 
var nameErrors = name.errors
var manufacturer =this.AddEditForm.controls["ManuFactId"]
var manufacturerErrors = manufacturer.errors


  if( nameErrors) {
    if( nameErrors["required"]) {
      this.msgInvalid = "Name is required"
  }
  return false
}

if( manufacturerErrors) {
  if( manufacturerErrors["required"]) {
    this.msgmanInvalid = "Please select a Manufacturer"
}
return false
}



return true  
}
  constructor(private addeditService:AddeditService) { }

  manufacturers:any = []
  models:any = []
  colors:any = []
  asset:any = {}

  ngOnInit(): void {
this.getManufacturers()
this.getModels()
this.getColors()

if(this.action=="EDIT")
{
  this.getValues();
  
}
console.log("@@@",this.assetId)

  }

getValues(){
this.addeditService.getAssetById(this.assetId).subscribe({
  next:(data:any)=> {
    this.asset = data[0]
    this.setFormValues(this.asset)
  }
})
}

cancelAddEdit() {
  this.closePopup.emit()
}

setFormValues(asset:any) {
  var d
  this.AddEditForm.controls["Name"].setValue(asset.name)
  this.AddEditForm.controls["ManuFactId"].setValue(asset.manuFactId)
  this.AddEditForm.controls["ModelId"].setValue(asset.modelId)
  this.AddEditForm.controls["ColorId"].setValue(asset.colorId)
  this.AddEditForm.controls["Price"].setValue(asset.price)
  this.AddEditForm.controls["Description"].setValue(asset.description)
  this.AddEditForm.controls["InUse"].setValue(asset.inUse)
  d = asset.purchaseDate.toString().substring(0,10)
  console.log(d)
  this.AddEditForm.controls["PurchaseDate"].setValue(d)
}

  getManufacturers() {
    this.addeditService.getManufacturers().subscribe({
      next:(data) => {
          this.manufacturers = data
      },
      error:(err) => {
        console.log(err)
      }
    })
  }

    getModels() {
      this.addeditService.getModels().subscribe({
        next:(data) => {
            this.models = data
        },
        error:(err) => {
          console.log(err)
        }
      })
    }

    getColors() {
      this.addeditService.getColors().subscribe({
        next:(data) => {
            this.colors = data
        },
        error:(err) => {
          console.log(err)
        }
      })
    }

getAssetInput() {
  let asset = {}
    asset = {
      Name:this.AddEditForm.controls["Name"].value,
      ManuFactId:this.AddEditForm.controls["ManuFactId"].value,
      ModelId:this.AddEditForm.controls["ModelId"].value,
      ColorId:this.AddEditForm.controls["ColorId"].value,
      Description:this.AddEditForm.controls["Description"].value,
      PurchaseDate:this.AddEditForm.controls["PurchaseDate"].value,
      Price:this.AddEditForm.controls["Price"].value,
      InUse:this.AddEditForm.controls["InUse"].value
    }
    return asset
}

onSubmit() {
  if(!this.validateForm()) {
    return
  }
  this.action == "ADD" ?this.addAsset():this.editAsset()
}

  addAsset(){
  var asset = this.getAssetInput()
  this.addeditService.addAsset(asset).subscribe({
  next:(data)=> {
       this.someEvent.emit();

  },
  error:(err) => {
    console.log(err)
  }
})
}

editAsset = async () => {
  var asset:any = {}
  asset = this.getAssetInput()
  asset.id = this.assetId
  console.log(asset)
this.addeditService.editAsset(asset).subscribe({
  next:(data)=> {
    this.someEvent.emit();  
  },
  error:(err) => {
    console.log(err)
  }
})
}
     

}
