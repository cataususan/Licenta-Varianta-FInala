import React from "react";
import { Button } from "@mui/material";

const ReservationCard = ({ reservation }) => {
  const formatDate = (unixTimestamp) => {
    const date = new Date(unixTimestamp * 1000);
    return date.toLocaleDateString("en-US", {
      year: "numeric",
      month: "long",
      day: "numeric",
      hour: "2-digit",
      minute: "2-digit",
    });
  };

  const updateReservationStatus = async (status) => {
    const token = localStorage.getItem("token");
    const body = JSON.stringify({
      locationName: "Casa Bunicii 1 Timisoara",
      locationType: "restaurant",
      customerName: reservation.customerName,
      customerEmail: reservation.customerEmail,
      status: status,
    });

    try {
      const response = await fetch(
        "https://timtour.pagekite.me/api/Reservation/updateReservationStatus",
        {
          method: "PATCH",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
          body: body,
        }
      );
      if (response.ok) {
        console.log("Reservation status updated successfully");
      } else {
        throw new Error("Failed to update reservation status");
      }
    } catch (error) {
      console.error("Error updating reservation status:", error);
    }
  };

  return (
    <div className="bg-white rounded-lg p-4 mb-4 shadow-md w-full">
      <div className="flex justify-between items-center">
        <div>
          <h3 className="text-lg font-semibold">{reservation.customerName}</h3>
          <p>{formatDate(reservation.reservationDate)}</p>
        </div>
        <div>
          <Button
            variant="contained"
            color="success"
            className="mr-2"
            onClick={() => updateReservationStatus("approved")}
          >
            Approve
          </Button>
          <Button
            variant="contained"
            color="error"
            onClick={() => updateReservationStatus("denied")}
          >
            Deny
          </Button>
        </div>
      </div>
    </div>
  );
};

export default ReservationCard;
