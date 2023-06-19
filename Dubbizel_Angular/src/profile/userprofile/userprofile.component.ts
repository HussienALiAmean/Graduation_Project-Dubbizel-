import { Component } from '@angular/core';

import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ProfileService } from 'src/app/Services/profile.service';
import { profile } from 'src/app/Interfaces/profile';


@Component({
  selector: 'app-userprofile',
  templateUrl: './userprofile.component.html',
  styleUrls: ['./userprofile.component.scss']
})
export class UserprofileComponent {
constructor(private activatedroute:ActivatedRoute,private http:HttpClient,private profileService:ProfileService,private fb:FormBuilder)
{
  this.buildForm();
}
newProfile:any=[];
AppuserId="99ba0e2f-a547-44ae-b85e-04b36dbeff4c";
userModel!:profile
userForm:any;
errorMessage:any;
AdForm!: FormGroup;
maxChars = 200;

  buildForm() {
    this.AdForm = this.fb.group({
      aboutMe: [""]
    });
  }

EditForm:any=this.fb.group({
  userName:['',[Validators.required]],
  //birthDate:[''],
  day:[''],
  month:[''],
  year:[''],
  gender:[''],
  aboutMe:[''],
  phoneNumber:[''],
  email:[''],

})
get userName()
{
  return this.EditForm.get('userName');
}
get gender()
{
  return this.EditForm.get('gender');
}

get phoneNumber()
{
  return this.EditForm.get('phoneNumber');
}
get email()
{
  return this.EditForm.get('email');
}

get day()
{
  return this.EditForm.get('day');
}

get month()
{
  return this.EditForm.get('month');
}
get year()
{
  return this.EditForm.get('year');
}
get aboutMe()
{
  return this.EditForm.get('aboutMe');
}

ngOnInit()
{
  this.profileService.GetProfile(this.AppuserId).subscribe({
    next:(data:any)=>{
      console.log(data);
      //this.EditForm.value=data
      this.EditForm.patchValue({
        userName:data.userName,
        gender:data.gender,
        day:data.day,
        month:data.month,
        year:data.year,
        email:data.email,
        aboutMe:data.aboutMe,
        phoneNumber:data.phoneNumber
        
      })
      
    },
    error:error=>this.errorMessage=error
  })
}

public dataFields:Object={text:'Name',value:'id'};
public genders=['Male','Female','Prefer not to say']

public dataOfDays:Object={text:'id',value:'id'};
public days=['1','2','3','4','5','6','7','8','9','10','11','12','13','14','15','16','17','18','19','20','21','22','23','24','25','26','27','28','29','30','31']

public dataOfMonths:Object={text:'id',value:'id'};
public months=['1','2','3','4','5','6','7','8','9','10','11','12']

public dataOfYears:Object={text:'id',value:'id'};

public years=['2023','2022','2021','2020','2019','2018','2017','2016','2015','2014','2013','2012','2011','2010',
  '2009','2008','2007','2006','2005','2004','2003','2002','2001','2000','1999','1998','1997','1996',
  '1995','1994','1993','1992','1991','1990','1989','1988','1987','1986','1985','1984','1983','1982',
  '1981','1980','1979','1978','1977','1976','1975','1974','1973','1972','1971','1970','1969','1968',
  '1967','1966','1965','1964','1963','1962','1961','1960','1959','1958','1957','1956','1955','1954',
  '1953','1952','1951','1950']
  

loadData()
{
  //this.newProfile
  this.profileService.editProfile(this.AppuserId,this.EditForm.value).subscribe({
    next:data=>{
      console.log(this.EditForm.value)
      console.log(data);
      this.newProfile=data;
    },
    error:error=>this.errorMessage=error
  })
}



}