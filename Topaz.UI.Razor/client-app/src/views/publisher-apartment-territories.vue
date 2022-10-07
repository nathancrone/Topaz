<template>
  <div>
    <ul class="list-group">
      <li
        class="list-group-item list-group-item-action flex-column align-items-start"
      >
        <div class="d-flex w-100 justify-content-end">
          <router-link
            tag="a"
            class="btn btn-primary"
            :to="{
              name: 'PublisherApartmentCheckout',
            }"
            >Check Out</router-link
          >
        </div>
      </li>
      <li
        v-if="territories.length === 0"
        class="list-group-item list-group-item-action flex-column align-items-start"
      >
        You currently have no apartment territories checked out.
      </li>
      <li
        v-for="t in territories"
        :key="t.territoryActivityId"
        class="list-group-item list-group-item-action flex-column align-items-start"
      >
        <div class="d-flex w-100 justify-content-between">
          <h5 class="mb-1">{{ t.territoryCode }}</h5>
          <small
            >Checked Out:
            {{ t.checkOutDate === null ? "Never" : t.checkOutDate }}</small
          >
        </div>
        <div class="d-flex w-100 justify-content-end">
          <a
            v-if="t !== checkin && t !== rework && t.mapLocation && t.mapLocation !== ''"
            class="btn btn-secondary mr-1"
            :href="t.mapLocation"
            target="_blank"
          >View</a>
          <button
            v-if="t !== checkin && t !== rework"
            class="btn btn-primary mr-1"
            @click.prevent="handleCheckin(t)"
          >
            Check In
          </button>
          <button
            v-if="t !== checkin && t !== rework"
            class="btn btn-primary"
            @click.prevent="handleRework(t)"
          >
            Rework
          </button>
          <div v-if="t === checkin || t === rework">
            <span class="mr-1" v-if="checkin">Check in?</span>
            <span class="mr-1" v-if="rework">Rework?</span>
            <button class="btn btn-success mr-1" @click.prevent="handleConfirm">
              Confirm
            </button>
            <button class="btn btn-danger" @click.prevent="handleCancel">
              Cancel
            </button>
          </div>
        </div>
      </li>
    </ul>
  </div>
</template>

<script>
import { data } from "../shared";

export default {
  name: "PublisherApartmentTerritories",
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
  methods: {
    async loadTerritories() {
      this.territories = [];
      this.territories = await data.getPublisherApartmentTerritories();
    },
    async checkinTerritory(t) {
      await data.userCheckin(t.territoryId);
      await this.loadTerritories();
    },
    async reworkTerritory(t) {
      await data.currentUserRework(t);
      await this.loadTerritories();
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
