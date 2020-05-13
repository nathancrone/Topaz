import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import CheckOutList from "./CheckOutList";
import DataApi from "../../lib/DataApi";

function CheckOutPage() {
  const [territory, setTerritory] = useState([]);

  useEffect(() => {
    DataApi.getAvailableStreetTerritory().then((_territory) =>
      setTerritory(_territory)
    );
  }, []);

  return (
    <>
      <h2>Available Street Territory</h2>
      <CheckOutList territory={territory} />
      <Link to="">Cancel</Link>
    </>
  );
}

export default CheckOutPage;
