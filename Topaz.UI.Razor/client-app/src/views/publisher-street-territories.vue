<template>
  <div>
    <table class="table">
      <thead>
        <tr>
          <th>Territory</th>
          <th>Date Checked Out</th>
          <th class="text-right">&nbsp;</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="t in territories" :key="t.territoryActivityId">
          <td>{{ t.territoryCode }}</td>
          <td>{{ t.checkOutDate }}</td>

          <td class="text-right">
            <router-link
              tag="a"
              class="btn btn-primary"
              :to="{
                name: 'PublisherStreetCheckin',
                params: { id: t.territoryId },
              }"
            >
              Check In </router-link
            >&nbsp;
            <router-link
              tag="a"
              class="btn btn-primary"
              :to="{
                name: 'PublisherStreetRework',
                params: { id: t.territoryId },
              }"
            >
              Rework</router-link
            >&nbsp;
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import { data } from "../shared";
import { format } from "date-fns";

export default {
  name: "PublisherStreetTerritories",
  data() {
    return {
      territories: [],
    };
  },
  async created() {
    await this.loadTerritories();
  },
  methods: {
    async loadTerritories() {
      this.territories = [];
      this.territories = await data.getPublisherStreeTerritories();
    },
    handleCancelCheckout() {},
    handleConfirmCheckout(territory) {
      const index = this.territories.findIndex(
        (t) => t.territoryId === territory.territoryId
      );
      this.territories.splice(index, 1, territory);
      this.territories = [...this.territories];
    },
  },
  filters: {
    dateFormat: function(value, formatString) {
      return format(value, formatString);
    },
  },
};
</script>
