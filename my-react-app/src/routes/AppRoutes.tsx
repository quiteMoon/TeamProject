import * as React from "react";
import { Route, Routes } from "react-router-dom";
import NotFound from "../pages/NotFound";
import MainLayout from "../layouts/MainLayout";
import Shop from "../pages/shop/Shop";
import AuthPage from "../pages/auth/AuthPage";
import AdminPage from "../pages/admin/AdminPage";
import ProductsPage from "../pages/admin/ProductsPage";
import CategoriesPage from "../pages/admin/Category/CategoriesPage";
import IngredientsPage from "../pages/admin/Ingredient/IngredientsPage";
import CategoryEditPage from "../pages/admin/Category/CategoryEditPage";
import CategoryDeletePage from "../pages/admin/Category/CategoryDeletePage";
import CategoryCreatePage from "../pages/admin/Category/CategoryCreatePage";
import IngredientEditPage from "../pages/admin/Ingredient/IngredientEditPage";
import IngredientDeletePage from "../pages/admin/Ingredient/IngredientDeletePage";
import UsersList from "../pages/admin/User/UsersPage";
import IngredientCreatePage from "../pages/admin/Ingredient/IngredientCreatePage";

const AppRoutes: React.FC = () => {
  return (
    <Routes>
      <Route path="/" element={<MainLayout />}>
        <Route index element={<Shop />} />
        <Route path="shop" element={<Shop />} />
        <Route path="login" element={<AuthPage />} />
        <Route path="admin" element={<AdminPage />}>
          <Route path="users" element={<UsersList />} />
          <Route path="ingredients" element={<IngredientsPage />} />
          <Route path="ingredients/edit" element={<IngredientEditPage />} />
          <Route path="ingredients/delete" element={<IngredientDeletePage />} />
          <Route path="ingredients/create" element={<IngredientCreatePage />} />
          <Route path="categories" element={<CategoriesPage />} />
          <Route path="categories/edit" element={<CategoryEditPage />} />
          <Route path="categories/delete" element={<CategoryDeletePage />} />
          <Route path="categories/create" element={<CategoryCreatePage />} />
          <Route path="products" element={<ProductsPage />} />
        </Route>
      </Route>
      <Route path="*" element={<NotFound />} />
    </Routes>
  );
};

export default AppRoutes;
