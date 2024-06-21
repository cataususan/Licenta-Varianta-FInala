import React from "react";
import { Button } from "@mui/material";

const LocationApproval = ({ location, updateEndpoint, type }) => {
  const formatSchedule = (schedule) => {
    return schedule
      .map((day) => `${day.name}: ${day.openTime} - ${day.closeTime}`)
      .join(", ");
  };

  const updateLocationStatus = async (status) => {
    let body = JSON.stringify({
      [`${type.toLowerCase()}Name`]: location.name,
      [`${type.toLowerCase()}Status`]: status,
    });
    console.log(body)

    const token = localStorage.getItem("token");
    try {
      const response = await fetch(updateEndpoint, {
        method: "PATCH",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: body,
      });
      if (!response.ok) throw new Error("Failed to update location status");
      console.log("Location status updated successfully");
    } catch (error) {
      console.error("Error updating location status:", error);
    }
  };

  return (
    <div className="bg-white rounded-lg p-4 mb-4 shadow-md w-full flex justify-between">
      <div>
        <h3 className="text-lg font-semibold">{location.name}</h3>
        <p>{formatSchedule(location.schedule)}</p>
      </div>
      <div className="flex items-center gap-2">
        <Button
          variant="contained"
          color="success"
          onClick={() => updateLocationStatus("approved")}
        >
          Approve
        </Button>
        <Button
          variant="contained"
          color="error"
          onClick={() => updateLocationStatus("denied")}
        >
          Deny
        </Button>
      </div>
    </div>
  );
};

export default LocationApproval;
