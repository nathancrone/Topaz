import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import IndexList from "./IndexList";
import DataApi from "../../lib/DataApi";

function IndexPage() {
  const [territory, setTerritory] = useState([]);

  useEffect(() => {
    DataApi.getCurrentStreetTerritory().then((_territory) =>
      setTerritory(_territory)
    );
  }, []);

  return (
    <>
      <h2>Current Street Territory</h2>
      <IndexList territory={territory} />
      <Link to="/checkout">Check Out</Link>
    </>
  );
}

export default IndexPage;
