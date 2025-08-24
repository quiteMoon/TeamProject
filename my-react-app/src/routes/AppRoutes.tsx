import * as React from "react";
import { Route, Routes } from "react-router-dom";
import UsersList from "../pages/users/UsersList";
import Login from "../pages/auth/Login";
import NotFound from "../pages/NotFound";
import MainLayout from "../layouts/MainLayout";
import UserView from "../pages/users/UserView";

const AppRoutes : React.FC = () => {
  return (
    <Routes>
      <Route path="/" element={<MainLayout />}>
        <Route index element={<UsersList />} />
        <Route path="login" element={<Login />} />
        <Route path={"user"}>
          <Route path={":id"} element={<UserView />} />
        </Route>
      </Route>
      <Route path="*" element={<NotFound />} />
    </Routes>
  );
};

export default AppRoutes;
