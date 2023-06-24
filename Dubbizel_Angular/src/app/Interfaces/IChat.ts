import { IUser } from "./IUser"

export interface IChat{
    id?:number
    senderID:string
    receiverID:string
    content:string
    file?:File
    sender?:IUser
    receiver?:IUser
}