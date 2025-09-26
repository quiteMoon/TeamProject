import React from "react";
import { useLocation, useNavigate } from "react-router-dom";
import type { IIngredient } from "./IngredientInterface";
import axios from "axios";
import EnvConfig from "../../../config/env";

const IngredientDeletePage: React.FC = () => {
  const navigate = useNavigate();
  const location = useLocation();

  const ingredient = location.state as IIngredient | undefined;

  if (!ingredient) {
    return <p>Інгредієнт не переданий. Поверніться назад.</p>;
  }

  const handleDelete = async () => {
    try {
      await axios.delete(`${EnvConfig.API_URL}/api/Ingredient/delete/${ingredient.id}`);
      navigate("/admin/ingredients");
    } catch (error) {
      console.error("Помилка при видаленні інгредієнта:", error);
      alert("Не вдалося видалити інгредієнта. Спробуйте пізніше.");
    }
  };

  return (
    <div className="max-w-2xl mx-auto p-6 bg-white rounded-lg shadow">
      <h1 className="text-2xl font-bold mb-4">Видалити категорію</h1>

      <div>
        <label className="block font-semibold mb-2">Зображення інгредієнта</label>
        <div className="w-full h-64 bg-gray-100 border border-gray-300 rounded-lg flex items-center justify-center overflow-hidden mb-4">
          {ingredient.image ? (
            <img
              src={`${EnvConfig.API_URL}/images/ingredients/${ingredient.image}`}
              alt={ingredient.name}
              className="object-contain h-full"
            />
          ) : (
            <span className="text-gray-500">Немає зображення</span>
          )}
        </div>
      </div>

      <div className="mb-6">
        <label className="block font-semibold mb-1">Назва інгредієнта</label>
        <p className="px-3 py-2 border border-gray-300 rounded-lg bg-gray-50">{ingredient.name}</p>
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

export default IngredientDeletePage;