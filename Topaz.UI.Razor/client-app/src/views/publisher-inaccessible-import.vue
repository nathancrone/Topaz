<template>
  <div>
      <div v-if="true" class="d-flex">
        <div class="flex-grow-1 mb-2">&nbsp;</div>
        <div class="flex-grow-1 flex-md-grow-0 mb-2">
          <a class="btn btn-sm btn-primary" href="#">save</a>
        </div>
      </div>
      <div v-else class="w-100 alert alert-primary" role="alert">no properties found for this territory</div>
    <div :class="{ 'card': true }" 
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
        <form>
            <div class="form-group">
                <label :for="`fileInput${p.inaccessiblePropertyId}`">Import File</label>
                <input type="file" accept=".csv" class="form-control-file" :id="`fileInput${p.inaccessiblePropertyId}`" @change="changeFile($event, p.inaccessiblePropertyId)">
            </div>
            <a v-if="p.file" class="btn btn-primary" href="#" role="button" @click.prevent="uploadFile(p.inaccessiblePropertyId)">Upload</a>
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
        p.isExpanded = false;
        p.file = null;
        p.fileContacts = [];
        p.contacts = [];
      });
      this.properties = [...properties];
    },
    changeFile(evt, id) {
      var property = this.properties.find((p) => p.inaccessiblePropertyId === id);
      property.file = (evt.target.files.length !== 0) ? evt.target.files[0] : null;
    }, 
    async uploadFile(id) {
      var property = this.properties.find((p) => p.inaccessiblePropertyId === id);
      await data.uploadContactsCsv(property.file);
    }, 
    toggle(p) {
      p.isExpanded = !p.isExpanded;
    }
  }
}
</script>