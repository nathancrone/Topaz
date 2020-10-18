<template>
  <div>
    <div :class="{ 'card': true, 'border-danger': !p.isImported }" 
      v-for="p in properties"
      :key="`card${p.inaccessiblePropertyId}`">
        <ul class="bg-light list-group list-group-flush">
            <a href="#" class="d-flex list-group-item list-group-item-action" v-on:click.prevent="toggle(p)">
            <div class="w-100 justify-content-between">
                <strong>{{ p.street }} ({{ p.streetNumbers }}) - {{ p.propertyName }}</strong>
            </div>
            <div class="justify-content-end">
                <i :class="{'arrow': true, 'down': !p.isExpanded, 'up': p.isExpanded }"></i>
            </div>
            </a>
        </ul>
        <div :class="{'card-body': true, 'collapse': true, 'show': p.isExpanded}">
        <form v-if="!p.isImported">
            <div class="form-group">
                <label :for="`fileInput${p.inaccessiblePropertyId}`">Import File</label>
                <input type="file" class="form-control-file" :id="`fileInput${p.inaccessiblePropertyId}`">
            </div>
        </form>
        </div>
    </div>
  </div>
</template>
<script>
import { data } from "../shared";

export default {
  name: "PublisherInaccessibleImport",
  props: {
    id: {
      type: Number,
      default: 0,
    },
  },
  data() {
    return {
        properties: []
    }
  }, 
  async created() {
    await this.loadProperties();
  },
  methods: {
    async loadProperties() {
      const properties = await data.getTerritoryProperties(
        this.id
      );
      properties.forEach(function (p) {
        p.isImported = false;
        p.isExpanded = false;
        p.contacts = [];
      });
      this.properties = [...properties];
    },
    toggle(p) {
      p.isExpanded = !p.isExpanded;
    }
  }
}
</script>