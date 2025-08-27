import React, { useEffect, useState } from "react";

interface Category {
  id: number;
  name: string;
}

const Shop: React.FC = () => {
  const [categories, setCategories] = useState<Category[]>([]);

  useEffect(() => {
    fetch("http://localhost:5028/api/category")
      .then((res) => res.json())
      .then((data) => setCategories(data))
      .catch((err) => console.error(err));
  }, []);

  return (
    <div className="p-6 bg-white min-h-screen">
      <ul className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-4">
        {categories.map((c) => (
          <li key={c.id} className="p-4 border rounded-lg shadow text-center font-medium">
            {c.name}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Shop;
