import React, { useEffect, useState } from "react";
import EnvConfig from "../../config/env";
import axios from "axios";
import type { ICategory } from "../admin/Category/CategoryInterface";

interface IProduct {
  id: number;
  name: string;
  image: string;
  price: number;
  category?: string | null;
}

const Shop: React.FC = () => {
  const urlGetCategories = `${EnvConfig.API_URL}/api/Category/list`;
  const urlGetProducts = `${EnvConfig.API_URL}/Product/list`;
  const [categories, setCategories] = useState<ICategory[]>([]);
  const [selectedCategoryName, setSelectedCategoryName] = useState<string | null>(null);
  const [products, setProducts] = useState<IProduct[]>([]);

  useEffect(() => {
    axios.get(urlGetCategories)
      .then(response => {
        setCategories(response.data.payload);
        console.log("Categories:", response.data);
      })
      .catch(error => {
        console.error("Error fetching categories:", error);
      });
  }, []);

  useEffect(() => {
    const categoryParam = selectedCategoryName;
    console.log("Fetching products for category:", categoryParam);
    axios.get(urlGetProducts, { params: { categoryName: categoryParam } })
      .then(response => {
        setProducts(response.data.payload);
        console.log("Products:", response.data);
      })
      .catch(error => {
        console.error("Error fetching products:", error);
      });
  }, [selectedCategoryName]);

  return (
    <div className="flex bg-gray-50 p-6 min-h-screen px-70 pt-15">
      <div className="flex flex-col ml-20 self-start gap-4">
        
        <div
          onClick={() => setSelectedCategoryName(null)}
          className={`relative w-72 flex items-center justify-center p-3 rounded-lg border border-gray-200 shadow bg-white text-lg font-medium text-gray-800 cursor-pointer transition`}
        >
          {selectedCategoryName === null && (
            <span className="absolute left-0 top-0 h-full w-1 bg-red-600 rounded-l-lg"></span>
          )}
          Все меню
        </div>

        <div className="w-72 border border-gray-200 rounded-lg shadow bg-white p-3 flex flex-col gap-2">
          {categories.map((cat) => (
            <div
              key={cat.id}
              onClick={() => setSelectedCategoryName(cat.name)}
              className={`relative flex items-center gap-3 p-2 rounded-lg cursor-pointer transition`}
            >
              {selectedCategoryName === cat.name && (
                <span className="absolute left-0 top-0 h-full w-1 bg-red-600 rounded-l-lg"></span>
              )}
              <img src={`${EnvConfig.API_URL}/images/categories/${cat.image}`} alt={cat.name} className="w-12 h-12 object-contain" />
              <span className="text-lg font-medium text-gray-800">{cat.name}</span>
            </div>
          ))}
        </div>
      </div>

      <div className="flex-1 p-8 pt-0 ml-6">
        <h1 className="text-4xl font-bold text-center text-black">
          {selectedCategoryName ?? "Все меню"}
        </h1>

        <div className="mt-6 grid grid-cols-3 gap-6">
          {products.map((product) => (
            <div
              key={product.id}
              className="rounded-lg p-4 bg-white shadow flex flex-col items-center ml-6 mr-6"
            >
              <img
                src={`${EnvConfig.API_URL}/images/products/${product.image}`}
                alt={product.name}
                className="w-32 h-32 object-contain"
              />
              <h2 className="text-lg font-medium text-center">{product.name}</h2>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default Shop;
