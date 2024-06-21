import React from 'react';
import GoogleMapsLoader from './GoogleMapsLoader';
import MapComponent from '../components/MapComponent';
import Navbar from '../components/NavbarBack';

function MapPage() {
  return (
    <GoogleMapsLoader>
      <Navbar />
      <MapComponent />
    </GoogleMapsLoader>
  );
}

export default MapPage;
