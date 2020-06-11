<template>
  <div>
    <ul class="list-group">
      <li
        v-for="t in territoryRows"
        :key="t.territoryId"
        class="list-group-item list-group-item-action flex-column align-items-start"
      >
        <div class="d-flex w-100 justify-content-between">
          <h5 class="mb-1">{{ t.territoryCode }}</h5>
          <small>Last Check In: {{ (t.checkInDate === null) ? "Never" : t.checkInDate }}</small>
        </div>
        <div class="d-flex w-100 justify-content-end">
          <button v-if="t !== checkout" class="btn btn-primary" @click="handleSelect(t)">Select</button>
          <div v-if="t === checkout">
            Check out?
            <button class="btn btn-success mr-1" @click="handleConfirm">Confirm</button>
            <button class="btn btn-danger" @click="handleCancel">Cancel</button>
          </div>
        </div>
      </li>
    </ul>
  </div>
</template>

<script>
import { data } from "../shared";

export default {
  name: "PublisherInaccessibleCheckout",
  data() {
    return {
      territories: [],
      checkout: undefined
    };
  },
  async created() {
    await this.loadTerritories();
  },
  computed: {
    territoryRows() {
      return this.territories.map(x => {
        return Object.assign(
          {},
          { ...x },
          {
            territoryCode: [x.streetTerritoryCode, x.territoryCode].join(" / ")
          }
        );
      });
    }
  },
  methods: {
    async loadTerritories() {
      this.territories = [];
      this.territories = await data.getAvailableInaccessibleTerritories();
      console.log(this.territories);
    },
    async checkoutTerritory(t) {
      await data.currentUserCheckout(t);
      await this.loadTerritories(t);
    },
    handleSelect(t) {
      this.checkout = t;
    },
    handleCancel() {
      this.checkout = undefined;
    },
    async handleConfirm() {
      if (this.checkout) {
        await this.checkoutTerritory(this.checkout);
      }
      this.checkout = undefined;
      this.$router.push({ name: "PublisherInaccessibleTerritories" });
    }
  }
};
</script>
