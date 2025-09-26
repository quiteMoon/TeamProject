import * as React from "react";
import type { IIngredient } from "./IngredientInterface";
import EnvConfig from "../../../config/env";
import { useNavigate } from "react-router-dom";

interface Props {
  ingredient: IIngredient;
}

const IngredientRow: React.FC<Props> = ({ ingredient }) => {
  const navigate = useNavigate();

  return (
    <tr className="hover:bg-neutral-50/80 dark:hover:bg-white/5">
      <td className="px-5 py-4 text-sm text-neutral-600 dark:text-neutral-300 whitespace-nowrap">
        {ingredient.id}
      </td>

      <td className="px-5 py-4 font-medium text-neutral-800 dark:text-neutral-200">
        {ingredient.name}
      </td>

      <td className="px-5 py-4">
        <div className="w-14 h-14 bg-neutral-100 dark:bg-neutral-800 rounded-lg overflow-hidden">
          {ingredient.image ? (
            <img
              src={`${EnvConfig.API_URL}/images/ingredients/${ingredient.image}`}
              alt={ingredient.name}
              className="object-contain w-full h-full"
              onError={(e) => {
                const target = e.currentTarget as HTMLImageElement;
                target.style.display = "none";
              }}
            />
          ) : (
            <div className="text-sm text-neutral-400 grid place-items-center h-full">
              Немає зображення
            </div>
          )}
        </div>
      </td>

      <td className="px-5 py-4">
        <div className="flex justify-end gap-2">
          <button
            onClick={() => navigate(`/admin/ingredients/edit`, { state: ingredient })}
            className="px-3 py-1.5 rounded-xl text-xs font-medium bg-neutral-900 text-white hover:opacity-90 dark:bg-white dark:text-neutral-900"
          >
            Редагувати
          </button>
          <button
            onClick={() => navigate(`/admin/ingredients/delete`, { state: ingredient })}
            className="px-3 py-1.5 rounded-xl text-xs font-medium bg-neutral-100 hover:bg-neutral-200 dark:bg-neutral-800 dark:hover:bg-neutral-700"
          >
            Видалити
          </button>
        </div>
      </td>
    </tr>
  );
};

export default IngredientRow;
