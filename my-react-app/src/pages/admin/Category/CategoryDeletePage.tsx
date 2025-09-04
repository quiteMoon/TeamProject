import React from "react";
import { useLocation, useNavigate } from "react-router-dom";
import type { ICategory } from "./CategoryInterface";
import axios from "axios";
import EnvConfig from "../../../config/env";

const CategoryDeletePage: React.FC = () => {
  const navigate = useNavigate();
  const location = useLocation();

  const category = location.state as ICategory | undefined;

  if (!category) {
    return <p>Категорія не передана. Поверніться назад.</p>;
  }

  const handleDelete = async () => {
    try {
      await axios.delete(`${EnvConfig.API_URL}/api/Category/delete/${category.id}`);
      navigate("/admin/categories");
    } catch (error) {
      console.error("Помилка при видаленні категорії:", error);
      alert("Не вдалося видалити категорію. Спробуйте пізніше.");
    }
  };

  return (
    <div className="max-w-2xl mx-auto p-6 bg-white rounded-lg shadow">
      <h1 className="text-2xl font-bold mb-4">Видалити категорію</h1>

      <div>
        <label className="block font-semibold mb-2">Зображення категорії</label>
        <div className="w-full h-64 bg-gray-100 border border-gray-300 rounded-lg flex items-center justify-center overflow-hidden mb-4">
          {category.image ? (
            <img
              src={`${EnvConfig.API_URL}/images/categories/${category.image}`}
              alt={category.name}
              className="object-contain h-full"
            />
          ) : (
            <span className="text-gray-500">Немає зображення</span>
          )}
        </div>
      </div>

      <div className="mb-6">
        <label className="block font-semibold mb-1">Назва категорії</label>
        <p className="px-3 py-2 border border-gray-300 rounded-lg bg-gray-50">{category.name}</p>
      </div>

      <div className="flex justify-end space-x-2">
        <button
          type="button"
          onClick={() => navigate(-1)}
          className="px-4 py-2 rounded-xl text-1.5xs font-medium bg-neutral-100 hover:bg-neutral-200 dark:bg-neutral-800 dark:hover:bg-neutral-700"
        >
          Відмінити
        </button>

        <button
          type="button"
          onClick={handleDelete}
          className="px-4 py-2 rounded-xl text-2xs font-medium bg-neutral-900 text-white hover:opacity-90 dark:bg-white dark:text-neutral-900"
        >
          Видалити
        </button>
      </div>
    </div>
  );
};

export default CategoryDeletePage;