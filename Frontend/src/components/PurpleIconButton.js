// PurpleIconButton.js
import React from "react";
import IconButton from "@mui/material/IconButton";
import CameraAltIcon from "@mui/icons-material/CameraAlt";


const PurpleIconButton = () => {
  return (
    <IconButton
      color="secondary"
      className="bg-purple-500 hover:bg-purple-700 text-white rounded-full p-2"
      aria-label="camera"
    >
      <CameraAltIcon />
    </IconButton>
  );
};

export default PurpleIconButton;
