import { IUser } from "./IUser";

export interface IChatData{
    id?:number
    sender:IUser
    receiver:IUser
    content:string
    senderID:string
    receiverID:string
    file?:string
}