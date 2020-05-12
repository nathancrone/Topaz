import React, { useState, useEffect } from "react";
import IndexList from "./IndexList";
import DataApi from "../../DataApi";

function IndexPage() {
  const [activity, setActivity] = useState([]);

  useEffect(() => {
    DataApi.getCurrentStreetActivity().then((_activity) =>
      setActivity(_activity)
    );
  }, []);

  return (
    <>
      <h2>Current Street Territory</h2>
      <IndexList activity={activity} />
    </>
  );
}

export default IndexPage;
