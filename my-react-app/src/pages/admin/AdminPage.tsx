import { Outlet, NavLink } from "react-router-dom";

const AdminPage: React.FC = () => {
const linkClass = ({ isActive }: { isActive: boolean }) =>
    `block rounded-lg px-4 py-2 transition ${
      isActive
        ? "bg-blue-600 text-white font-semibold"
        : "text-gray-300 hover:bg-gray-700 hover:text-white"
    }`;  

  return (
    <div className="flex min-h-screen">
      {/* Sidebar */}
      <aside className="w-64 bg-gray-900 p-4 space-y-2">
        <NavLink to="users" className={linkClass}>
          Користувачі
        </NavLink>
        <NavLink to="ingredients" className={linkClass}>
          Інгредієнти
        </NavLink>
        <NavLink to="categories" className={linkClass}>
          Категорії
        </NavLink>
        <NavLink to="products" className={linkClass}>
          Продукти
        </NavLink>
      </aside>

      {/* Main content */}
      <main className="flex-1 bg-gray-100 p-6">
        <Outlet />
      </main>
    </div>
  );
};

export default AdminPage;