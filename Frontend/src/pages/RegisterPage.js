import React, { useState } from "react";
import { TextField, Button, Snackbar } from "@mui/material";
import MuiAlert from "@mui/material/Alert";
import { useNavigate } from "react-router-dom";

const Alert = React.forwardRef(function Alert(props, ref) {
  return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
});

const RegisterPage = () => {
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [name, setName] = useState("");
  const [open, setOpen] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");

  const handleClose = (event, reason) => {
    if (reason === "clickaway") {
      return;
    }
    setOpen(false);
  };

  const handleRegister = async () => {
    try {
      const userType = "Admin";
      const response = await fetch("https://timtour.pagekite.me/api/Register", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ email, password, name, type: userType }),
      });

      const data = await response.json();
      if (response.ok) {
        console.log("Registration successful:", data);
        navigate("/");
      } else {
        throw new Error(data.message || "Registration failed");
      }
    } catch (error) {
      console.error("Registration error:", error);
      setErrorMessage(error.message);
      setOpen(true);
    }
  };

  return (
    <div className="h-screen flex items-center justify-center bg-gray-100">
      <div className="flex flex-col h-full p-6 max-w-sm w-full bg-white shadow-md rounded justify-center">
        <form className="space-y-6" onSubmit={(e) => e.preventDefault()}>
          <div>
            <label className="block mb-2 text-sm font-medium text-gray-700">
              Email
            </label>
            <TextField
              required
              fullWidth
              variant="outlined"
              type="email"
              placeholder="Enter your email"
              size="small"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
          </div>
          <div>
            <label className="block mb-2 text-sm font-medium text-gray-700">
              Password
            </label>
            <TextField
              required
              fullWidth
              variant="outlined"
              type="password"
              placeholder="Enter your password"
              size="small"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </div>
          <div>
            <label className="block mb-2 text-sm font-medium text-gray-700">
              Name
            </label>
            <TextField
              required
              fullWidth
              variant="outlined"
              type="text"
              placeholder="Enter your name"
              size="small"
              value={name}
              onChange={(e) => setName(e.target.value)}
            />
          </div>
          <Button
            onClick={handleRegister}
            fullWidth
            variant="contained"
            style={{ backgroundColor: "purple", color: "white" }}
          >
            Register
          </Button>
        </form>
      </div>
      <Snackbar open={open} autoHideDuration={6000} onClose={handleClose}>
        <Alert onClose={handleClose} severity="error" sx={{ width: "100%" }}>
          {errorMessage}
        </Alert>
      </Snackbar>
    </div>
  );
};

export default RegisterPage;
