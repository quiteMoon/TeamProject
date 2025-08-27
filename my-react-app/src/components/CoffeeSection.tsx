import React from "react";

const CoffeeSection: React.FC = () => {
  return (
    <section className="bg-white py-12">
      <div className="max-w-6xl mx-auto px-4 flex flex-col md:flex-row items-center gap-8">
        
        {/* Картинка */}
        <div className="flex-1">
          <img
            src="https://s7d1.scene7.com/is/image/mcdonalds/coffeebaner:1-column-desktop?resmode=sharp2"
            alt="McCafe Coffee"
            className="w-full h-auto rounded-lg shadow-lg"
          />
        </div>

        {/* Текст */}
        <div className="flex-1">
          <h2 className="text-3xl font-bold text-neutral-900 mb-4">
            Насолоджуйся улюбленою кавою McCafé ☕
          </h2>
          <p className="text-lg text-neutral-700 mb-6">
            Від ароматного еспресо до ніжного капучино — в McCafé на тебе чекає широкий вибір напоїв, 
            приготованих із турботою, щоб кожен момент був особливим.
          </p>
          <button className="bg-yellow-600 hover:bg-red-700 text-white px-6 py-3 rounded-full font-medium transition">
            Дізнатися більше
          </button>
        </div>
      </div>
    </section>
  );
};

export default CoffeeSection;
