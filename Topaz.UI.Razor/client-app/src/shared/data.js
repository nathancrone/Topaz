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

const getAvailableInaccessibleAssignments = async function(territoryId, type) {
  try {
    const response = await axios.get(
      `/Inaccessible/GetAvailableAssignments/${territoryId}/${type}`
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

const getPublisherSelectOptions = async function(token) {
  try {
    const response = await axios.get(
      `/Publisher/GetPublisherSelectOptions/${token}`
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

const assignInaccessibleContacts = async function(assignee, assignments) {
  try {
    const response = await axios.post(
      `/Inaccessible/Assign/${assignee}`,
      assignments
    );
    if (response.status !== 200) throw Error(response.message);
    if (!response.data) return;
    return response.data;
  } catch (error) {
    console.error(error);
    return;
  }
};

const currentUserInaccessibleAssignments = async function() {
  try {
    const response = await axios.get(`/Inaccessible/CurrentUserAssignments`);
    if (!response.data) return null;
    return response.data;
  } catch (error) {
    console.error(error);
    return [];
  }
};

const getPhoneResponseTypes = async function() {
  try {
    const response = await axios.get(`/Inaccessible/GetPhoneResponseTypes`);
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

const saveInaccessibleContactPhoneActivity = async function(
  inaccessibleContactId,
  contactActivityTypeId,
  notes,
  phoneResponseTypeId
) {
  try {
    const response = await axios.post(
      `/Inaccessible/Contact/${inaccessibleContactId}/PhoneActivity`,
      { contactActivityTypeId, notes, phoneResponseTypeId }
    );
    if (response.status !== 200) throw Error(response.message);
    if (!response.data) return;
    return response.data;
  } catch (error) {
    console.error(error);
    return;
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
  getPublisherSelectOptions,
  assignInaccessibleContacts,
  currentUserInaccessibleAssignments,
  getPhoneResponseTypes,
  saveInaccessibleContactPhoneActivity,
  currentUserCheckout,
  currentUserCheckin,
  currentUserRework,
};
