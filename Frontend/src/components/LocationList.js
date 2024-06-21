import React, { useEffect, useState } from "react";
import LocationCard from "./LocationCard";

const LocationsList = ({ url, type }) => {
  const [locations, setLocations] = useState([]);

  useEffect(() => {
    if (!url) return;

    fetch(url) 
      .then((response) => response.json())
      .then((data) => {
        setLocations(
          data.map((location) => ({
            id: `${location.id.timestamp}-${location.id.machine}-${location.id.pid}-${location.id.increment}`,
            name: location.name,
            address: location.adress,
            rating: location.rating.ratingValue,
            ratingCount: location.rating.personsNumber,
          }))
        );
      })
      .catch((error) => {
        console.error("Error fetching locations:", error);
      });
  }, [url]);

  return (
    <div>
      {locations.map((location) => (
        <LocationCard
          key={location.id}
          name={location.name}
          address={location.address}
          rating={location.rating}
          ratingCount={location.ratingCount}
          type={type}
        />
      ))}
    </div>
  );
};

export default LocationsList;
