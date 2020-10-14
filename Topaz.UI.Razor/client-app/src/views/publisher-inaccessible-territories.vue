<template>
  <div>
    <ul class="list-group">
      <li class="list-group-item list-group-item-action flex-column align-items-start">
        <div class="d-flex w-100 justify-content-end">
          <router-link
            tag="a"
            class="btn btn-primary"
            :to="{
              name: 'PublisherInaccessibleCheckout'
            }"
          >Check Out</router-link>
        </div>
      </li>
      <li
        v-if="territories.length === 0"
        class="list-group-item list-group-item-action flex-column align-items-start"
      >You currently have no inaccessible territories checked out.</li>
      <li
        v-for="t in territoryRows"
        :key="t.territoryActivityId"
        class="list-group-item list-group-item-action flex-column align-items-start"
      >
        <div class="d-flex w-100 justify-content-between">
          <h5 class="mb-1">{{ t.territoryCode }}</h5>
          <small>Checked Out: {{ (t.checkOutDate === null) ? "Never" : t.checkOutDate }}</small>
        </div>
        <div class="d-flex w-100 justify-content-end">
          <div v-if="t === checkin || t === rework">
            <span class="mr-1" v-if="checkin">Check in?</span>
            <span class="mr-1" v-if="rework">Rework?</span>
            <button class="btn btn-success mr-1" @click.prevent="handleConfirm">Confirm</button>
            <button class="btn btn-danger mr-1" @click.prevent="handleCancel">Cancel</button>
          </div>
          <button
            v-if="t !== checkin && t !== rework"
            class="btn btn-primary mr-1"
            @click.prevent="handleCheckin(t)"
          >Check In</button>
          <button
            v-if="t !== checkin && t !== rework"
            class="btn btn-primary mr-1"
            @click.prevent="handleRework(t)"
          >Rework</button>
          <router-link
            tag="a"
            class="btn btn-primary mr-1"
            :to="{ name: 'PublisherInaccessibleImport', params: { id: t.territoryId } }"
          >Import</router-link>
          <router-link
            tag="a"
            class="btn btn-primary"
            :to="{ name: 'PublisherInaccessibleAssign', params: { id: t.territoryId } }"
          >Assign</router-link>
        </div>
      </li>
    </ul>
  </div>
</template>

<script>
import { data } from "../shared";

export default {
  name: "PublisherInaccessibleTerritories",
  data() {
    return {
      territories: [],
      checkin: undefined,
      rework: undefined,
    };
  },
  async created() {
    await this.loadTerritories();
  },
  computed: {
    territoryRows() {
      return this.territories.map((x) => {
        return Object.assign(
          {},
          { ...x },
          {
            territoryCode: [x.streetTerritoryCode, x.territoryCode].join(" / "),
          }
        );
      });
    },
  },
  methods: {
    async loadTerritories() {
      this.territories = [];
      this.territories = await data.getPublisherInaccessibleTerritories();
    },
    async checkinTerritory(t) {
      await data.currentUserCheckin(t);
      await this.loadTerritories(t);
    },
    async reworkTerritory(t) {
      await data.currentUserRework(t);
      await this.loadTerritories(t);
    },
    handleAssign(t) {
      console.log(t);
      //this.checkin = t;
    },
    handleCheckin(t) {
      this.checkin = t;
    },
    handleRework(t) {
      this.rework = t;
    },
    async handleConfirm() {
      if (this.checkin) {
        await this.checkinTerritory(this.checkin);
      } else if (this.rework) {
        await this.reworkTerritory(this.rework);
      }
      this.checkin = undefined;
      this.rework = undefined;
    },
    handleCancel() {
      this.checkin = undefined;
      this.rework = undefined;
    },
  },
};
</script>
