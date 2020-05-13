import React from "react";
import { Link } from "react-router-dom";
import PropTypes from "prop-types";
import { shortDate } from "../../lib/Utils";

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
        {props.territory.map((t) => {
          return (
            <tr key={t.territoryActivityId}>
              <td>{t.territoryCode}</td>
              <td>{shortDate(t.checkOutDate)}</td>
              <td>
                <Link to={"/checkin/" + t.territoryId}>Check In</Link>
                {" | "}
                <Link to={"/rework/" + t.territoryId}>Rework</Link>
              </td>
            </tr>
          );
        })}
      </tbody>
    </table>
  );
}

IndexList.propTypes = {
  territory: PropTypes.arrayOf(
    PropTypes.shape({
      territoryActivityId: PropTypes.number.isRequired,
      territoryId: PropTypes.number.isRequired,
      territoryCode: PropTypes.string.isRequired,
      checkOutDate: PropTypes.string.isRequired,
    })
  ).isRequired,
};

export default IndexList;
