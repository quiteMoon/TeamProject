import React, { useState } from "react";
import { Link, Outlet } from "react-router-dom";
import { ChevronDown } from "lucide-react"; // стрілочка

// Список категорій із картинками
const categories = [
  { name: "Сніданок", img: "https://s7d1.scene7.com/is/image/mcdonalds/nav_BKFT_Muffin_Roll:menu-category?resmode=sharp2" },
  { name: "НайсПрайс комбо", img: "https://s7d1.scene7.com/is/image/mcdonalds/2forU_180x180px:menu-category?resmode=sharp2" },
  { name: "Бургери та роли", img: "https://s7d1.scene7.com/is/image/mcdonalds/nav_Burger_ROLL:menu-category?resmode=sharp2" },
  { name: "Курка", img: "https://s7d1.scene7.com/is/image/mcdonalds/nav_chicken:menu-category?resmode=sharp2" },
  { name: "Шеринг Бокси", img: "https://s7d1.scene7.com/is/image/mcdonalds/nav_company:menu-category?resmode=sharp2" },
  { name: "Картопля, Каша та Салати", img: "https://s7d1.scene7.com/is/image/mcdonalds/nav_fries:menu-category?resmode=sharp2" },
  { name: "Соуси", img: "https://s7d1.scene7.com/is/image/mcdonalds/nav_sauce:menu-category?resmode=sharp2" },
  { name: "Топінги та сиропи", img: "https://s7d1.scene7.com/is/image/mcdonalds/nav_topings:menu-category?resmode=sharp2" },
  { name: "Десерти", img: "https://s7d1.scene7.com/is/image/mcdonalds/nav_desserts:menu-category?resmode=sharp2" },
  { name: "Напої", img: "https://s7d1.scene7.com/is/image/mcdonalds/nav_drinks:menu-category?resmode=sharp2" },
  { name: "Кава", img: "https://s7d1.scene7.com/is/image/mcdonalds/nav_coffee_v4:menu-category?resmode=sharp2" },
  { name: "Хеппі Міл", img: "https://s7d1.scene7.com/is/image/mcdonalds/nav_happy-meal:menu-category?resmode=sharp2" },
  { name: "МакМеню та МакМеню+", img: "https://s7d1.scene7.com/is/image/mcdonalds/nav_mcmenu:menu-category?resmode=sharp2" },
];

const MainLayout: React.FC = () => {
  const [isOpen, setIsOpen] = useState(false);

  return (
    <div className="min-h-screen flex flex-col bg-white">
      {/* Хедер */}
      <header className="bg-white shadow-sm border-b border-black/10 relative">
        <div className="w-full px-8 flex items-center justify-between h-16">
          {/* Логотип */}
          <Link to="/" className="flex items-center">
            <img
              src="https://www.mcdonalds.com/content/dam/sites/ua/nfl/icons/Logo_on_white_desktop_Small.jpg"
              alt="Logo"
              className="h-10 w-auto"
            />
          </Link>

          {/* Меню */}
          <div className="relative">
            <button
              className="flex items-center gap-1 text-lg font-medium text-neutral-800 hover:text-red-600"
              onClick={() => setIsOpen(!isOpen)}
            >
              Меню
              <ChevronDown
                className={`h-5 w-5 transition-transform duration-300 ${
                  isOpen ? "rotate-180" : "rotate-0"
                }`}
              />
            </button>

            {isOpen && (
              <div className="absolute left-1/2 -translate-x-1/2 mt-2 w-72 bg-white border rounded-lg shadow-lg grid grid-cols-2 gap-2 p-4 z-50">
                {categories.map((cat, i) => (
                  <Link
                    key={i}
                    to={`/category/${i + 1}`}
                    className="flex items-center gap-3 p-2 rounded hover:bg-neutral-100"
                  >
                    <img src={cat.img} alt={cat.name} className="h-10 w-10 object-contain" />
                    <span className="text-sm text-neutral-700">{cat.name}</span>
                  </Link>
                ))}
              </div>
            )}
          </div>

          {/* Іконка входу */}
          <Link to="/login">
            <img
              src="https://img.icons8.com/?size=100&id=26211&format=png&color=000000"
              alt="Login"
              className="h-8 w-8 cursor-pointer"
            />
          </Link>
        </div>
      </header>

      {/* ВАЖЛИВО: Виводимо дочірні компоненти */}
      <main className="flex-grow">
        <Outlet />
      </main>
    </div>
  );
};

export default MainLayout;
