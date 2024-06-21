import React, { useState } from "react";
import { TextField, Button } from "@mui/material";

function Exchange() {
  const [amount, setAmount] = useState("");
  const [conversionResults, setConversionResults] = useState({
    rates: { EUR: "", USD: "", AUD: "" },
    amounts: { EUR: "", USD: "", AUD: "" },
  });

  const handleInputChange = (event) => {
    setAmount(event.target.value);
  };

  const convertCurrency = async () => {
    const token = localStorage.getItem("token");
    try {
      const response = await fetch(
        `https://timtour.pagekite.me/api/Exchange/getCurrency?CurrentCurrency=1&WantedCurrency=RON,USD,AUD&Amount=${amount}`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      const data = await response.json();
      setConversionResults({
        rates: {
          EUR: parseFloat(data.ratesReturned.EUR).toFixed(4),
          USD: parseFloat(data.ratesReturned.USD).toFixed(4),
          AUD: parseFloat(data.ratesReturned.AUD).toFixed(4),
        },
        amounts: {
          EUR: parseFloat(data.amountsReturned.EUR).toFixed(2),
          USD: parseFloat(data.amountsReturned.USD).toFixed(2),
          AUD: parseFloat(data.amountsReturned.AUD).toFixed(2),
        },
      });
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  return (
    <div className="p-4 flex flex-col items-center">
      <TextField
        label="Amount in RON"
        variant="outlined"
        value={amount}
        onChange={handleInputChange}
        className="mb-20 w-72"
      />
      <div className="grid mt-10 grid-cols-2 gap-8 w-full">
        <TextField
          disabled
          label="EUR Rate"
          value={conversionResults.rates.EUR}
          variant="outlined"
        />
        <TextField
          disabled
          label="EUR Value"
          value={
            amount && conversionResults.rates.EUR
              ? (amount / parseFloat(conversionResults.rates.EUR)).toFixed(2)
              : ""
          }
          variant="outlined"
        />
        <TextField
          disabled
          label="USD Rate"
          value={conversionResults.rates.USD}
          variant="outlined"
        />
        <TextField
          disabled
          label="USD Value"
          value={
            amount && conversionResults.rates.USD
              ? (amount / parseFloat(conversionResults.rates.USD)).toFixed(2)
              : ""
          }
          variant="outlined"
        />
        <TextField
          disabled
          label="AUD Rate"
          value={conversionResults.rates.AUD}
          variant="outlined"
        />
        <TextField
          disabled
          label="AUD Value"
          value={
            amount && conversionResults.rates.AUD
              ? (amount / parseFloat(conversionResults.rates.AUD)).toFixed(2)
              : ""
          }
          variant="outlined"
        />
      </div>
      <Button
        variant="contained"
        style={{
          backgroundColor: "#C798C6", 
          color: "white",
          marginTop: "16px",
        }}
        onClick={convertCurrency}
      >
        Convert
      </Button>
    </div>
  );
}

export default Exchange;
