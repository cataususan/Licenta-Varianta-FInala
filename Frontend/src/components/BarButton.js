// PurpleIconButton.js
import React from "react";
import IconButton from "@mui/material/IconButton";
import WineBarIcon from '@mui/icons-material/WineBar';
import { Link } from 'react-router-dom';
const BarButton = () => {
  return (
    <Link to="/bars" className="no-underline">
    <div className="hover:bg-purple-700 text-white rounded-lg px-2 py-4 w-32 inline-flex flex-col items-center justify-center text-center cursor-pointer"
         style={{ backgroundColor: '#C798C6' }}>
      <IconButton
        color="inherit"
        aria-label="dinner"
        style={{ color: 'white', justifyContent: 'center', alignItems: 'center', display: 'flex' }}
      >
        <WineBarIcon fontSize="large" />
      </IconButton>
      <div className="text-sm mt-1 font-bold">Bars</div>
    </div>
    </Link>
  );
};

export default BarButton;
