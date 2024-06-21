import React from "react";
import IconButton from "@mui/material/IconButton";
import CurrencyExchangeIcon from '@mui/icons-material/CurrencyExchange';
import { Link } from 'react-router-dom';

const ExchangeButton = () => {
  return (
    <Link to="/exchange" className="no-underline">
    <div className="hover:bg-purple-700 text-white rounded-full p-2 w-20 h-20 flex justify-center items-center cursor-pointer"
         style={{ backgroundColor: '#C798C6' }}>
      <IconButton
        color="secondary"
        aria-label="exchange"
        className="w-full h-full"
        style={{ fontSize: '4rem', color: 'white' }} 
      >
        <CurrencyExchangeIcon style={{ color: 'inherit' }} />
      </IconButton>
    </div>
    </Link>
  );
};

export default ExchangeButton;
