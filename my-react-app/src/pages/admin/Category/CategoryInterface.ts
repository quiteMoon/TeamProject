export interface ICategory {
  id: number;
  name: string;
  image: string;
}

export interface ICategoryUpdateInputs {
  name: string;
  image?: FileList;
}

export interface ICategoryCreateInputs {
  name: string;
  image?: FileList | null;
}
