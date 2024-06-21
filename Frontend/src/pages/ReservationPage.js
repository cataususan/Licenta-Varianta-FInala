import React, { useState } from "react";
import { useParams } from "react-router-dom";
import { TextField, Button } from "@mui/material";

const ReservationPage = () => {
  const { name, type } = useParams(); 
  const [dateTime, setDateTime] = useState(""); 

  const handleDateTimeChange = (event) => {
    setDateTime(event.target.value);
  };

  const handleSubmit = async () => {
    const formattedDateTime = dateTime.replace("T", " "); 
    const token = localStorage.getItem("token"); 

    const requestOptions = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`, 
      },
      body: JSON.stringify({
        locationName: name,
        locationType: type,
        reservationDateAndTime: formattedDateTime,
      }),
    };

    try {
      const response = await fetch(
        "https://timtour.pagekite.me/api/Reservation/makeReservation",
        requestOptions
      );
      const data = await response.json();
      if (response.ok) {
        console.log("Reservation successful:", data);
        
      } else {
        console.error("Failed to make reservation:", data);
        
      }
    } catch (error) {
      console.error("Network error:", error);
      
    }
  };

  return (
    <div style={{ padding: 20 }}>
      <h1>Make a Reservation</h1>
      <TextField
        label="Location Name"
        variant="outlined"
        fullWidth
        margin="normal"
        value={name}
        disabled
      />
      <TextField
        label="Type"
        variant="outlined"
        fullWidth
        margin="normal"
        value={type}
        disabled
      />
      <TextField
        label="Select Date and Time"
        type="datetime-local"
        value={dateTime}
        onChange={handleDateTimeChange}
        InputLabelProps={{
          shrink: true,
        }}
        fullWidth
        margin="normal"
        style={{ display: "block" }}
      />
      <Button
        onClick={handleSubmit}
        variant="contained"
        disabled={!dateTime} 
        style={{
          marginTop: "20px",
          backgroundColor: "#C798C6", 
          color: "white", 
          display: "block", 
          marginLeft: "auto", 
          marginRight: "auto",
          width: "fit-content", 
        }}
      >
        Confirm Reservation
      </Button>
    </div>
  );
};

export default ReservationPage;
