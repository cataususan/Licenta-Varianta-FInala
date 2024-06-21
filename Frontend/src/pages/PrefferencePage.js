// src/App.js
import React, { useEffect, useState } from "react";
import EnumSelector from "../components/PrefferenceSelector"; 
import Button from "@mui/material/Button"; 
import NavbarBack from "../components/NavbarBack";

function PrefferencePage() {
  const [data, setData] = useState({});
  const [selectedOptions, setSelectedOptions] = useState({});
  const [isLoaded, setIsLoaded] = useState(false);

  useEffect(() => {
    fetch("https://timtour.pagekite.me/api/Prefference")
      .then((response) => response.json())
      .then((data) => {
        if (data) {
          setData(data);
          const initialSelections = {};
          Object.keys(data).forEach((key) => {
            initialSelections[key] = "NONE";
          });
          setSelectedOptions(initialSelections);
        }
        setIsLoaded(true);
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
        setIsLoaded(false);
      });
  }, []);

  const handleSelectionChange = (category, value) => {
    setSelectedOptions((prev) => ({ ...prev, [category]: value }));
  };

  const handleSubmit = () => {
    const token = localStorage.getItem("token");
    if (!token) {
      alert("No authentication token found. Please log in.");
      return;
    }

    const requestOptions = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        barFeatures: {
          type: selectedOptions.barType,
          barEvent: selectedOptions.barEvents,
          barAmbiance: selectedOptions.barAmbiance,
          barDrinkSpecialties: selectedOptions.barDrinkSpecialties,
          barFoodOptions: selectedOptions.barFoodOptions,
          priceRange: selectedOptions.universalPriceRange,
        },
        eventFeatures: {
          eventAudience: selectedOptions.eventAudience,
          eventDuration: selectedOptions.eventDuration,
          eventGenre: selectedOptions.eventGenre,
          eventTypes: selectedOptions.eventTypes,
          eventVenue: selectedOptions.eventVenue,
          eventPrince: selectedOptions.universalPriceRange,
        },
        museumFeatures: {
          museumAccesibility: selectedOptions.museumAccesibility,
          museumExhibitsTypes: selectedOptions.museumExhibitsTypes,
          museumTypes: selectedOptions.museumTypes,
          museumVisitorService: selectedOptions.museumVisitorService,
          priceRange: selectedOptions.universalPriceRange,
        },
        restaurantFeatures: {
          atmosphere: selectedOptions.restaurantAtmosphere,
          cusineTypes: selectedOptions.restaurantCusineTypes,
          dietaryRestrictions: selectedOptions.restaurantDietaryRestrictions,
          specialFeatures: selectedOptions.restaurantSpecialFeatures,
          priceRange: selectedOptions.universalPriceRange,
        },
      }),
    };

    fetch("https://timtour.pagekite.me/api/Prefference", requestOptions)
      .then((response) => response.json())
      .then((data) => console.log("Response from backend:", data))
      .catch((error) => console.error("Failed to submit preferences:", error));
  };

  if (!isLoaded) {
    return <div>Loading...</div>;
  }

  if (Object.keys(data).length === 0) {
    return <div>No data available</div>;
  }

  return (
    <>
      <NavbarBack></NavbarBack>
      <div className="p-5">
        {Object.keys(data).map((key) => (
          <EnumSelector
            key={key}
            title={key}
            options={data[key]}
            selected={selectedOptions[key]}
            onSelect={(value) => handleSelectionChange(key, value)}
          />
        ))}
        <div className="flex justify-center mt-4">
          <Button
            variant="contained"
            style={{ backgroundColor: "#C798C6", color: "white" }}
            onClick={handleSubmit}
          >
            Submit
          </Button>
        </div>
      </div>
    </>
  );
}

export default PrefferencePage;
