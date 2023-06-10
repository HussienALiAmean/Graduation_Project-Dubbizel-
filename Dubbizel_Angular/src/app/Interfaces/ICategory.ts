export interface ICategory
{
    id:number,
    name:string,
    subCategoryDTOs:ISubCategory[]
}

export interface ISubCategory
{
    id:number,
    name:string
}