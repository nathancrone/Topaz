import * as axios from "axios";
import { format } from "date-fns";

const getPublisherStreetTerritories = async function() {
  try {
    const response = await axios.get(`/Street/GetCurrentTerritory`);
    let data = parseList(response);
    const territories = data.map((t) => {
      t.checkOutDate =
        t.checkOutDate === null
          ? null
          : format(parseDate(t.checkOutDate), "MMM dd, yyyy");
      return t;
    });
    return territories;
  } catch (error) {
    console.error(error);
    return [];
  }
};

const getAvailableStreetTerritories = async function() {
  try {
    const response = await axios.get(`/Street/GetAvailableTerritory`);
    let data = parseList(response);
    const territories = data.map((t) => {
      t.checkInDate =
        t.checkInDate === null
          ? null
          : format(parseDate(t.checkInDate), "MMM dd, yyyy");
      return t;
    });
    return territories;
  } catch (error) {
    console.error(error);
    return [];
  }
};

const getPublisherInaccessibleTerritories = async function() {
  try {
    const response = await axios.get(`/Inaccessible/GetCurrentTerritory`);
    let data = parseList(response);
    const territories = data.map((t) => {
      t.checkOutDate =
        t.checkOutDate === null
          ? null
          : format(parseDate(t.checkOutDate), "MMM dd, yyyy");
      return t;
    });
    return territories;
  } catch (error) {
    console.error(error);
    return [];
  }
};

const getAvailableInaccessibleTerritories = async function() {
  try {
    const response = await axios.get(`/Inaccessible/GetAvailableTerritory`);
    let data = parseList(response);
    const territories = data.map((t) => {
      t.checkInDate =
        t.checkInDate === null
          ? null
          : format(parseDate(t.checkInDate), "MMM dd, yyyy");
      return t;
    });
    return territories;
  } catch (error) {
    console.error(error);
    return [];
  }
};

const getAvailableInaccessibleAssignments = async function(territoryId) {
  try {
    const response = await axios.get(
      `/Inaccessible/GetAssignments/${territoryId}/phone`
    );
    let data = parseList(response);
    const available = data.map((a) => {
      return a;
    });
    return available;
  } catch (error) {
    console.error(error);
    return [];
  }
};

const currentUserCheckout = async function(territory) {
  try {
    const response = await axios.post(
      `/Territory/CurrentUserCheckout/${territory.territoryId}`
    );
    if (response.status !== 200) throw Error(response.message);
    if (!response.data) return [];
    return response.data;
  } catch (error) {
    console.error(error);
    return null;
  }
};

const currentUserCheckin = async function(territory) {
  try {
    const response = await axios.post(
      `/Territory/CurrentUserCheckin/${territory.territoryId}`
    );
    if (response.status !== 200) throw Error(response.message);
    if (!response.data) return [];
    return response.data;
  } catch (error) {
    console.error(error);
    return null;
  }
};

const currentUserRework = async function(territory) {
  try {
    const response = await axios.post(
      `/Territory/CurrentUserRework/${territory.territoryId}`
    );
    if (response.status !== 200) throw Error(response.message);
    if (!response.data) return [];
    return response.data;
  } catch (error) {
    console.error(error);
    return null;
  }
};

const parseDate = (d) => {
  var arr = d.split(/\D+/);
  return new Date(
    Date.UTC(arr[0], --arr[1], arr[2], arr[3], arr[4], arr[5], arr[6])
  );
};

const parseList = (response) => {
  if (response.status !== 200) throw Error(response.message);
  if (!response.data) return [];
  let list = response.data;
  if (typeof list !== "object") {
    list = [];
  }
  return list;
};

export const data = {
  getPublisherStreetTerritories,
  getAvailableStreetTerritories,
  getPublisherInaccessibleTerritories,
  getAvailableInaccessibleTerritories,
  getAvailableInaccessibleAssignments,
  currentUserCheckout,
  currentUserCheckin,
  currentUserRework,
};
