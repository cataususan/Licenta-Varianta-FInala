import React from "react";
import Navbar from "../components/Navbar";
import RestaurantButton from "../components/RestaurantButton";
import BarButton from "../components/BarButton";
import MuseumButton from "../components/MuseumButton";
import EventButton from "../components/EventButton";
import TranslateButton from "../components/TranslateButton";
import TransportButton from "../components/TransportButton";
import ExchangeButton from "../components/ExchangeButton";
import RecommendedButton from "../components/RecommendedButton";

function Dashboard() {
  return (
    <div className="flex flex-col h-screen">
      <Navbar />
      <div className="px-10 pt-10 py-2 w-full">
        <RecommendedButton />
      </div>
      <div className="flex-grow flex items-center justify-center">
        <div className="grid grid-cols-2 gap-x-6 gap-y-12">
          <RestaurantButton />
          <BarButton />
          <MuseumButton />
          <EventButton />
        </div>
      </div>
      <div className="flex justify-around items-end mt-auto mb-24">
        <TranslateButton />
        <TransportButton />
        <ExchangeButton />
      </div>
    </div>
  );
}

export default Dashboard;
