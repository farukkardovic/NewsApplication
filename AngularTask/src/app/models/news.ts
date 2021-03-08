export class News {
  Id: number;
  Name: string;
  Description: string;
  CategoryId: number;
  AuthorId?: number;
  IsDeleted?: boolean;
  File: File;
}
