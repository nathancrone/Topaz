var DataApi = {
  getCurrentStreetActivity: async function () {
    const response = await fetch("http://example.com/movies.json");
    return await response.json();
  },
};

export default DataApi;
