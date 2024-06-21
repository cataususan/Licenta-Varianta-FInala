import React, { useState, useEffect } from "react";
import { Typography, Box } from "@mui/material";
import NavbarBack from "../components/NavbarBack";
import CustomLocationCard from "../components/CustomLocationCard";
const LocationRecommandation = () => {
  const [locations, setLocations] = useState({
    restaurants: [],
    bars: [],
    museums: [],
    events: [],
  });

  useEffect(() => {
    const token = localStorage.getItem("token"); 

    const fetchData = async (type, endpoint) => {
      try {
        const response = await fetch(endpoint, {
          headers: { Authorization: `Bearer ${token}` },
        });
        const data = await response.json();
        console.log(`Response for ${type}:`, data); 
        return data.recommandation; 
      } catch (error) {
        console.error(`Error fetching ${type} data:`, error);
        return null;
      }
    };

    const fetchAllLocations = async () => {
      const restaurant = await fetchData(
        "restaurant",
        "https://timtour.pagekite.me/api/Recommandation/getRestaurantRecommandation"
      );
      const bar = await fetchData(
        "bar",
        "https://timtour.pagekite.me/api/Recommandation/getBarRecommandation"
      );
      const museum = await fetchData(
        "museum",
        "https://timtour.pagekite.me/api/Recommandation/getMuseumRecommandation"
      );
      const event = await fetchData(
        "event",
        "https://timtour.pagekite.me/api/Recommandation/getEventRecommandation"
      );

      setLocations({
        restaurants: restaurant ? [restaurant] : [],
        bars: bar ? [bar] : [],
        museums: museum ? [museum] : [],
        events: event ? [event] : [],
      });
    };

    if (token) {
      fetchAllLocations();
    }
  }, []);

  const getLocationTypeTitle = (type) => {
    switch (type) {
      case "restaurants":
        return "Recommended Restaurant";
      case "bars":
        return "Recommended Bar";
      case "museums":
        return "Recommended Museum";
      case "events":
        return "Recommended Event";
      default:
        return "";
    }
  };

  return (
    <>
      <NavbarBack></NavbarBack>
      <div className="pt-4">
        {Object.entries(locations).map(([key, locationArray]) =>
          locationArray.length > 0 ? (
            <Box key={key} mb={4}>
              <Typography
                variant="h6"
                component="p"
                sx={{
                  backgroundColor: "#C798C6",
                  color: "white",
                  padding: "8px",
                  borderRadius: "4px",
                  textAlign: "center",
                  marginBottom: "16px",
                  fontWeight: "bold",
                }}
                className="text-white bg-purple-300 p-2 rounded"
                ml={2}
                mr={2}
              >
                {getLocationTypeTitle(key)}
              </Typography>
              {locationArray.map((location, index) => (
                <CustomLocationCard
                  key={`${key}-${index}`}
                  name={location.name || "Unknown Name"}
                  address={location.address || "Unknown Name"} 
                  rating={location.rating || "4.5"} 
                  ratingCount={location.rating || "3"} 
                  type={key.slice(0, -1)} 
                />
              ))}
            </Box>
          ) : null
        )}
      </div>
    </>
  );
};

export default LocationRecommandation;
