import React from "react";
import { Link } from "react-router-dom";

function CheckInPage(props) {
  return (
    <>
      {props.match.params.id}
      <Link to="">Cancel</Link>
    </>
  );
}

export default CheckInPage;
