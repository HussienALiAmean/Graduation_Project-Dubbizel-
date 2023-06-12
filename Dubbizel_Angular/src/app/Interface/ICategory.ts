import { ExpressionType } from "@angular/compiler"

export interface Icategory{
    id:number,
    name: string,
    parentCategoryID: number,
    subCategoriesList: IsubCategory[],
    categoryAdvertismentsList: IcategoryAds []
}

export interface IsubCategory{
    id:number,
    name: string,
    parentCategoryID: number
}

export interface IcategoryAds{
    title: string,
    adType: string,
    adStatus: string,
    location: string,
    postedAt: Date,
    advertismentImagesList:  IAdvertismentImages[]
    advertisment_RentOptionList:IAdvertismentRent[]
}

export interface IAdvertismentImages{
    imageName:string
}
export interface IAdvertismentRent{
    cost: number
}