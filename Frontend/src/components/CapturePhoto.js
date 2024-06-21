import React, { useState, useRef } from "react";
import Webcam from "react-webcam";
import axios from "axios";
import NavbarBack from "./NavbarBack";

const CapturePhoto = () => {
  const webcamRef = useRef(null);
  const [image, setImage] = useState("");
  const [showCamera, setShowCamera] = useState(true);
  const [responseImage, setResponseImage] = useState("");
  const backendURL = "https://translate-timtour.pagekite.me//extract-text";

  const videoConstraints = {
    width: 1920,
    height: 1080,
    facingMode: "environment",
  };

  const capturePhoto = () => {
    const imageSrc = webcamRef.current.getScreenshot();
    setImage(imageSrc);
    setShowCamera(false);
  };

  const redoPhoto = () => {
    setImage("");
    setShowCamera(true);
  };

  const sendPhoto = async () => {
    if (!image) {
      alert('Please capture an image first.');
      return;
    }

    const formData = new FormData();
    const blob = await fetch(image).then(res => res.blob());
    formData.append('image', blob, 'captured-image.jpg');

    try {
      const response = await axios.post(backendURL, formData, {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
      });
      const base64Image = `data:image/jpeg;base64,${response.data.image}`;
      setResponseImage(base64Image);
    } catch (error) {
      console.error("Error sending photo:", error);
      alert("Failed to upload image.");
    }
  };

  return (
    <>
      <NavbarBack />
      <div
        style={{
          position: "relative",
          width: "100vw",
          height: "100vh",
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          justifyContent: "center",
        }}
      >
        {showCamera ? (
          <>
            <Webcam
              audio={false}
              ref={webcamRef}
              screenshotFormat="image/jpeg"
              videoConstraints={videoConstraints}
              style={{
                width: "100%",
                height: "100%",
                objectFit: "cover",
                position: "absolute",
              }}
            />
            <button
              onClick={capturePhoto}
              style={{ position: "absolute", bottom: "20px", zIndex: 2 }}
            >
              Capture
            </button>
          </>
        ) : (
          <>
            <img
              src={image}
              alt="Captured"
              style={{
                width: "100%",
                height: "100%",
                objectFit: "cover",
                position: "absolute",
              }}
            />
            <div
              style={{
                position: "absolute",
                bottom: "60px",
                zIndex: 2,
                display: "flex",
                justifyContent: "center",
                width: "100%",
              }}
            >
              <button onClick={redoPhoto} style={{ marginRight: "20px" }}>
                REDO
              </button>
              <button onClick={sendPhoto}>SEND</button>
            </div>
          </>
        )}
        {responseImage && (
          <img
            src={responseImage}
            alt="Response"
            style={{ marginTop: "20px", zIndex: 3 }}
          />
        )}
      </div>
    </>
  );
};

export default CapturePhoto;
