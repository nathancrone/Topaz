import * as axios from "axios";
import { parseISO, format } from "date-fns";

const getStreetTerritory = async function() {
  try {
    const response = await axios.get(`/Street/GetTerritory`);
    let data = parseList(response);
    const result = data.map((a) => {
      return a;
    });
    return result;
  } catch (error) {
    console.error(error);
    return [];
  }
};

const getStreetTerritoryOut = async function() {
  try {
    const response = await axios.get(`/Street/GetTerritoryOut`);
    let data = parseList(response);
    const result = data.map((a) => {
      return a;
    });
    return result;
  } catch (error) {
    console.error(error);
    return [];
  }
};

const getStreetTerritoryIn = async function() {
  try {
    const response = await axios.get(`/Street/GetTerritoryIn`);
    let data = parseList(response);
    const result = data.map((a) => {
      return a;
    });
    return result;
  } catch (error) {
    console.error(error);
    return [];
  }
};

const getStreetActivity = async function(id) {
  try {
    const response = await axios.get(`/Territory/GetStreetActivity/${id}`);
    if (response.status !== 200) throw Error(response.message);
    if (!response.data) return;
    return response.data;
  } catch (error) {
    console.error(error);
    return [];
  }
};

const getBusinessTerritory = async function() {
  try {
    const response = await axios.get(`/Business/GetTerritory`);
    let data = parseList(response);
    const result = data.map((a) => {
      return a;
    });
    return result;
  } catch (error) {
    console.error(error);
    return [];
  }
};

const getBusinessTerritoryOut = async function() {
  try {
    const response = await axios.get(`/Business/GetTerritoryOut`);
    let data = parseList(response);
    const result = data.map((a) => {
      return a;
    });
    return result;
  } catch (error) {
    console.error(error);
    return [];
  }
};

const getBusinessTerritoryIn = async function() {
  try {
    const response = await axios.get(`/Business/GetTerritoryIn`);
    let data = parseList(response);
    const result = data.map((a) => {
      return a;
    });
    return result;
  } catch (error) {
    console.error(error);
    return [];
  }
};

const getBusinessActivity = async function(id) {
  try {
    const response = await axios.get(`/Territory/GetBusinessActivity/${id}`);
    if (response.status !== 200) throw Error(response.message);
    if (!response.data) return;
    return response.data;
  } catch (error) {
    console.error(error);
    return [];
  }
};

const getInaccessibleTerritory = async function() {
  try {
    const response = await axios.get(`/Inaccessible/GetTerritory`);
    let data = parseList(response);
    const result = data.map((a) => {
      return a;
    });
    return result;
  } catch (error) {
    console.error(error);
    return [];
  }
};

const getInaccessibleTerritoryOut = async function() {
  try {
    const response = await axios.get(`/Inaccessible/GetTerritoryOut`);
    let data = parseList(response);
    const result = data.map((a) => {
      return a;
    });
    return result;
  } catch (error) {
    console.error(error);
    return [];
  }
};

const getInaccessibleTerritoryIn = async function() {
  try {
    const response = await axios.get(`/Inaccessible/GetTerritoryIn`);
    let data = parseList(response);
    const result = data.map((a) => {
      return a;
    });
    return result;
  } catch (error) {
    console.error(error);
    return [];
  }
};

const getInaccessibleActivity = async function(id) {
  try {
    const response = await axios.get(
      `/Territory/GetInaccessibleActivity/${id}`
    );
    if (response.status !== 200) throw Error(response.message);
    if (!response.data) return;
    return response.data;
  } catch (error) {
    console.error(error);
    return [];
  }
};

const getPublisherStreetTerritories = async function() {
  try {
    const response = await axios.get(`/Street/GetCurrentTerritory`);
    let data = parseList(response);
    const territories = data.map((t) => {
      t.checkOutDate =
        t.checkOutDate === null
          ? null
          : format(parseISO(t.checkOutDate), "MMM dd, yyyy");
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
          : format(parseISO(t.checkInDate), "MMM dd, yyyy");
      return t;
    });
    return territories;
  } catch (error) {
    console.error(error);
    return [];
  }
};

const getPublisherBusinessTerritories = async function() {
  try {
    const response = await axios.get(`/Business/GetCurrentTerritory`);
    let data = parseList(response);
    const territories = data.map((t) => {
      t.checkOutDate =
        t.checkOutDate === null
          ? null
          : format(parseISO(t.checkOutDate), "MMM dd, yyyy");
      return t;
    });
    return territories;
  } catch (error) {
    console.error(error);
    return [];
  }
};

