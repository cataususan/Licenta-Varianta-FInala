import React, { useState, useCallback, useRef } from 'react';
import { GoogleMap, DirectionsRenderer, useLoadScript, Autocomplete } from '@react-google-maps/api';
/* global google */

const containerStyle = {
  width: '100%',
  height: '500px'
};


const defaultCenter = {
  lat: 45.756, 
  lng: 21.229
};

const libraries = ["places"];

function MapComponent() {
  const { isLoaded, loadError } = useLoadScript({
    googleMapsApiKey: "api_key",
    libraries
  });

  const [map, setMap] = useState(null);
  const [directions, setDirections] = useState(null);
  const originRef = useRef(null);
  const destinationRef = useRef(null);

  const onLoad = useCallback((map) => {
    const bounds = new window.google.maps.LatLngBounds();
    map.fitBounds(bounds);
    setMap(map);
  }, []);

  const onUnmount = useCallback(() => {
    setMap(null);
  }, []);

  const calculateRoute = async () => {
    if (!originRef.current || !destinationRef.current) {
      alert('Please enter origin and destination.');
      return;
    }

    const originPlace = originRef.current.getPlace();
    const destinationPlace = destinationRef.current.getPlace();

    if (!originPlace || !destinationPlace) {
      alert('Please select valid places from the dropdown.');
      return;
    }

    const directionsService = new google.maps.DirectionsService();
    const results = await directionsService.route({
      origin: originPlace.geometry.location,
      destination: destinationPlace.geometry.location,
      travelMode: google.maps.TravelMode.TRANSIT
    });
    if (results.status === 'OK') {
      setDirections(results);
    } else {
      alert('Directions request failed due to ' + results.status);
    }
  };

  if (loadError) {
    return <div>Map cannot be loaded right now, sorry.</div>;
  }

  return isLoaded ? (
    <div>
      <Autocomplete
        onLoad={(autocomplete) => {
          originRef.current = autocomplete;
        }}
        onPlaceChanged={() => {
          console.log(originRef.current.getPlace());
        }}
      >
        <input type="text" placeholder="Enter origin" 
          className="border-2 border-[#C798C6] rounded-full text-center py-3 px-4 w-full mt-2" />
      </Autocomplete>
      <Autocomplete
        onLoad={(autocomplete) => {
          destinationRef.current = autocomplete;
        }}
        onPlaceChanged={() => {
          console.log(destinationRef.current.getPlace());
        }}
      >
        <input type="text" placeholder="Enter destination" 
          className="border-2 border-[#C798C6] rounded-full text-center py-2 px-4 w-full my-4" />
      </Autocomplete>
      <div className="flex justify-center">
        <button onClick={calculateRoute} 
          className="bg-[#C798C6] text-white py-2 px-4 rounded-full mb-4">
          Get Directions
        </button>
      </div>
      <GoogleMap
        mapContainerStyle={containerStyle}
        center={defaultCenter}
        zoom={10}
        onLoad={onLoad}
        onUnmount={onUnmount}
      >
        {directions && <DirectionsRenderer directions={directions} />}
      </GoogleMap>
    </div>
  ) : <div>Loading...</div>;
}

export default MapComponent;
