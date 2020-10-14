<template>
  <div>
    <div class="card" 
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
                <label for="exampleFormControlFile1">Example file input</label>
                <input type="file" class="form-control-file" id="exampleFormControlFile1">
            </div>
        </form>
        </div>
    </div>
  </div>
</template>
<script>
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
        properties: [
            { inaccessiblePropertyId: 1, streetNumbers: '7171', street: 'Gaston Ave.', estimatedDwellingCount: 20, propertyName: 'Winsted at White Rock', description: '', isImported: false, isExpanded: false, contacts: [] }, 
            { inaccessiblePropertyId: 2, streetNumbers: '7173', street: 'Gaston Ave.', estimatedDwellingCount: 20, propertyName: 'Winsted at White Rock', description: '', isImported: false, isExpanded: false, contacts: [] }, 
            { inaccessiblePropertyId: 3, streetNumbers: '7175', street: 'Gaston Ave.', estimatedDwellingCount: 20, propertyName: 'Winsted at White Rock', description: '', isImported: false, isExpanded: false, contacts: [] }
        ]
    }
  }, 
  methods: {
    toggle(p) {
      p.isExpanded = !p.isExpanded;
    }
  }
}
</script>