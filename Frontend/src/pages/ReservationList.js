import React, { useState, useEffect } from "react";
import ReservationCard from "../components/ReservationCard";

const ReservationsList = () => {
  const [reservations, setReservations] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      const token = localStorage.getItem("token"); 
      const headers = new Headers();
      headers.append("Authorization", `Bearer ${token}`); 

      try {
        const response = await fetch(
          "https://timtour.pagekite.me/api/Reservation/restaurantName?Name=Casa%20Bunicii%201%20Timisoara",
          {
            method: "GET",
            headers: headers,
          }
        );
        if (response.ok) {
          const data = await response.json();
          setReservations(data);
        } else {
          throw new Error("Failed to fetch reservations");
        }
      } catch (error) {
        console.error("Error fetching reservations:", error);
      }
    };

    fetchData();
  }, []);

  return (
    <div className="container mx-auto p-4">
      {reservations.map((reservation) => (
        <ReservationCard
          key={reservation.id.timestamp}
          reservation={reservation}
        />
      ))}
    </div>
  );
};

export default ReservationsList;
