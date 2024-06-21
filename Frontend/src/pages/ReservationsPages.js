import React, { useEffect, useState } from "react";
import {
  Box,
  Typography,
  Card,
  CardContent,
  CircularProgress,
  Grid,
} from "@mui/material";
import { format } from "date-fns";
import NavbarBack from "../components/NavbarBack";

const ReservationsPage = () => {
  const [reservations, setReservations] = useState([]);
  const [loading, setLoading] = useState(true);
  const token = localStorage.getItem("token"); 

  useEffect(() => {
    const fetchReservations = async () => {
      try {
        const response = await fetch(
          "https://timtour.pagekite.me/api/Reservation/customerEmail",
          {
            headers: {
              Authorization: `Bearer ${token}`,
              "Content-Type": "application/json",
            },
          }
        );
        const data = await response.json();
        setReservations(data);
        setLoading(false);
      } catch (error) {
        console.error("Error fetching reservations:", error);
        setLoading(false);
      }
    };

    fetchReservations();
  }, [token]);

  if (loading) {
    return <CircularProgress />;
  }

  const getStatusColor = (status) => {
    switch (status) {
      case "pending":
        return "#FFD700";
      case "approved":
        return "#32CD32";
      case "denied":
        return "#FF4500";
      default:
        return "gray";
    }
  };

  return (
    <>
      <NavbarBack />
      <Box
        className="flex flex-col items-center justify-center"
        sx={{ minHeight: "100vh", bgcolor: "background.paper", p: 4 }}
      >
        <Grid container spacing={2}>
          {reservations.map((reservation) => {
            const formattedDate = format(
              new Date(reservation.reservationDate * 1000),
              "MMMM dd, yyyy hh:mm a"
            );
            return (
              <Grid item xs={12} md={6} lg={4} key={reservation.id.timestamp}>
                <Card
                  className="w-full bg-purple-300"
                  sx={{ backgroundColor: "#C798C6" }}
                >
                  <CardContent>
                    <Typography
                      variant="h6"
                      component="h2"
                      style={{ color: "white" }}
                    >
                      {reservation.locationName}
                    </Typography>
                    <Typography variant="body2" style={{ color: "white" }}>
                      {formattedDate}
                    </Typography>
                    <Box
                      component="span"
                      sx={{
                        display: "inline-block",
                        p: "4px 8px",
                        borderRadius: "12px",
                        backgroundColor: getStatusColor(reservation.status),
                        color: "white",
                        mt: 1,
                      }}
                    >
                      <Typography variant="body2">
                        Status: {reservation.status}
                      </Typography>
                    </Box>
                  </CardContent>
                </Card>
              </Grid>
            );
          })}
        </Grid>
      </Box>
    </>
  );
};

export default ReservationsPage;
