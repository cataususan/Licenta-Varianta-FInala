import React, { useState, useEffect } from "react";
import LocationApproval from "./LocationApproval";

const fetchLocations = async (endpoint, setLocations, token) => {
  try {
    const response = await fetch(endpoint, {
      method: "GET",
      headers: { Authorization: `Bearer ${token}` },
    });
    if (response.ok) {
      const data = await response.json();
      setLocations(data);
    } else {
      throw new Error("Failed to fetch locations");
    }
  } catch (error) {
    console.error("Error:", error);
    setLocations([]);
  }
};

const LocationApprovalList = ({ type, endpoint, updateEndpoint }) => {
  const [locations, setLocations] = useState([]);
  const token = localStorage.getItem("token");

  useEffect(() => {
    fetchLocations(endpoint, setLocations, token);
  }, [endpoint, token]);

  if (locations.length === 0) {
    return (
      <div>
        <p>{type}</p>
        <p className="text-center">
          There are no locations that need approval.
        </p>
      </div>
    );
  }

  return (
    <div className="container mx-auto p-4">
      <div>{type}</div>
      {locations.map((location) => (
        <LocationApproval
          key={location.id.timestamp}
          location={location}
          updateEndpoint={updateEndpoint}
          type={type}
        />
      ))}
    </div>
  );
};

export default LocationApprovalList;
