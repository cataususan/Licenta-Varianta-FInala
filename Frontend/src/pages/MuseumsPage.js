import React from "react";
import LocationsList from "../components/LocationList";
import NavbarMuseums from "../components/NavbarMuseums";
function App() {
  return (
    <div className="App">
      <NavbarMuseums></NavbarMuseums>
      <LocationsList
        type="museum"
        url="https://timtour.pagekite.me/api/Museums"
      />
    </div>
  );
}

export default App;
