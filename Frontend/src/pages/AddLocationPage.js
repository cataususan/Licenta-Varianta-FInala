import React, { useState } from "react";
import {
  Box,
  Typography,
  Button,
  TextField,
  MenuItem,
  Grid,
} from "@mui/material";
import NavbarBack from "../components/NavbarBack";

const AddLocationPage = () => {
  const [locationType, setLocationType] = useState("");
  const [formData, setFormData] = useState({
    name: "",
    address: "",
    schedule: [],
    location: { lat: 0, lng: 0 },
    status: "",
    features: {},
  });

  const handleTypeChange = (event) => {
    setLocationType(event.target.value);
    setFormData({
      name: "",
      address: "",
      schedule: [],
      location: { lat: 0, lng: 0 },
      status: "",
      features: {},
    });
  };

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    if (name === "lat" || name === "lng") {
      setFormData((prev) => ({
        ...prev,
        location: { ...prev.location, [name]: parseFloat(value) },
      }));
    } else {
      setFormData((prev) => ({ ...prev, [name]: value }));
    }
  };

  const handleScheduleChange = (day, openTime, closeTime) => {
    setFormData((prev) => ({
      ...prev,
      schedule: [
        ...prev.schedule.filter((s) => s.name !== day),
        { name: day, openTime, closeTime },
      ],
    }));
  };

  const handleFeatureChange = (event) => {
    const { name, value } = event.target;
    setFormData((prev) => ({
      ...prev,
      features: { ...prev.features, [name]: value.toUpperCase() },
    }));
  };

  const handleSubmit = async () => {
    const token = localStorage.getItem("token");
    let url = "";
    let body = {};

    switch (locationType) {
      case "bar":
        url = "https://timtour.pagekite.me/api/Bars/addBar";
        body = {
          bar: {
            id: {},
            name: formData.name,
            rating: { ratingValue: 0, personsNumber: 0 },
            location: formData.location,
            adress: formData.address,
            schedule: formData.schedule,
            status: formData.status,
            features: {
              type: formData.features.type || "NONE",
              barEvent: formData.features.barEvent || "NONE",
              barAmbiance: formData.features.barAmbiance || "NONE",
              barDrinkSpecialties:
                formData.features.barDrinkSpecialties || "NONE",
              barFoodOptions: formData.features.barFoodOptions || "NONE",
              priceRange: formData.features.priceRange || "NONE",
            },
          },
        };
        break;
      case "restaurant":
        url = "https://timtour.pagekite.me/api/Restaurants";
        body = {
          restaurant: {
            id: {},
            name: formData.name,
            rating: { ratingValue: 0, personsNumber: 0 },
            location: formData.location,
            adress: formData.address,
            schedule: formData.schedule,
            status: formData.status,
            features: {
              type: formData.features.type || "NONE",
              barEvent: formData.features.barEvent || "NONE",
              barAmbiance: formData.features.barAmbiance || "NONE",
              barDrinkSpecialties:
                formData.features.barDrinkSpecialties || "NONE",
              barFoodOptions: formData.features.barFoodOptions || "NONE",
              priceRange: formData.features.priceRange || "NONE",
            },
          },
        };
        break;
      case "museum":
        url = "https://timtour.pagekite.me/api/Museums";
        body = {
          museum: {
            id: {},
            name: formData.name,
            rating: { ratingValue: 0, personsNumber: 0 },
            location: formData.location,
            adress: formData.address,
            schedule: formData.schedule,
            status: formData.status,
            features: {
              type: formData.features.type || "NONE",
              barEvent: formData.features.barEvent || "NONE",
              barAmbiance: formData.features.barAmbiance || "NONE",
              barDrinkSpecialties:
                formData.features.barDrinkSpecialties || "NONE",
              barFoodOptions: formData.features.barFoodOptions || "NONE",
              priceRange: formData.features.priceRange || "NONE",
            },
          },
        };
        break;
      case "event":
        url = "https://timtour.pagekite.me/api/Events";
        body = {
          eventAdded: {
            id: {},
            name: formData.name,
            rating: { ratingValue: 0, personsNumber: 0 },
            location: formData.location,
            adress: formData.address,
            schedule: formData.schedule,
            status: formData.status,
            features: {
              type: formData.features.type || "NONE",
              barEvent: formData.features.barEvent || "NONE",
              barAmbiance: formData.features.barAmbiance || "NONE",
              barDrinkSpecialties:
                formData.features.barDrinkSpecialties || "NONE",
              barFoodOptions: formData.features.barFoodOptions || "NONE",
              priceRange: formData.features.priceRange || "NONE",
            },
            eventDate: Date.now(),
          },
        };
        break;
      default:
        return;
    }

    try {
      const response = await fetch(url, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(body),
      });
      const data = await response.json();
      console.log("Response from backend:", data);
    } catch (error) {
      console.error("Error submitting location:", error);
    }
  };

  return (
    <>
      <NavbarBack />
      <Box className="flex flex-col items-center justify-center p-4">
        <Typography variant="h4" component="h1" gutterBottom>
          Add a New Location
        </Typography>
        {!locationType && (
          <TextField
            select
            label="Select Location Type"
            value={locationType}
            onChange={handleTypeChange}
            variant="outlined"
            fullWidth
            margin="normal"
          >
            <MenuItem value="bar">Bar</MenuItem>
            <MenuItem value="restaurant">Restaurant</MenuItem>
            <MenuItem value="museum">Museum</MenuItem>
            <MenuItem value="event">Event</MenuItem>
          </TextField>
        )}
        {locationType && (
          <form className="w-full max-w-lg">
            <TextField
              label="Name"
              name="name"
              value={formData.name}
              onChange={handleInputChange}
              variant="outlined"
              fullWidth
              margin="normal"
            />
            <TextField
              label="Address"
              name="address"
              value={formData.address}
              onChange={handleInputChange}
              variant="outlined"
              fullWidth
              margin="normal"
            />
            <TextField
              label="Latitude"
              name="lat"
              value={formData.location.lat}
              onChange={handleInputChange}
              variant="outlined"
              fullWidth
              margin="normal"
            />
            <TextField
              label="Longitude"
              name="lng"
              value={formData.location.lng}
              onChange={handleInputChange}
              variant="outlined"
              fullWidth
              margin="normal"
            />
            {locationType !== "event" && (
              <>
                {["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"].map(
                  (day) => (
                    <Grid container spacing={2} key={day}>
                      <Grid item xs={4}>
                        <Typography>{day}</Typography>
                      </Grid>
                      <Grid item xs={4}>
                        <TextField
                          name="openTime"
                          type="time"
                          variant="outlined"
                          fullWidth
                          onChange={(e) =>
                            handleScheduleChange(
                              day,
                              e.target.value,
                              formData.schedule.find((s) => s.name === day)
                                ?.closeTime || ""
                            )
                          }
                        />
                      </Grid>
                      <Grid item xs={4}>
                        <TextField
                          name="closeTime"
                          type="time"
                          variant="outlined"
                          fullWidth
                          onChange={(e) =>
                            handleScheduleChange(
                              day,
                              formData.schedule.find((s) => s.name === day)
                                ?.openTime || "",
                              e.target.value
                            )
                          }
                        />
                      </Grid>
                    </Grid>
                  )
                )}
              </>
            )}
            {locationType === "event" && (
              <TextField
                label="Event Date"
                name="eventDate"
                type="datetime-local"
                value={formData.eventDate}
                onChange={handleInputChange}
                variant="outlined"
                fullWidth
                margin="normal"
              />
            )}
            {locationType === "bar" && (
              <>
                <TextField
                  select
                  label="Bar Type"
                  name="type"
                  value={formData.features.type || ""}
                  onChange={handleFeatureChange}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                >
                  <MenuItem value="NONE">None</MenuItem>
                  <MenuItem value="SPORTS_BAR">Sports Bar</MenuItem>
                  <MenuItem value="DIVE_BAR">Dive Bar</MenuItem>
                  <MenuItem value="COCKTAIL_BAR">Cocktail Bar</MenuItem>
                  <MenuItem value="WINE_BAR">Wine Bar</MenuItem>
                  <MenuItem value="LIVE_MUSIC_BAR">Live Music Bar</MenuItem>
                  <MenuItem value="GAY_BAR">Gay Bar</MenuItem>
                </TextField>
                <TextField
                  select
                  label="Bar Event"
                  name="barEvent"
                  value={formData.features.barEvent || ""}
                  onChange={handleFeatureChange}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                >
                  <MenuItem value="NONE">None</MenuItem>
                  <MenuItem value="NO_EVENTS">No Events</MenuItem>
                  <MenuItem value="TRIVIA_NIGHTS">Trivia Nights</MenuItem>
                  <MenuItem value="KARAOKE_NIGHTS">Karaoke Nights</MenuItem>
                  <MenuItem value="SPORTS_VIEWING_PARTIES">
                    Sports Viewing Parties
                  </MenuItem>
                  <MenuItem value="COMEDY_NIGHTS">Comedy Nights</MenuItem>
                </TextField>
                <TextField
                  select
                  label="Bar Ambiance"
                  name="barAmbiance"
                  value={formData.features.barAmbiance || ""}
                  onChange={handleFeatureChange}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                >
                  <MenuItem value="NONE">None</MenuItem>
                  <MenuItem value="COZY">Cozy</MenuItem>
                  <MenuItem value="RUSTIC">Rustic</MenuItem>
                  <MenuItem value="INDUSTRIAL">Industrial</MenuItem>
                  <MenuItem value="OPEN_AIR">Open Air</MenuItem>
                </TextField>
                <TextField
                  select
                  label="Bar Drink Specialties"
                  name="barDrinkSpecialties"
                  value={formData.features.barDrinkSpecialties || ""}
                  onChange={handleFeatureChange}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                >
                  <MenuItem value="NONE">None</MenuItem>
                  <MenuItem value="CLASSIC_COCKTAILS">
                    Classic Cocktails
                  </MenuItem>
                  <MenuItem value="WINE_BARS">Wine Bars</MenuItem>
                  <MenuItem value="NON_ALCOHOLIC">Non-Alcoholic</MenuItem>
                  <MenuItem value="INFUSED_SPIRITS">Infused Spirits</MenuItem>
                </TextField>
                <TextField
                  select
                  label="Bar Food Options"
                  name="barFoodOptions"
                  value={formData.features.barFoodOptions || ""}
                  onChange={handleFeatureChange}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                >
                  <MenuItem value="NONE">None</MenuItem>
                  <MenuItem value="NO_FOOD">No Food</MenuItem>
                  <MenuItem value="APPETIZERS">Appetizers</MenuItem>
                  <MenuItem value="FRIED_FOOD">Fried Food</MenuItem>
                  <MenuItem value="DESSERTS">Desserts</MenuItem>
                </TextField>
                <TextField
                  select
                  label="Price Range"
                  name="priceRange"
                  value={formData.features.priceRange || ""}
                  onChange={handleFeatureChange}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                >
                  <MenuItem value="NONE">None</MenuItem>
                  <MenuItem value="LOW">Low</MenuItem>
                  <MenuItem value="MEDIUM">Medium</MenuItem>
                  <MenuItem value="HIGH">High</MenuItem>
                </TextField>
              </>
            )}
            {locationType === "restaurant" && (
              <>
                <TextField
                  select
                  label="Atmosphere"
                  name="atmosphere"
                  value={formData.features.atmosphere || ""}
                  onChange={handleFeatureChange}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                >
                  <MenuItem value="NONE">None</MenuItem>
                  <MenuItem value="CASUAL">Casual</MenuItem>
                  <MenuItem value="ELEGANT">Elegant</MenuItem>
                  <MenuItem value="HISTORIC">Historic</MenuItem>
                  <MenuItem value="OUTDOOR">Outdoor</MenuItem>
                </TextField>
                <TextField
                  select
                  label="Cuisine Types"
                  name="cusineTypes"
                  value={formData.features.cusineTypes || ""}
                  onChange={handleFeatureChange}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                >
                  <MenuItem value="NONE">None</MenuItem>
                  <MenuItem value="ITALIAN">Italian</MenuItem>
                  <MenuItem value="MEXICAN">Mexican</MenuItem>
                  <MenuItem value="JAPANESE">Japanese</MenuItem>
                  <MenuItem value="CHINESE">Chinese</MenuItem>
                  <MenuItem value="INDIAN">Indian</MenuItem>
                  <MenuItem value="ROMANIAN">Romanian</MenuItem>
                </TextField>
                <TextField
                  select
                  label="Dietary Restrictions"
                  name="dietaryRestrictions"
                  value={formData.features.dietaryRestrictions || ""}
                  onChange={handleFeatureChange}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                >
                  <MenuItem value="NONE">None</MenuItem>
                  <MenuItem value="VEGETARIAN">Vegetarian</MenuItem>
                  <MenuItem value="VEGAN">Vegan</MenuItem>
                  <MenuItem value="LACTOSE_FREE">Lactose Free</MenuItem>
                  <MenuItem value="GLUTEN_FREE">Gluten Free</MenuItem>
                  <MenuItem value="SUGAR_FREE">Sugar Free</MenuItem>
                </TextField>
                <TextField
                  select
                  label="Special Features"
                  name="specialFeatures"
                  value={formData.features.specialFeatures || ""}
                  onChange={handleFeatureChange}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                >
                  <MenuItem value="NONE">None</MenuItem>
                  <MenuItem value="OPEN_KITCHEN">Open Kitchen</MenuItem>
                  <MenuItem value="CHEFS_TABLE">Chef's Table</MenuItem>
                  <MenuItem value="COOKING_CLASSES">Cooking Classes</MenuItem>
                </TextField>
                <TextField
                  select
                  label="Price Range"
                  name="priceRange"
                  value={formData.features.priceRange || ""}
                  onChange={handleFeatureChange}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                >
                  <MenuItem value="NONE">None</MenuItem>
                  <MenuItem value="LOW">Low</MenuItem>
                  <MenuItem value="MEDIUM">Medium</MenuItem>
                  <MenuItem value="HIGH">High</MenuItem>
                </TextField>
              </>
            )}
            {locationType === "museum" && (
              <>
                <TextField
                  select
                  label="Museum Accessibility"
                  name="museumAccesibility"
                  value={formData.features.museumAccesibility || ""}
                  onChange={handleFeatureChange}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                >
                  <MenuItem value="NONE">None</MenuItem>
                  <MenuItem value="WHEELCHAIR_ACCESSIBILITY">
                    Wheelchair Accessibility
                  </MenuItem>
                  <MenuItem value="TACTILE_TOURS">Tactile Tours</MenuItem>
                  <MenuItem value="SIGN_LANGUAGE_INTERPRETED_TOURS">
                    Sign Language Interpreted Tours
                  </MenuItem>
                  <MenuItem value="SERVICE_ANIMALS_ALLOWED">
                    Service Animals Allowed
                  </MenuItem>
                </TextField>
                <TextField
                  select
                  label="Exhibits Types"
                  name="museumExhibitsTypes"
                  value={formData.features.museumExhibitsTypes || ""}
                  onChange={handleFeatureChange}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                >
                  <MenuItem value="NONE">None</MenuItem>
                  <MenuItem value="PERMANENT">Permanent</MenuItem>
                  <MenuItem value="TEMPORARY">Temporary</MenuItem>
                  <MenuItem value="INTERACTIVE">Interactive</MenuItem>
                  <MenuItem value="ART">Art</MenuItem>
                  <MenuItem value="HISTORICAL">Historical</MenuItem>
                </TextField>
                <TextField
                  select
                  label="Museum Types"
                  name="museumTypes"
                  value={formData.features.museumTypes || ""}
                  onChange={handleFeatureChange}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                >
                  <MenuItem value="NONE">None</MenuItem>
                  <MenuItem value="ART">Art</MenuItem>
                  <MenuItem value="HISTORY">History</MenuItem>
                  <MenuItem value="SCIENCE">Science</MenuItem>
                  <MenuItem value="TECHNOLOGY">Technology</MenuItem>
                  <MenuItem value="MILITARY">Military</MenuItem>
                  <MenuItem value="ARCHEOLOGICAL">Archeological</MenuItem>
                </TextField>
                <TextField
                  select
                  label="Visitor Services"
                  name="museumVisitorService"
                  value={formData.features.museumVisitorService || ""}
                  onChange={handleFeatureChange}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                >
                  <MenuItem value="NONE">None</MenuItem>
                  <MenuItem value="GUIDED_TOURS">Guided Tours</MenuItem>
                  <MenuItem value="AUDIO_GUIDES">Audio Guides</MenuItem>
                  <MenuItem value="VISITOR_LOUNGES">Visitor Lounges</MenuItem>
                  <MenuItem value="CHILDREN_ACTIVITIES">
                    Children Activities
                  </MenuItem>
                </TextField>
                <TextField
                  select
                  label="Price Range"
                  name="priceRange"
                  value={formData.features.priceRange || ""}
                  onChange={handleFeatureChange}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                >
                  <MenuItem value="NONE">None</MenuItem>
                  <MenuItem value="LOW">Low</MenuItem>
                  <MenuItem value="MEDIUM">Medium</MenuItem>
                  <MenuItem value="HIGH">High</MenuItem>
                </TextField>
              </>
            )}
            <Button
              variant="contained"
              style={{
                backgroundColor: "#C798C6",
                color: "white",
                marginTop: "16px",
              }}
              onClick={handleSubmit}
            >
              Submit
            </Button>
          </form>
        )}
      </Box>
    </>
  );
};

export default AddLocationPage;
