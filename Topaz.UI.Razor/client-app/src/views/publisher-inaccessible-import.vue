<template>
  <div>
      <div class="accordion">

          <div class="card" 
            v-for="p in properties"
            :key="`card${p.inaccessiblePropertyId}`">
              <div :class="{ 'd-flex': true, 'card-header': true, 'collapsed': !p.isExpanded}">
                  <div class="w-100 justify-content-between">
                      {{ p.street }} ({{ p.streetNumbers }}) - {{ p.propertyName }}
                  </div>
                  <div class="justify-content-end">
                      <i class="arrow down"></i>
                  </div>
              </div>
              <div :class="{'card-body': true, 'collapse': true, 'show': p.isExpanded}">
                <form v-if="!p.isImported">
                    <div class="form-group">
                        <label for="exampleFormControlFile1">Example file input</label>
                        <input type="file" class="form-control-file" id="exampleFormControlFile1">
                    </div>
                </form>
              </div>
          </div>

          <!--
          <div class="card">
              <div class="card-header">
                  Property X
              </div>
              <div class="card-body collapse show">
                  Lorem ipsum dolor sit amet.
              </div>
          </div>
          <div class="card">
              <div class="card-header collapsed">
                  Property X
              </div>
              <div class="card-body collapse">
                  Lorem ipsum dolor sit amet.
              </div>
          </div>
          <div class="card">
              <div class="card-header collapsed">
                  Property X
              </div>
              <div class="card-body collapse">
                  Lorem ipsum dolor sit amet.
              </div>
          </div>-->
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
            { inaccessiblePropertyId: 3, streetNumbers: '7175', street: 'Gaston Ave.', estimatedDwellingCount: 20, propertyName: 'Winsted at White Rock', description: '', isImported: false, isExpanded: false, contacts: [] },
            { inaccessiblePropertyId: 4, streetNumbers: '7177', street: 'Gaston Ave.', estimatedDwellingCount: 20, propertyName: 'Winsted at White Rock', description: '', isImported: false, isExpanded: false, contacts: [] },
            { inaccessiblePropertyId: 5, streetNumbers: '7179', street: 'Gaston Ave.', estimatedDwellingCount: 20, propertyName: 'Winsted at White Rock', description: '', isImported: false, isExpanded: false, contacts: [] }
        ]
    }
  }
}
</script>