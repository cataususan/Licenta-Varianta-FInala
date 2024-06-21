import React, { useEffect, useState } from "react";
import EventCard from "./EventCard";

const EventList = ({ url, type }) => {

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
        <EventCard
          key={location.id}
          name={location.name}
          address={location.address}
          type={type}
        />
      ))}
    </div>
  );
};

export default EventList;
