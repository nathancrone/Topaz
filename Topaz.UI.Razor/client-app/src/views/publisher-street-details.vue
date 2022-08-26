<template>
  <div>
    <div class="d-flex">
      <div class="flex-grow-1 mb-2">&nbsp;</div>
      <div class="flex-grow-1 flex-md-grow-0 mb-2">
        <button
          @click="$router.go(-1)"
          type="button"
          class="close"
          aria-label="Close"
        >
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
    </div>
    <h3>Street List</h3>
    <table v-if="addressBlocks.length != 0" class="table table-sm">
      <thead>
        <tr>
          <th scope="col">Street #s</th>
          <th scope="col">Street Name</th>
          <th scope="col">Approx. Doors</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="x in addressBlocks" :key="x.streetTerritoryAddressBlockId">
          <td>{{ x.streetNumbers }}</td>
          <td>{{ x.street }}</td>
          <td>{{ x.estimatedDwellingCount }}</td>
        </tr>
      </tbody>
    </table>
    <p v-else>Not found</p>
    <h3>Locked Addresses</h3>
    <table v-if="locked.length != 0" class="table table-sm">
      <thead>
        <tr>
          <th scope="col">Street #s</th>
          <th scope="col">Street Name</th>
          <th scope="col">Name</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="x in locked" :key="x.streetTerritoryAddressBlockId">
          <td>{{ x.streetNumbers }}</td>
          <td>{{ x.street }}</td>
          <td v-if="x.propertyName">{{ x.propertyName }}</td><td v-else>No Name</td>
        </tr>
      </tbody>
    </table>
    <p v-else>Not found</p>
  </div>
</template>

<script>
import { data } from "../shared";

export default {
  name: "PublisherStreetDetails",
  props: {
    id: {
      type: Number,
      default: 0,
    },
  },
  data() {
    return {
      addressBlocks: [],
      locked: []
    };
  },
  async created() {
    await this.loadAddressBlocks();
    await this.loadLocked();
  },
  methods: {
    async loadAddressBlocks() {
      this.addressBlocks = [];
      this.addressBlocks = await data.getStreetTerritoryAddressBlocks(this.id);
    },
    async loadLocked() {
      this.locked = [];
      this.locked = await data.getStreetTerritoryLocked(this.id);
    },
  },
};
</script>
