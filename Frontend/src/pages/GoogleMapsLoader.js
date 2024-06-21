import React from "react";
import { LoadScriptNext } from "@react-google-maps/api";

const googleMapsApiKey = "api_key"; // Replace with your actual API key
const libraries = ["places"]; // Specify any libraries you need

const GoogleMapsLoader = ({ children }) => {
  return (
    <LoadScriptNext googleMapsApiKey={googleMapsApiKey} libraries={libraries}>
      {children}
    </LoadScriptNext>
  );
};

export default GoogleMapsLoader;
