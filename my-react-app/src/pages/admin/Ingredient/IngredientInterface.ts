export interface IIngredient {
  id: number;
  name: string;
  image: string;
}

export interface IIngredientUpdateInputs {
  name: string;
  image?: FileList;
}

export interface IIngredientCreateInputs {
  name: string;
  image?: FileList | null;
}