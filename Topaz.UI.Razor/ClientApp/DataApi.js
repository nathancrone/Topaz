var DataApi = {
  getCurrentStreetActivity: async function () {
    const response = await fetch("street");
    return await response.json();
  },
};

export default DataApi;
