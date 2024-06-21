import React from "react";
import LocationsList from "../components/LocationList";
import NavbarRestaurants from "../components/NavbarRestaurants";
function App() {
  return (
    <div className="App">
      <NavbarRestaurants></NavbarRestaurants>
      <LocationsList
        type="restaurant"
        url="https://timtour.pagekite.me/api/Restaurants"
      />
    </div>
  );
}

export default App;
