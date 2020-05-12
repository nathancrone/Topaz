import React from "react";
import { Link } from "react-router-dom";
import PropTypes from "prop-types";

function IndexList(props) {
  return (
    <table className="table">
      <thead>
        <tr>
          <th>Territory</th>
          <th>Date Checked Out</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        {props.activity.map((a) => {
          return (
            <tr key="a.territoryActivityId">
              <td>{a.territoryCode}</td>
              <td>{a.checkOutDate}</td>
              <td>
                <Link to={"/checkin/" + a.territoryId}>Check In</Link>
                {" | "}
                <Link to={"/rework/" + a.territoryId}>Rework</Link>
              </td>
            </tr>
          );
        })}
      </tbody>
    </table>
  );
}

IndexList.propTypes = {
  activity: PropTypes.arrayOf(
    PropTypes.shape({
      territoryActivityId: PropTypes.number.isRequired,
      territoryId: PropTypes.number.isRequired,
      territoryCode: PropTypes.string.isRequired,
      checkOutDate: PropTypes.object.isRequired,
    })
  ).isRequired,
};

export default IndexList;
