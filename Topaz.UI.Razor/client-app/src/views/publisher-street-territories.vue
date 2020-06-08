<template>
  <div>
    <ul class="list-group">
      <li class="list-group-item list-group-item-action flex-column align-items-start">
        <div class="d-flex w-100 justify-content-end">
          <router-link
            tag="a"
            class="btn btn-primary"
            :to="{
            name: 'PublisherStreetCheckout'
            }"
          >Check Out</router-link>
        </div>
      </li>
      <li
        v-for="t in territories"
        :key="t.territoryActivityId"
        class="list-group-item list-group-item-action flex-column align-items-start"
      >
        <div class="d-flex w-100 justify-content-between">
          <h5 class="mb-1">{{ t.territoryCode }}</h5>
          <small>Checked Out: {{ t.checkOutDate }}</small>
        </div>
        <div class="d-flex w-100 justify-content-end">
          <button
            v-if="t !== checkinTerritory && t !== reworkTerritory"
            class="btn btn-primary mr-1"
            @click="handleCheckin(t)"
          >Check In</button>
          <button
            v-if="t !== checkinTerritory && t !== reworkTerritory"
            class="btn btn-primary"
            @click="handleRework(t)"
          >Rework</button>
          <div v-if="t === checkinTerritory || t === reworkTerritory">
            <span class="mr-1" v-if="checkinTerritory">Check in?</span>
            <span class="mr-1" v-if="reworkTerritory">Rework?</span>
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
  name: "PublisherStreetTerritories",
  data() {
    return {
      territories: [],
      checkinTerritory: undefined,
      reworkTerritory: undefined
    };
  },
  async created() {
    await this.loadTerritories();
  },
  methods: {
    async loadTerritories() {
      this.territories = [];
      this.territories = await data.getPublisherStreetTerritories();
    },
    handleCheckin(t) {
      this.checkinTerritory = t;
    },
    handleRework(t) {
      this.reworkTerritory = t;
    },
    handleCancel() {
      this.checkinTerritory = undefined;
      this.reworkTerritory = undefined;
    },
    handleConfirm() {}
  }
};
</script>
