// RecommendedButton.js
import React from "react";
import { Link } from "react-router-dom";
import Button from "@mui/material/Button";

const RecommendedButton = () => {
  return (
    <Link to="/recommendation" className="no-underline">
      <Button
        variant="contained"
        style={{
          backgroundColor: '#C798C6', 
          color: 'white',
          fontWeight: 'bold' 
        }}
        className="w-full" 
      >
        Recommended for You
      </Button>
    </Link>
  );
};

export default RecommendedButton;
