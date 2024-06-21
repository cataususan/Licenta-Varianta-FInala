import React from "react";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import { Link } from "react-router-dom";
import { IconButton } from "@mui/material";

function NavbarMuseums() {
  return (
    <div className="bg-[#C798C6] flex items-center justify-between">
      <Link to="/dashboard" style={{ color: "white", textDecoration: "none" }}>
        <IconButton style={{ color: "white" }}>
          <ArrowBackIcon style={{ color: "white", fontSize: "20px" }} />
        </IconButton>
      </Link>
      <div className="text-center flex-grow text-white font-bold text-lg">
        Museums
      </div>
      <div style={{ width: 48 }}></div>
    </div>
  );
}

export default NavbarMuseums;
