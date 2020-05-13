import React from "react";
import { Link } from "react-router-dom";
import PropTypes from "prop-types";
import { shortDate } from "../../lib/Utils";

function CheckOutList(props) {
  return (
    <table className="table">
      <thead>
        <tr>
          <th>Territory</th>
          <th>Date Checked In</th>
          <th>&nbsp;</th>
        </tr>
      </thead>
      <tbody>
        {props.territory.map((t) => {
          return (
            <tr key={t.territoryId}>
              <td>{t.territoryCode}</td>
              <td>{shortDate(t.checkInDate)}</td>
              <td>Select</td>
            </tr>
          );
        })}
      </tbody>
    </table>
  );
}

CheckOutList.propTypes = {
  territory: PropTypes.arrayOf(
    PropTypes.shape({
      territoryId: PropTypes.number.isRequired,
      territoryCode: PropTypes.string.isRequired,
      checkInDate: PropTypes.string,
    })
  ).isRequired,
};

export default CheckOutList;
