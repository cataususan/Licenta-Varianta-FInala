// src/components/EnumSelector.js
import React from "react";

function EnumSelector({ title, options, selected, onSelect }) {
  if (!options) return null;

  return (
    <div className="mt-3 flex flex-col items-center">
      <h2 className="font-bold text-lg mb-2 capitalize text-[#C798C6]">
        {title && title.replace(/([A-Z])/g, " $1").trim()}
      </h2>
      <div className="flex flex-wrap justify-center">
        {options.map((option) => (
          <button
            key={option}
            className={`m-1 p-2 rounded-md border transition-colors duration-300 
                        ${
                          selected === option
                            ? "bg-[#C798C6] text-white"
                            : "text-[#C798C6] border-[#C798C6] bg-white hover:bg-gray-100"
                        }`}
            onClick={() => onSelect(option)}
          >
            {option}
          </button>
        ))}
      </div>
    </div>
  );
}

export default EnumSelector;
