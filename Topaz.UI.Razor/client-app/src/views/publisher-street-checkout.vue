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
          <small>Last Check In: {{ t.checkInDate }}</small>
        </div>
        <div class="d-flex w-100 justify-content-end">
          <button
            v-if="t !== selectedTerritory"
            class="btn btn-primary"
            @click="handleSelect(t)"
          >Select</button>
          <div v-if="t === selectedTerritory">
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
  name: "PublisherStreetCheckout",
  data() {
    return {
      territories: [],
      selectedTerritory: undefined
    };
  },
  async created() {
    await this.loadTerritories();
  },
  methods: {
    async loadTerritories() {
      this.territories = [];
      this.territories = await data.getAvailableStreetTerritories();
    },
    handleSelect(t) {
      this.selectedTerritory = t;
    },
    handleCancel() {
      this.selectedTerritory = undefined;
    },
    handleConfirm() {}
  }
};
</script>
