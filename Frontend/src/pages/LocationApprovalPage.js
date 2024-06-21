import React from "react";
import LocationList from "../components/LocationApprovalList";

const LocationApprovalPage = () => {
  return (
    <div>
      <h1 className="text-xl font-bold text-center my-4">
        Pending Locations Approval
      </h1>
      <LocationList
        type="Restaurant"
        endpoint="https://timtour.pagekite.me/api/Restaurants/pendingRestaurant"
        updateEndpoint="https://timtour.pagekite.me/api/Restaurants/updateRestaurantStatus"
      />
      <LocationList
        type="Museum"
        endpoint="https://timtour.pagekite.me/api/Museums/pendingMuseum"
        updateEndpoint="https://timtour.pagekite.me/api/Museums/updateMuseumStatus"
      />
      <LocationList
        type="Bar"
        endpoint="https://timtour.pagekite.me/api/Bars/pendingBar"
        updateEndpoint="https://timtour.pagekite.me/api/Bars/updateBarStatus"
      />
    </div>
  );
};

export default LocationApprovalPage;
