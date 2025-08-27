import React, { useState } from "react";
import { ChevronLeft, ChevronRight } from "lucide-react";

interface BannerSliderProps {
  images: string[];
}

const BannerSlider: React.FC<BannerSliderProps> = ({ images }) => {
  const [current, setCurrent] = useState(0);

  const prev = () => setCurrent((p) => (p === 0 ? images.length - 1 : p - 1));
  const next = () => setCurrent((p) => (p === images.length - 1 ? 0 : p + 1));

  return (
    <div className="relative max-w-5xl mx-auto h-[400px] overflow-hidden rounded-md shadow-lg">
      <div
        className="flex transition-transform duration-700 ease-in-out"
        style={{ transform: `translateX(-${current * 100}%)` }}
      >
        {images.map((src, i) => (
          <img
            key={i}
            src={src}
            alt={`banner-${i}`}
            className="w-full h-[450px] object-cover flex-shrink-0"
          />
        ))}
      </div>

      {/* Ліва стрілка */}
      <button
        onClick={prev}
        className="absolute inset-y-1/2 left-4 -translate-y-1/2 text-white"
      >
        <ChevronLeft size={40} strokeWidth={3} />
      </button>

      {/* Права стрілка */}
      <button
        onClick={next}
        className="absolute inset-y-1/2 right-4 -translate-y-1/2 text-white"
      >
        <ChevronRight size={40} strokeWidth={3} />
      </button>

      {/* Індикатори */}
      <div className="absolute bottom-4 left-1/2 -translate-x-1/2 flex gap-2">
        {images.map((_, i) => (
          <button
            key={i}
            onClick={() => setCurrent(i)}
            className={`w-3 h-3 rounded-full ${
              i === current ? "bg-white" : "bg-gray-400"
            }`}
          />
        ))}
      </div>
    </div>
  );
};

export default BannerSlider;