const getAvailableBusinessTerritories = async function() {
  try {
    const response = await axios.get(`/Business/GetAvailableTerritory`);
    let data = parseList(response);
    const territories = data.map((t) => {
      t.checkInDate =
        t.checkInDate === null
          ? null
          : format(parseISO(t.checkInDate), "MMM dd, yyyy");
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
          : format(parseISO(t.checkOutDate), "MMM dd, yyyy");
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
          : format(parseISO(t.checkInDate), "MMM dd, yyyy");
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

const getTerritoryExports = async function(territoryId) {
  try {
    const response = await axios.get(
      `/Inaccessible/GetTerritoryExports/${territoryId}`
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

const getContactActivity = async function(id) {
  try {
    const response = await axios.get(`/Inaccessible/GetContactActivity/${id}`);
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

const getTerritoryProperties = async function(id) {
  try {
    const response = await axios.get(
      `/Inaccessible/GetTerritoryProperties/${id}`
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

const unassignInaccessibleContacts = async function(assignments) {
  try {
    const response = await axios.post(`/Inaccessible/Unassign`, assignments);
    if (response.status !== 200) throw Error(response.message);
    if (!response.data) return;
    return response.data;
  } catch (error) {
    console.error(error);
    return;
  }
};

const flagAvailabilityInaccessibleContacts = async function(assignments, isAvailable) {
  try {
    const response = await axios.post(`/Inaccessible/FlagAvailability/${isAvailable}`, assignments);
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

const saveInaccessibleContactPhoneActivities = async function(
  responseTypeId,
  activityTypeId,
  assignments
) {
  try {
    const response = await axios.post(
      `/Inaccessible/ResponseType/${responseTypeId}/${activityTypeId}/PhoneActivities`,
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

const saveInaccessibleContactLetterActivity = async function(
  inaccessibleContactId,
  notes
) {
  try {
    const response = await axios.post(
      `/Inaccessible/Contact/${inaccessibleContactId}/LetterActivity`,
      { notes }
    );
    if (response.status !== 200) throw Error(response.message);
    if (!response.data) return;
    return response.data;
  } catch (error) {
    console.error(error);
    return;
  }
};

const saveInaccessibleContactLetterActivities = async function(assignments) {
  try {
    const response = await axios.post(
      `/Inaccessible/LetterActivities`,
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

const saveInaccessibleContactExportActivities = async function(
  assignee,
  assignments
) {
  try {
    const response = await axios.post(
      `/Inaccessible/ExportActivities/${assignee}`,
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

const removePropertyContactList = async function(id) {
  try {
    const response = await axios.post(
      `/Inaccessible/RemovePropertyContactList/${id}`
    );
    if (response.status !== 200) throw Error(response.message);
    if (!response.data) return;
    return response.data;
  } catch (error) {
    console.error(error);
    return;
  }
};

const uploadContactsCsv = async function(file) {
  try {
    var formData = new FormData();
    formData.append("csvFile", file);
    const response = await axios.post(
      `/Inaccessible/UploadContactsCsv`,
      formData,
      {
        headers: {
          Accept: "application/vnd.ms-excel",
        },
      }
    );
    if (response.status !== 200) throw Error(response.message);
    if (!response.data) return;
    return response.data;
  } catch (error) {
    console.error(error);
    return;
  }
};

const uploadContacts = async function(id, contacts) {
  try {
    const response = await axios.post(
      `/Inaccessible/UploadContacts/${id}`,
      contacts
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

const publisherCheckout = async function(
  territoryId,
  publisherId,
  checkoutDate
) {
  try {
    const response = await axios.post(
      `/Territory/PublisherCheckout/${territoryId}`,
      { publisherId, checkoutDate }
    );
    if (response.status !== 200) throw Error(response.message);
    if (!response.data) return [];
    return response.data;
  } catch (error) {
    console.error(error);
    return null;
  }
};

const userCheckin = async function(territoryId, checkinDate) {
  try {
    const response = await axios.post(`/Territory/UserCheckin/${territoryId}`, {
      checkinDate,
    });
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

const currentUserAssignAvailable = async function(type) {
  try {
    const response = await axios.post(
      `/Inaccessible/CurrentUserAssignAvailable/${type}`);
    if (response.status !== 200) throw Error(response.message);
    if (response.data === undefined) return;
    return response.data;
  } catch (error) {
    console.error(error);
    return;
  }
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
  getStreetTerritory,
  getStreetTerritoryOut,
  getStreetTerritoryIn,
  getStreetActivity,
  getBusinessTerritory,
  getBusinessTerritoryOut,
  getBusinessTerritoryIn,
  getBusinessActivity,
  getInaccessibleTerritory,
  getInaccessibleTerritoryOut,
  getInaccessibleTerritoryIn,
  getInaccessibleActivity,
  getPublisherStreetTerritories,
  getAvailableStreetTerritories,
  getPublisherBusinessTerritories,
  getAvailableBusinessTerritories,
  getPublisherInaccessibleTerritories,
  getAvailableInaccessibleTerritories,
  getAvailableInaccessibleAssignments,
  getTerritoryExports,
  getContactActivity,
  getPublisherSelectOptions,
  getTerritoryProperties,
  assignInaccessibleContacts,
  unassignInaccessibleContacts,
  flagAvailabilityInaccessibleContacts, 
  currentUserInaccessibleAssignments,
  getPhoneResponseTypes,
  saveInaccessibleContactPhoneActivity,
  saveInaccessibleContactPhoneActivities,
  saveInaccessibleContactLetterActivity,
  saveInaccessibleContactLetterActivities,
  saveInaccessibleContactExportActivities,
  removePropertyContactList,
  uploadContactsCsv,
  uploadContacts,
  currentUserCheckout,
  publisherCheckout,
  userCheckin,
  currentUserRework,
  currentUserAssignAvailable, 
};
