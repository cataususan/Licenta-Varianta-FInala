import React, { useState } from "react";
import {
  Card,
  CardContent,
  CardMedia,
  Typography,
  Box,
  Button,
  Modal,
  Fade,
  Backdrop,
  Rating,
} from "@mui/material";
import { Star } from "@mui/icons-material";
import { useNavigate } from "react-router-dom";
import axios from "axios";

const LocationCard = ({ name, address, rating, ratingCount, type }) => {
  const [open, setOpen] = useState(false);
  const [userRating, setUserRating] = useState(0);
  const navigate = useNavigate();

  const handleReservation = () => {
    navigate(`/reservation/${name}/${type}`);
  };

  const handleRate = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const handleSubmitRating = async () => {
    const token = localStorage.getItem("token"); 
    try {
      await axios.post(
        "https://timtour.pagekite.me/api/Rating",
        {
          name,
          rating: userRating,
          type,
        },
        {
          headers: {
            Authorization: `Bearer ${token}`, 
          },
        }
      );
      setOpen(false);
    } catch (error) {
      console.error("Error submitting rating:", error);
    }
  };

  return (
    <>
      <Card
        className="flex m-2 bg-purple-300"
        style={{ backgroundColor: "#C798C6", height: "100%" }}
      >
        <Box
          sx={{
            width: "130px", 
            height: "130px", 
            display: "flex",
            flexDirection: "column",
            justifyContent: "center",
            paddingLeft: "15px",
            flexShrink: 0,
            margin: "auto",
          }}
        >
          <CardMedia
            component="img"
            sx={{
              width: "100%",
              height: "100%",
              borderRadius: "4px",
              objectFit: "cover",
            }}
            image="https://picsum.photos/150"
            alt="Location Image"
          />
        </Box>
        <CardContent className="flex-grow text-white flex flex-col justify-between">
          <Box>
            <Typography variant="h5" component="div" className="font-bold">
              {name}
            </Typography>
          </Box>
          <Box className="flex justify-start mt-2 space-x-2">
            <Button
              variant="contained"
              onClick={handleReservation}
              style={{
                backgroundColor: "#C798C6",
                color: "white",
                borderColor: "white",
                borderWidth: "1px",
                borderStyle: "solid",
              }}
            >
              Make Reservation
            </Button>
            <Button
              variant="contained"
              onClick={handleRate}
              style={{
                backgroundColor: "#C798C6",
                color: "white",
                borderColor: "white",
                borderWidth: "1px",
                borderStyle: "solid",
              }}
            >
              Rate
            </Button>
          </Box>
        </CardContent>
      </Card>

      <Modal
        open={open}
        onClose={handleClose}
        closeAfterTransition
        BackdropComponent={Backdrop}
        BackdropProps={{
          timeout: 500,
        }}
      >
        <Fade in={open}>
          <Box
            className="flex flex-col items-center justify-center p-4"
            sx={{
              position: "absolute",
              top: "50%",
              left: "50%",
              transform: "translate(-50%, -50%)",
              width: 300,
              bgcolor: "white",
              border: "2px solid #000",
              boxShadow: 24,
              p: 4,
              borderRadius: "8px",
            }}
          >
            <Typography
              variant="h6"
              component="h2"
              className="mb-4"
              style={{ color: "#C798C6" }}
            >
              Rate {name}
            </Typography>
            <Rating
              name="user-rating"
              value={userRating}
              onChange={(event, newValue) => {
                setUserRating(newValue);
              }}
              size="large"
              style={{ marginBottom: "16px" }}
            />
            <Box className="flex justify-between w-full">
              <Button
                variant="contained"
                onClick={handleClose}
                style={{
                  backgroundColor: "#C798C6",
                  color: "white",
                  borderColor: "white",
                  borderWidth: "1px",
                  borderStyle: "solid",
                }}
              >
                Back
              </Button>
              <Button
                variant="contained"
                onClick={handleSubmitRating}
                style={{
                  backgroundColor: "#C798C6",
                  color: "white",
                  borderColor: "white",
                  borderWidth: "1px",
                  borderStyle: "solid",
                }}
              >
                Submit Rating
              </Button>
            </Box>
          </Box>
        </Fade>
      </Modal>
    </>
  );
};

export default LocationCard;
