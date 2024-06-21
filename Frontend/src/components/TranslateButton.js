// RoundIconButton.js
import React from "react";
import IconButton from "@mui/material/IconButton";
import TranslateIcon from '@mui/icons-material/Translate';
import { Link } from 'react-router-dom';

const TranslateButton = () => {
  return (
    <Link to="/translate" className="no-underline">
    <div className="hover:bg-purple-700 text-white rounded-full p-2 w-20 h-20 flex justify-center items-center cursor-pointer"
         style={{ backgroundColor: '#C798C6' }}> 
      <IconButton
        color="secondary"
        aria-label="exchange"
        className="w-full h-full"
        style={{ fontSize: '4rem', color: 'white' }}  
      >
        <TranslateIcon style={{ color: 'inherit' }} />
      </IconButton>
    </div>
    </Link>
  );
};

export default TranslateButton;
