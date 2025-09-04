import axios from 'axios';
import * as React from 'react';
import { useEffect, useState } from 'react';
import type { ICategory } from './CategoryInterface';
import CategoryRow from './CategoryRow';
import EnvConfig from '../../../config/env';
import { useNavigate } from 'react-router-dom';

const CategoriesList: React.FC = () => {
  const urlGet = `${EnvConfig.API_URL}/api/Category/list`;
  const [categories, setCategories] = useState<ICategory[]>([]);

  const navigate = useNavigate();

  useEffect(() => {
    axios.get(urlGet)
      .then(response => {
        console.log("Categories:", response.data);
        setCategories(response.data.payload);
      })
      .catch(error => {
        console.error("Error fetching categories:", error);
      });
  }, []);

  return (
    <div className="w-full">
      <div className="hidden md:block overflow-x-auto">
        <div className="bg-white dark:bg-neutral-900 rounded-2xl shadow-sm ring-1 ring-black/5 overflow-hidden">
          {/* Header */}
          <div className="flex items-center justify-between gap-3 px-5 py-4 border-b border-black/10 dark:border-white/10">
            <h2 className="text-lg font-semibold">Список категорій</h2>

            <div className="flex items-center gap-2">
              <div className="text-sm text-neutral-500 pr-5">
                {categories.length} запис(ів)
              </div>
              <button
                onClick={() => navigate("/admin/categories/create")}
                className="px-4 py-2 rounded-xl text-sm font-medium bg-neutral-900 text-white hover:opacity-90 dark:bg-white dark:text-neutral-900"
              >
                + Створити категорію
              </button>
            </div>
          </div>

          {/* Table */}
          <div className="overflow-x-auto">
            <table className="min-w-full text-left align-middle">
              <thead className="bg-neutral-50 dark:bg-neutral-800 text-neutral-600 dark:text-neutral-300 text-xs uppercase tracking-wider">
                <tr>
                  <th className="px-5 py-3 font-semibold">ID</th>
                  <th className="px-5 py-3 font-semibold">Назва</th>
                  <th className="px-5 py-3 font-semibold">Зображення</th>
                  <th className="px-5 py-3 font-semibold text-right">Дії</th>
                </tr>
              </thead>
              <tbody className="divide-y divide-black/5 dark:divide-white/10">
                {categories.map((cat) => (
                  <CategoryRow key={cat.id} category={cat} />
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CategoriesList;
