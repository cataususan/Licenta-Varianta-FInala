import React from "react";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import { Link } from "react-router-dom";
import { IconButton } from "@mui/material";

function NavbarBack() {
  return (
    <div className="bg-[#C798C6] flex items-center">
      <Link to="/dashboard" style={{ color: "white", textDecoration: "none" }}>
        <IconButton style={{ color: "white" }}>
          <ArrowBackIcon style={{ color: "white", fontSize: "20px" }} />
        </IconButton>
      </Link>
    </div>
  );
}

export default NavbarBack;
