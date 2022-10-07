<template>
  <div>
    <div class="row mt-3">
      <ul class="nav nav-tabs flex-column flex-md-row">
        <li class="nav-item">
          <router-link
            tag="a"
            :class="{ 'nav-link': true, active: type === 'street' }"
            :to="{ name: 'AdminTerritory', params: { type: 'street' } }"
            >Street</router-link
          >
        </li>
        <li class="nav-item">
          <router-link
            tag="a"
            :class="{ 'nav-link': true, active: type === 'inaccessible' }"
            :to="{ name: 'AdminTerritory', params: { type: 'inaccessible' } }"
            >Inaccessible</router-link
          >
        </li>
        <li class="nav-item">
          <router-link
            tag="a"
            :class="{ 'nav-link': true, active: type === 'business' }"
            :to="{ name: 'AdminTerritory', params: { type: 'business' } }"
            >Business</router-link
          >
        </li>
        <li class="nav-item">
          <router-link
            tag="a"
            :class="{ 'nav-link': true, active: type === 'apartment' }"
            :to="{ name: 'AdminTerritory', params: { type: 'apartment' } }"
            >Apartment</router-link
          >
        </li>
      </ul>
    </div>
    <div class="row mt-3 no-gutters">
      <div class="d-flex flex-wrap col">
        <div class="flex-grow-1 mb-2">
          &nbsp;
        </div>
        <div class="flex-grow-1 flex-md-grow-0 mb-2">
          <div class="btn-group btn-group-sm" role="group" aria-label="filter">
            <button type="button" class="btn btn-secondary active">
              All
            </button>
            <button type="button" class="btn btn-secondary">
              Checked Out
            </button>
            <button type="button" class="btn btn-secondary">
              Checked In
            </button>
          </div>
        </div>
      </div>
    </div>
    <div>
      <div class="row no-gutters">
        <div class="col">
          <h3>Territories</h3>
          <table class="table table-sm">
            <thead>
              <tr>
                <th scope="col">Territory</th>
                <th scope="col">Publisher</th>
                <th scope="col">Checked Out</th>
                <th scope="col">Checked In</th>
                <th scope="col">&nbsp;</th>
              </tr>
            </thead>
            <tbody>
              <tr
                :class="{ 'table-secondary': t.inActive }"
                v-for="t in territories"
                :key="t.territoryId"
              >
                <th v-if="t.streetTerritoryCode" scope="row">
                  {{ t.streetTerritoryCode }} / {{ t.territoryCode }}
                </th>
                <th v-else scope="row">{{ t.territoryCode }}</th>
                <template v-if="!t.activity">
                  <td colspan="3" class="text-center">-</td>
                </template>
                <template v-else>
                  <td>
                    {{ t.publisher.firstName }} {{ t.publisher.lastName }}
                  </td>
                  <td>{{ displayDate(t.activity.checkOutDate) }}</td>
                  <td v-if="t.activity.checkInDate">
                    {{ displayDate(t.activity.checkInDate) }}
                  </td>
                  <td v-else>-</td>
                </template>
                <td class="text-right">
                  <template v-if="!t.inActive">
                    <a
                      v-if="t.activity && !t.activity.checkInDate"
                      class="btn btn-sm btn-primary mr-1"
                      href="#"
                      @click.prevent="checkIn(t)"
                      >check in</a
                    >
                    <a
                      v-else
                      class="btn btn-sm btn-primary mr-1"
                      href="#"
                      @click.prevent="checkOut(t)"
                      >check out</a
                    >
                  </template>
                  <router-link
                    tag="a"
                    class="btn btn-sm btn-primary"
                    :to="{
                      name: 'AdminTerritoryActivity',
                      params: { id: t.territoryId },
                    }"
                    >activity</router-link
                  >
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
    <AdminTerritoryCheckinModal
      @close="isCheckinModalOpen = false"
      @confirm="checkinConfirm"
      :territory="selectedTerritory"
      :open="isCheckinModalOpen"
    />
    <AdminTerritoryCheckoutModal
      @close="isCheckoutModalOpen = false"
      @confirm="checkoutConfirm"
      :territory="selectedTerritory"
      :open="isCheckoutModalOpen"
    />
  </div>
</template>

<script>
import { data } from "../shared";
import { parseISO, format } from "date-fns";
import AdminTerritoryCheckinModal from "../components/admin-territory-checkin-modal";
import AdminTerritoryCheckoutModal from "../components/admin-territory-checkout-modal";

export default {
  name: "AdminTerritory",
  props: {
    type: {
      type: String,
      required: true,
    },
  },
  data() {
    return {
      territories: [],
      selectedTerritory: null,
      isCheckinModalOpen: false,
      isCheckoutModalOpen: false,
    };
  },
  components: {
    AdminTerritoryCheckinModal,
    AdminTerritoryCheckoutModal,
  },
  methods: {
    async loadTerritories() {
      this.territories = [];
      if (this.type === "street") {
        this.territories = await data.getStreetTerritory();
      } else if (this.type === "inaccessible") {
        this.territories = await data.getInaccessibleTerritory();
      } else if (this.type === "business") {
        this.territories = await data.getBusinessTerritory();
      } else if (this.type === "apartment") {
        this.territories = await data.getApartmentTerritory();
      }
    },
    displayDate(date) {
      return format(parseISO(date), "MMM dd, yyyy");
    },
    checkIn(t) {
      this.selectedTerritory = t;
      this.isCheckinModalOpen = true;
    },
    checkOut(t) {
      this.selectedTerritory = t;
      this.isCheckoutModalOpen = true;
    },
    async checkinConfirm(args) {
      await data.userCheckin(args.territoryId, args.checkinDate);
      await this.loadTerritories();
      this.isCheckinModalOpen = false;
    },
    async checkoutConfirm(args) {
      await data.publisherCheckout(
        args.territoryId,
        args.publisherId,
        args.checkoutDate
      );
      await this.loadTerritories();
      this.isCheckoutModalOpen = false;
    },
  },
  watch: {
    type: {
      immediate: true,
      async handler() {
        await this.loadTerritories();
      },
    },
    isCheckinModalOpen: {
      handler(after) {
        if (after) this.isCheckoutModalOpen = !after;
      },
    },
    isCheckoutModalOpen: {
      handler(after) {
        if (after) this.isCheckinModalOpen = !after;
      },
    },
  },
};
</script>
