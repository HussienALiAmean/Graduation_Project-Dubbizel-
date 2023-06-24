export interface IadvertismetUser{
    userName: string,
    userEmail: string,
    countAds: number,
    advertismetsUsersDTOs: IadvertismetsUsersDTOs[]
}

export interface IadvertismetsUsersDTOs{
    id:number,
    title: string,
    location: string,
    adType: string,
    adStatus: string,
    date: Date,
    expirationDate: Date,
    expireDateOfPremium: Date,
    imageDTOs: []
}
export interface IimageDTOs{
    imageName: string
}