export interface IAdvertisment
{
    id?:number,
    title:string,
    categoryID:number,
    subCategoryID:number,
    adType:string,
    adStatus:string,
    location:string,
    date:string,
    expirationDate:string,
    expireDateOfPremium:string,
    isPremium:boolean,
    isSaved: boolean,
    applicationUserId:string,
    advertisment_FiltrationValuesList:string[]
    advertismentImagesList:string[]
}