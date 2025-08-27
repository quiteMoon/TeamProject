// src/components/NicePriceSection.tsx
import React from "react";

const NicePriceSection: React.FC = () => {
  return (
    <section className="bg-white py-12">
      <div className="max-w-6xl mx-auto flex flex-col md:flex-row items-center gap-8 px-4">
        
        {/* Картинка */}
        <div className="w-full md:w-1/2">
          <img
            src="https://s7d1.scene7.com/is/image/mcdonalds/Value_Mcd_v2:1-column-desktop?resmode=sharp2"
            alt="НайсПрайс Комбо"
            className="rounded-lg shadow-lg w-full object-cover"
          />
        </div>

        {/* Текст */}
        <div className="w-full md:w-1/2 text-center md:text-left">
          <h2 className="text-3xl font-bold text-neutral-900 mb-4">
            НайсПрайс Комбо!
          </h2>
          <p className="text-lg text-neutral-700 mb-6">
            Обирай чізбургер чи нагетси з напоєм або картоплею фрі
            за суперціною. Смачно та вигідно!
          </p>
          <a
            href="#"
            className="inline-block bg-yellow-600 text-white px-6 py-3 rounded-lg font-medium hover:bg-red-700 transition"
          >
            Детальніше
          </a>
        </div>
      </div>
    </section>
  );
};

export default NicePriceSection;
