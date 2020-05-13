var DataApi = {
  getCurrentStreetTerritory: async function () {
    const response = await fetch("/Street/GetCurrentTerritory");
    return await response.json();
  },
  getAvailableStreetTerritory: async function () {
    const response = await fetch("/Street/GetAvailableTerritory");
    return await response.json();
  },
};

export default DataApi;
