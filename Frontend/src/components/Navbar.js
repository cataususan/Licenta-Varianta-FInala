import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

const Navbar = () => {
  const [userName, setUserName] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    const fetchUserName = async () => {
      try {
        const token = localStorage.getItem("token");
        if (!token) {
          throw new Error("No token found in local storage");
        }

        const response = await fetch(
          "https://timtour.pagekite.me/api/User/name",
          {
            headers: {
              Authorization: `Bearer ${token}`,
              "Content-Type": "application/json",
            },
          }
        );

        if (!response.ok) {
          throw new Error("Failed to fetch user name");
        }

        const data = await response.json();
        setUserName(data.user_name);
      } catch (error) {
        console.error("Error fetching user name:", error);
      }
    };

    fetchUserName();
  }, []);

  const handleProfileClick = () => {
    navigate("/profile");
  };

  return (
    <div className="bg-[#C798C6] text-white font-bold p-4 text-lg">
      <div className="container mx-auto flex justify-between items-center">
        <h1>TimTour</h1>
        {userName && (
          <div className="cursor-pointer" onClick={handleProfileClick}>
            Welcome, {userName}
          </div>
        )}
      </div>
    </div>
  );
};

export default Navbar;
