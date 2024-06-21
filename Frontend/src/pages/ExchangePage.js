import React from 'react';
import Exchange from '../components/Exchange';
import NavbarCurrency from '../components/NavbarCurrency';
function ExchangePage() {
  return (
    <div className="App">
      <NavbarCurrency></NavbarCurrency>
      <Exchange />
    </div>
  );
}

export default ExchangePage;
