import { useLocation, useNavigate } from "react-router-dom";
import { useState } from "react";
import axios from "axios";
import EnvConfig from "../../../config/env";
import * as yup from "yup";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import type { IIngredientUpdateInputs, IIngredient } from "./IngredientInterface";

const schema: yup.ObjectSchema<IIngredientUpdateInputs> = yup.object({
  name: yup.string().required("Назва інгредієнта обов’язкова").min(2, "Мінімум 2 символи"),
  image: yup
    .mixed<FileList>()
    .test("fileType", "Потрібне зображення (jpg, png, gif)", (value) => {
      if (!value || value.length === 0) return true;
      const allowedTypes = ["image/jpeg", "image/png", "image/gif"];
      return allowedTypes.includes(value[0].type);
    }),
});


const CategoryEditPage: React.FC = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const ingredient = location.state as IIngredient | undefined;

  const [newImageFile, setNewImageFile] = useState<File | null>(null);
  const [previewImage, setPreviewImage] = useState<string>(
    `${EnvConfig.API_URL}/images/ingredients/${ingredient?.image}`
  );

  const {register, handleSubmit, formState: { errors } } = useForm<IIngredientUpdateInputs>({
    resolver: yupResolver(schema),
    defaultValues: {
      name: ingredient?.name || "",
      image: undefined,
    },
  });

  const handleImageChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (file) {
      setNewImageFile(file);
      setPreviewImage(URL.createObjectURL(file));
    }
  };

  const handleFormSubmit = async (data: IIngredientUpdateInputs) => {
    try {
      const formData = new FormData();

      formData.append("id", ingredient!.id.toString());
      formData.append("name", data.name);

      if (newImageFile) {
        formData.append("image", newImageFile);
      } else {
        formData.append("image", "");
      }

      await axios.put(`${EnvConfig.API_URL}/api/Ingredient/update`, formData, {
        headers: { "Content-Type": "multipart/form-data" },
      });

      navigate("/admin/ingredients");
    } catch (error) {
      console.error("Помилка при оновленні інгредієнта:", error);
    }
  };

  if (!ingredient) {
    return <p>Інгредієнт не переданий. Поверніться назад.</p>;
  }

  return (
    <div className="max-w-2xl mx-auto p-6 bg-white rounded-lg shadow">
      <h1 className="text-2xl font-bold mb-4">Редагувати інгредієнт</h1>

      <form onSubmit={handleSubmit(handleFormSubmit)} className="space-y-6">
        <div>
          <label className="block font-semibold mb-2">Зображення інгредієнта</label>

          <div className="w-full h-64 bg-gray-100 border border-gray-300 rounded-lg flex items-center justify-center overflow-hidden mb-3">
            {previewImage ? (
              <img
                src={previewImage}
                alt="Прев'ю зображення"
                className="object-contain h-full"
              />
            ) : (
              <span className="text-gray-500">Немає зображення</span>
            )}
          </div>

          <div className="flex items-center space-x-4 mb-3">
            <label
              htmlFor="imageUpload"
              className="px-4 py-2 bg-blue-50 text-blue-700 rounded-lg font-semibold cursor-pointer
                hover:bg-blue-100 transition"
            >
              Змінити зображення
            </label>

            <input
              type="file"
              id="imageUpload"
              accept="image/*"
              {...register("image")}
              onChange={handleImageChange}
              className="hidden"
            />

            <span className="text-gray-700 text-sm truncate max-w-xs">
              {newImageFile ? newImageFile.name : previewImage ? "Поточне зображення" : "Немає файлу"}
            </span>
          </div>

          {errors.image && (
            <p className="text-red-500 text-sm mt-1">{errors.image.message}</p>
          )}
        </div>

        <div>
          <label className="block font-semibold mb-1">Назва категорії</label>
          <input
            type="text"
            {...register("name")}
            className="w-full border px-3 py-2 rounded-lg"
          />
          {errors.name && (
            <p className="text-red-500 text-sm mt-1">{errors.name.message}</p>
          )}
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
            type="submit"
            className="px-4 py-2 rounded-xl text-2xs font-medium bg-neutral-900 text-white hover:opacity-90 dark:bg-white dark:text-neutral-900"
          >
            Зберегти
          </button>
        </div>
      </form>
    </div>
  );
};

export default CategoryEditPage;
