import { useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import axios from "axios";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import EnvConfig from "../../../config/env";
import type { ICategoryCreateInputs } from "./CategoryInterface";

const schema: yup.ObjectSchema<ICategoryCreateInputs> = yup
  .object({
    name: yup
      .string()
      .required("Назва категорії обов’язкова")
      .min(2, "Мінімум 2 символи"),
    image: yup
      .mixed<FileList>()
      .notRequired()
      .test(
        "fileType",
        "Потрібне зображення (jpg, png, gif)",
        (value) => {
          if (!value) return true;
          if (!(value instanceof FileList)) return false;
          if (value.length === 0) return true;
          const allowedTypes = ["image/jpeg", "image/png", "image/gif"];
          return allowedTypes.includes(value[0].type);
        }
      )
  })
  .required();

const CategoryCreatePage: React.FC = () => {
  const navigate = useNavigate();

  const { register, handleSubmit, formState: { errors }, watch } = useForm<ICategoryCreateInputs>({
    resolver: yupResolver(schema)
  });

  const [previewImage, setPreviewImage] = useState<File | null>(null);

  const [previewUrl, setPreviewUrl] = useState<string>("");

  const watchImage = watch("image");

  useEffect(() => {
    if (watchImage && watchImage.length > 0) {
      const file = watchImage[0];
      setPreviewImage(file);

      const url = URL.createObjectURL(file);
      setPreviewUrl(url);

      return () => URL.revokeObjectURL(url);
    } else {
      setPreviewImage(null);
      setPreviewUrl("");
    }
  }, [watchImage]);

  const onSubmit = async (data: ICategoryCreateInputs) => {
    try {
      const formData = new FormData();
      formData.append("name", data.name);
      if (data.image && data.image.length > 0) {
        formData.append("image", data.image[0]);
      }

      await axios.post(`${EnvConfig.API_URL}/api/Category/create`, formData, {
        headers: { "Content-Type": "multipart/form-data" },
      });

      navigate("/admin/categories");
    } catch (error) {
      console.error("Помилка при створенні категорії:", error);
      alert("Не вдалося створити категорію. Спробуйте пізніше.");
    }
  };

  return (
    <div className="max-w-2xl mx-auto p-6 bg-white rounded-lg shadow">
      <h1 className="text-2xl font-bold mb-4">Створити категорію</h1>

      <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
        <div>
          <label className="block font-semibold mb-2">Зображення категорії</label>

          <div className="w-full h-64 bg-gray-100 border border-gray-300 rounded-lg flex items-center justify-center overflow-hidden mb-3">
            {previewUrl ? (
              <img
                src={previewUrl}
                alt="Прев'ю зображення"
                className="object-contain h-full"
              />
            ) : (
              <span className="text-gray-500">Немає зображення</span>
            )}
          </div>

          <div className="flex items-center space-x-4">
            <label
              htmlFor="imageUpload"
              className="px-4 py-2 bg-blue-50 text-blue-700 rounded-lg font-semibold cursor-pointer
                hover:bg-blue-100 transition"
            >
              Вибрати зображення
            </label>

            <input
              type="file"
              id="imageUpload"
              accept="image/*"
              {...register("image")}
              className="hidden"
            />

            <span className="text-gray-700 text-sm truncate max-w-xs">
              {previewImage ? previewImage.name : "Немає файлу"}
            </span>
          </div>

          {errors.image && (
            <p className="text-red-600 text-sm mt-1">{errors.image.message}</p>
          )}
        </div>

        <div>
          <label className="block font-semibold mb-1">Назва категорії</label>
          <input
            type="text"
            {...register("name")}
            className={`w-full border px-3 py-2 rounded-lg ${
              errors.name ? "border-red-600" : "border-gray-300"
            }`}
          />
          {errors.name && (
            <p className="text-red-600 text-sm mt-1">{errors.name.message}</p>
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
            Створити
          </button>
        </div>
      </form>
    </div>
  );
};

export default CategoryCreatePage;
