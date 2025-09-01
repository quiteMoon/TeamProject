import * as React from "react";
import { Route, Routes } from "react-router-dom";
import NotFound from "../pages/NotFound";
import MainLayout from "../layouts/MainLayout";
import UserView from "../pages/users/UserView";
import Shop from "../pages/shop/Shop";
import AuthPage from "../pages/auth/AuthPage";

const AppRoutes: React.FC = () => {
  return (
    <Routes>
      <Route path="/" element={<MainLayout />}>
        <Route index element={<Shop />} />

        <Route path="shop" element={<Shop />} />

        <Route path="login" element={<AuthPage />} />

        <Route path="user/:id" element={<UserView />} />
      </Route>
      <Route path="*" element={<NotFound />} />
    </Routes>
  );
};

export default AppRoutes;
