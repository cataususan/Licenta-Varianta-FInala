// App.js
//COLOURS #C798C6 #7C0B6A
//https://timtour.pagekite.me/api/Recommandation/getBarRecommandation"
//https://timtour.serveo.net/api/Recommandation/getBarRecommandation
//python3 pagekite.py 8080 myapp.pagekite.me
import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import PurpleIconButton from "./components/PurpleIconButton";
import BarButton from "./components/BarButton";
import EventButton from "./components/EventButton";
import MuseumButton from "./components/MuseumButton";
import RestaurantButton from "./components/RestaurantButton";
import ExchangeButton from "./components/ExchangeButton";
import TranslateButton from "./components/TranslateButton";
import TransportButton from "./components/TransportButton";
import RegisterPage from "./pages/RegisterPage";
import LoginPage from "./pages/LoginPage";
import RestaurantsPage from "./pages/RestaurantsPage";
import PrefferenceSelector from "./components/PrefferenceSelector";
import PrefferencePage from "./pages/PrefferencePage";
import LocationsList from "./components/LocationList";
import ReservationPage from "./pages/ReservationPage";
import ExchangePage from "./pages/ExchangePage";
import ReservationsList from "./pages/ReservationList";
import LocationApprovalPage from "./pages/LocationApprovalPage";
import RestaurantRecommandation from "./pages/RestaurantRecommandation";
import MapPage from "./pages/MapPage";
import Dashboard from "./pages/Dashboard";
import RestaurantPage from "./pages/RestaurantsPage";
import EventsPage from "./pages/EventsPage";
import BarsPage from "./pages/BarsPage";
import MuseumsPage from "./pages/MuseumsPage";
import CapturePhoto from "./components/CapturePhoto";
import ProfilePage from "./pages/ProfilePage";
import ReservationsPages from "./pages/ReservationsPages";
import AddLocationPage from "./pages/AddLocationPage";
const App = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/restaurants" element={<RestaurantPage />} />
        <Route path="/bars" element={<BarsPage />} />
        <Route path="/museums" element={<MuseumsPage />} />
        <Route path="/events" element={<EventsPage />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/exchange" element={<ExchangePage />} />
        <Route path="/transport" element={<MapPage />} />
        <Route path="/translate" element={<CapturePhoto />} />
        <Route path="/recommendation" element={<RestaurantRecommandation />} />
        <Route path="/reservation/:name/:type" element={<ReservationPage />} />
        <Route path="/locationApproval" element={<LocationApprovalPage />} />
        <Route path="/reservationList" element={<ReservationsList />} />
        <Route path="/prefferencePage" element={<PrefferencePage />} />
        <Route path="/profile" element={<ProfilePage />} />
        <Route path="/myReservations" element={<ReservationsPages />} />
        <Route path="/addLocation" element={<AddLocationPage />} />
      </Routes>
    </Router>
  );
};

export default App;
