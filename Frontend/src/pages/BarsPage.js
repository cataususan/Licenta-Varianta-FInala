import React from "react";
import LocationsList from "../components/LocationList";
import NavbarBars from "../components/NavbarBars";

function App() {
  return (
    <div className="App">
      <NavbarBars></NavbarBars>
      <LocationsList type="bar" url="https://timtour.pagekite.me/api/Bars" />
    </div>
  );
}

export default App;
