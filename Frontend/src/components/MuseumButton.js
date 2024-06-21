// PurpleIconButton.js
import React from "react";
import IconButton from "@mui/material/IconButton";
import MuseumIcon from '@mui/icons-material/Museum';
import { Link } from 'react-router-dom';

const MuseumButton = () => {
  return (
    <Link to="/museums" className="no-underline">
    <div className="hover:bg-purple-700 text-white rounded-lg px-2 py-4 w-32 inline-flex flex-col items-center justify-center text-center cursor-pointer"
         style={{ backgroundColor: '#C798C6' }}>
      <IconButton
        color="inherit"
        aria-label="dinner"
        style={{ color: 'white', justifyContent: 'center', alignItems: 'center', display: 'flex' }}
      >
        <MuseumIcon fontSize="large" />
      </IconButton>
      <div className="text-sm mt-1 font-bold">Museums</div>
    </div>
    </Link>
  );
};

export default MuseumButton;
