import React from "react";
import PropTypes from "prop-types";

function IndexList(props) {
  return (
    <table className="table">
      <thead>
        <tr>Territory</tr>
        <tr>Date Checked Out</tr>
        <tr>Actions</tr>
      </thead>
      <tbody>
        {props.activity.map((a) => {
          return (
            <tr key="a.Id">
              <td>{a.TerritoryCode}</td>
              <td>{a.CheckOutDate}</td>
              <td>&nbsp;</td>
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
      territoryCode: PropTypes.string.isRequired,
      checkOutDate: PropTypes.object.isRequired,
    })
  ).isRequired,
};

export default IndexList;
