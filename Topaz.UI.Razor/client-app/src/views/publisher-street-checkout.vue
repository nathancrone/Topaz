<template>
  <div>
    <ul class="list-group">
      <li
        v-for="t in territories"
        :key="t.territoryId"
        class="list-group-item list-group-item-action flex-column align-items-start"
      >
        <div class="d-flex w-100 justify-content-between">
          <h5 class="mb-1">{{ t.territoryCode }}</h5>
          <small>Last Check In: {{ (t.checkInDate === null) ? "Never" : t.checkInDate }}</small>
        </div>
        <div class="d-flex w-100 justify-content-end">
          <a
            v-if="t !== checkout && t.mapLocation && t.mapLocation !== ''"
            class="btn btn-secondary mr-1"
            :href="t.mapLocation"
            target="_blank"
          >View</a>
          <button
            v-if="t !== checkout"
            class="btn btn-primary"
            @click.prevent="handleSelect(t)"
          >Select</button>
          <div v-if="t === checkout">
            Check out?
            <button class="btn btn-success mr-1" @click.prevent="handleConfirm">Confirm</button>
            <button class="btn btn-danger" @click.prevent="handleCancel">Cancel</button>
          </div>
        </div>
      </li>
    </ul>
  </div>
</template>

<script>
import { data } from "../shared";

export default {
  name: "PublisherStreetCheckout",
  data() {
    return {
      territories: [],
      checkout: undefined,
    };
  },
  async created() {
    await this.loadTerritories();
  },
  methods: {
    async loadTerritories() {
      this.territories = [];
      this.territories = await data.getAvailableStreetTerritories();
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
      this.$router.push({ name: "PublisherStreetTerritories" });
    },
  },
};
</script>
