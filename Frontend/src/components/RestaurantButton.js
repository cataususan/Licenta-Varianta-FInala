import React from "react";
import IconButton from "@mui/material/IconButton";
import { Link } from 'react-router-dom';
import DinnerDiningIcon from '@mui/icons-material/DinnerDining';

const RestaurantButton = () => {
  return (
    <Link to="/restaurants" className="no-underline">
    <div className="hover:bg-purple-700 text-white rounded-lg px-2 py-4 w-32 inline-flex flex-col items-center justify-center text-center cursor-pointer"
         style={{ backgroundColor: '#C798C6' }}>
      <IconButton
        color="inherit"
        aria-label="dinner"
        style={{ color: 'white', justifyContent: 'center', alignItems: 'center', display: 'flex' }}
      >
        <DinnerDiningIcon fontSize="large" />
      </IconButton>
      <div className="text-sm mt-1 font-bold">Restaurants</div>
    </div>
    </Link>
  );
};

export default RestaurantButton;
