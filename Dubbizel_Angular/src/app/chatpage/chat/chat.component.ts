import { Component, OnInit } from '@angular/core';
import { IUser } from 'src/app/Interfaces/IUser';
import { ChatService } from 'src/app/Services/chat.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IChat } from 'src/app/Interfaces/IChat';
import { IChatData } from 'src/app/Interfaces/IChatData';
import * as signalR from '@aspnet/signalr';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { IAdvertismentDetails } from 'src/app/Interface/AdvertismentDetails';
import { AdvertismentService } from 'src/admin/Services/advertisment.service';
import { AdvertismentServiceService } from 'src/app/Services/advertisment-service.service';


@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent  implements OnInit  {

  currentPage = 1; 
  itemsPerPage = 5;
  
  users:any[]=[]
  myForm= this.fb.group({
    content: ['', [Validators.required,
      Validators.minLength(3)
      ]],
      file:[],
    
  }) ;
  AdvertismentDetails!:IAdvertismentDetails
  tempData:any[]=[]
  file!: File
  chat:IChat={
    // id:0,
    AdvertismentId:5,
    senderID:"2946b9f6-35f7-4e2f-8c2a-7a0ab10885db",
    receiverID:"99ba0e2f-a547-44ae-b85e-04b36dbeff4c",
    content:"",
    sold:false,
    file:this.file
  }
  CurrentUserId:any = "2946b9f6-35f7-4e2f-8c2a-7a0ab10885db"
  chatData!:IChatData[]
  errorMessage:any
  hubConnectionBuilder: any;
  hubconnection!: signalR.HubConnection;
  newChat!: any;
  UserTemp:any
  img!:any
  Id!:any
  top:number = 5;
  AdvertismentId:any
  userId:any
  loginId:any = localStorage.getItem('ApplicationUserId')
  // fb: any;
  constructor(private chatService:ChatService
    ,private activatRoute:ActivatedRoute
    ,private AdvertismentService:AdvertismentServiceService
    ,private fb: FormBuilder){
    this.loginId = localStorage.getItem('ApplicationUserId')

    }
  ngOnInit(): void {
    this.activatRoute.paramMap.subscribe((params:ParamMap)=>{
    this.AdvertismentId= params.get('adId');
    this.userId=params.get('UserID')
    });
    this.Users()
    this.createForm()
    this.top=5
    this.CurrentUserId = this.userId
    this.chat.senderID=this.loginId
    this.chat.AdvertismentId=this.AdvertismentId
    this.StartHubConnection()
    this.openHubToListenAnychat()
    this.openHubToListenRemovechat()
    console.log(this.loginId)
    console.log(this.chat.AdvertismentId)
    var loginId = localStorage.getItem('ApplicationUserId')
    this.getAdvertisment()
  }

  onSelectFile(fileInput: any) {
    this.file = fileInput.target.files[0]
    this.chat.file = fileInput.target.files[0];
    console.log(this.file)
    console.log(this.chat.file)
    
  }

  createForm() {
    this.myForm = this.fb.group({
      content: ['', [Validators.required,
        Validators.minLength(3)
        ,Validators.maxLength(15)]],
        file:[],
      
    });
  }
  GetUser(id:any){
    this.chatData = []
    this.chatService.skip=0;
    this.chat.receiverID=id
    console.log(this.chat)
    this.GetMessage()

  }
  getAdvertisment(){
    this.AdvertismentService.getDetails(this.AdvertismentId,this.userId).subscribe({
      next:data=>this.AdvertismentDetails=data,//this.users=data ,//.push(data[0],data[1])
      error:error=>this.errorMessage=error
    })
    setTimeout(()=>{

      console.log(this.AdvertismentDetails)
      console.log(this.AdvertismentDetails.advertismentImagesList[0].imageName)
    },2000)
  }
  Users(){
    console.log(this.userId)
    this.chatService.GetUsers(this.userId,this.loginId).subscribe({
      next:data=>this.UserTemp=data,//this.users=data ,//.push(data[0],data[1])
      error:error=>this.errorMessage=error
    })
    
    setTimeout(()=> {
      let values = Object.values(this.UserTemp)
      console.log('users here')
      console.log(values[0])
      console.log(values)
      console.log(this.UserTemp[0])
      for (let index = 0; index < values.length; index++) {
        const element = values[index];
        // if(type( element) == Array)
        console.log(element)
        this.users.push(element)
      }
      console.log(this.users)
    },2000)
  }

  StartHubConnection(){
    this.hubconnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:7189/ChatHub',
        {
          skipNegotiation: true,
          transport: signalR.HttpTransportType.WebSockets
        }).configureLogging(signalR.LogLevel.Debug).build();

      setTimeout(async () => {

        
        this.hubconnection.start().then(() => {
          console.log("connection started");
        }).catch(err => console.log(err));
      },2000);
  }
  get totalPages() {
    return Math.ceil(this.chatData.length / 7);
     // Change 10 to the number of items per page
  }

  get pages() {
    const pageCount = Math.ceil(this.chatData.length / 7); 
    // Change 10 to the number of items per page
    return Array(pageCount).fill(0).map((_, i) => i + 1);
  }

  getCeil(num: number): number {
    return Math.ceil(num);
  }

  getChatDataForPage() {
    const startIndex = 0;//(this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    return this.chatData.slice(startIndex, endIndex);
  }
  setPage(page: number) {
    this.currentPage = page;
  }

  nextPage() {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
    }
  }

  prevPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }

  openHubToListenAnychat(){
    console.log("try to liesten")
    this.hubconnection.on('NewMessageNotify', (message,image) => {
      console.log(message);
      console.log(image);
      this.img = image
      this.newChat = message
    })
    console.log(this.newChat)
    console.log("this.newChat")
  }
  openHubToListenRemovechat(){
    console.log("try to liesten for removing ")
    this.hubconnection.on('ChatDeleteNotify', (hide,id) => {
      console.log(hide);
      console.log(id);
      console.log(this.Id);
      this.Id=id
      console.log(this.Id);
      setTimeout(()=>{

        var item = document.getElementById(id)
        console.log(item)
        item?.classList.add(`${hide}`)
      },2000)
    })
  }
  delete(id:any){
    console.log("delete")
    console.log(id)
    this.Id = id

    this.chatService.delete(id).subscribe({
      next:data=>console.log(data) ,//.push(data[0],data[1])
     error:error=>this.errorMessage=error
    })
    var ID = id.toString()
    setTimeout(  ()=>{
      
      // let Price = String(this.inputPrice);
      // let Quantity = String(this.objDto.quantity);
     

      this.hubconnection
    .invoke('ChatDelete',ID)
      console.log(ID)
      console.log("after invoke con here")
      const self = this;

    },2000)

  }

  onSubmit(){
    console.log(this.chat.content)
    var formData = new FormData()
   
    formData.append("AdvertismentId", this.chat.AdvertismentId.toString())
    formData.append("senderID", this.chat.senderID)
    formData.append("receiverID", this.chat.receiverID)
    formData.append("content", this.chat.content)
    formData.append("sold", "false")
    

    if (this.chat.file != null) {
      formData.append("image", this.chat.file, this.chat.file.name)
      console.log(this.chat.file)
    }

    console.log(formData)
    console.log(formData.get("AdvertismentId"))
    console.log(formData.get("receiverID"))
    console.log(formData.get("image"))
    console.log(this.chat)
    this.chatService.AddMessage(formData).subscribe({
       next:data=>console.log(data) ,//.push(data[0],data[1])
      error:error=>this.errorMessage=error
    })
    setTimeout(  ()=>{
      
      
      this.hubconnection
    .invoke('NewChatHub',this.chat.content,this.file?.name)
      console.log(this.chat)
      console.log("after invoke con here")
      const self = this;

    },3000)
  }
  async GetMessage(){
        this.chatService.getChat(this.chat.senderID , this.chat.receiverID).subscribe({
        next:data=>this.tempData=data ,//.push(data[0],data[1])
        error:error=>this.errorMessage=error
    })
    setTimeout(()=> {
      // this.chatService.top+=5;
      this.chatService.skip+=5;

      console.log(this.tempData)
      console.log("All Keys");
      console.log(Object.keys(this.tempData))
      console.log("All Values");
      let values =Object.values(this.tempData)
      console.log("result arrays")
      console.log(values[0])
      for (let index = 0; index < values[0].length; index++) {
        const element =  values[0][index];
        console.log(element)
        console.log(element.room.sold)

        this.chatData.push(element)
      }
      // this.chatData = values[0]
      // console.log(Object.entries(this.tempData));
      // console.log(Array.from( this.tempData))
      // this.chatData = this.tempData
      
      // console.log(this.chatData)
    } ,3000)
  //  console.log( this.chatData[0].room?.sold)
    console.log(this.errorMessage)
  }
}


