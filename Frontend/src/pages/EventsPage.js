import React from "react";
import LocationsList from "../components/LocationList";
import NavbarEvents from "../components/NavbarEvents";
import EventList from "../components/EventList";
function App() {
  return (
    <div className="App">
      <NavbarEvents></NavbarEvents>
      <EventList type="event" url="https://timtour.pagekite.me/api/Events" />
    </div>
  );
}

export default App;
