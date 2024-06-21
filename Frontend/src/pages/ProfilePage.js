import React, { useEffect, useState } from "react";
import {
  Avatar,
  Box,
  Button,
  Typography,
  CircularProgress,
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import NavbarBack from "../components/NavbarBack";

const ProfilePage = () => {
  const [userName, setUserName] = useState("");
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();
  const token = localStorage.getItem("token");

  useEffect(() => {
    const fetchUserName = async () => {
      try {
        const response = await fetch(
          "https://timtour.pagekite.me/api/User/name",
          {
            headers: {
              Authorization: `Bearer ${token}`,
              "Content-Type": "application/json",
            },
          }
        );
        const data = await response.json();
        setUserName(data.name);
        setLoading(false);
      } catch (error) {
        console.error("Error fetching user name:", error);
        setLoading(false);
      }
    };

    fetchUserName();
  }, [token]);

  if (loading) {
    return <CircularProgress />;
  }

  return (
    <>
      <NavbarBack></NavbarBack>
      <Box
        className="flex flex-col items-center justify-center"
        sx={{
          height: "100vh",
          bgcolor: "background.paper",
          p: 4,
        }}
      >
        <Avatar
          alt="User Profile Picture"
          src="https://mui.com/static/images/avatar/1.jpg"
          sx={{ width: 100, height: 100, mb: 2 }}
        />
        <Typography variant="h5" component="h1" gutterBottom>
          {userName}
        </Typography>
        <Box className="flex flex-col items-center space-y-2">
          <Button
            variant="contained"
            onClick={() => navigate("/prefferencePage")}
            style={{
              backgroundColor: "#C798C6",
              color: "white",
              borderColor: "white",
              borderWidth: "1px",
              borderStyle: "solid",
              width: "200px",
            }}
          >
            Change My Preferences
          </Button>
          <Button
            variant="contained"
            onClick={() => navigate("/myReservations")}
            style={{
              backgroundColor: "#C798C6",
              color: "white",
              borderColor: "white",
              borderWidth: "1px",
              borderStyle: "solid",
              width: "200px",
            }}
          >
            View My Reservations
          </Button>
        </Box>
      </Box>
    </>
  );
};

export default ProfilePage;
