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
          <div v-if="p.errorMessages.length !== 0" class="alert alert-danger" role="alert">
            <ul>
              <li v-for="(m, i) in p.errorMessages" :key="i">{{ m }}</li>
            </ul>
          </div>
          <div v-if="p.warningMessages.length !== 0" class="alert alert-warning" role="alert">
            <ul>
              <li v-for="(m, i) in p.warningMessages" :key="i">{{ m }}</li>
            </ul>
          </div>
          <div v-if="p.successMessages.length !== 0" class="alert alert-success" role="alert">
            <ul>
              <li v-for="(m, i) in p.successMessages" :key="i">{{ m }}</li>
            </ul>
          </div>
          <form v-if="p.fileContacts.length === 0 && p.contacts.length === 0">
              <div class="form-group">
                  <label :for="`fileInput${p.inaccessiblePropertyId}`">Import File</label>
                  <input type="file" accept=".csv" class="form-control-file" :id="`fileInput${p.inaccessiblePropertyId}`" @change="changeFile($event, p.inaccessiblePropertyId)">
              </div>
              <a v-if="p.file" class="btn btn-primary" href="#" role="button" @click.prevent="uploadFile(p.inaccessiblePropertyId)">Upload</a>
          </form>
          <table v-else class="table">
              <thead>
                  <tr>
                      <th>
                          First Name
                      </th>
                      <th>
                          Last Name
                      </th>
                      <th>
                          Middle Initial
                      </th>
                      <th>
                          Age
                      </th>
                      <th>
                          Address 1
                      </th>
                      <th>
                          Address 2
                      </th>
                      <th>
                          Postal Code
                      </th>
                      <th>
                          Phone
                      </th>
                      <th>
                          Phone Type
                      </th>
                  </tr>
              </thead>
              <tbody>
                  <template v-for="(c, i) in p.fileContacts">
                    <tr :key="`row-contact-${i}`">
                        <td>
                            {{ c.FirstName }}
                        </td>
                        <td>
                            {{ c.LastName }}
                        </td>
                        <td>
                            {{ c.MiddleInitial }}
                        </td>
                        <td>
                            {{ c.Age }}
                        </td>
                        <td>
                            {{ c.MailingAddress1 }}
                        </td>
                        <td>
                            {{ c.MailingAddress2 }}
                        </td>
                        <td>
                            {{ c.PostalCode }}
                        </td>
                        <td>
                            {{ c.PhoneNumber }}
                        </td>
                        <td>
                            {{ c.PhoneType }}
                        </td>
                    </tr>
                    <tr class="table-light" :key="`row-message-${i}`" v-if="c.Errors.length !== 0 || c.Warnings.length !== 0">
                      <td colspan="9">
                        <div v-if="c.Errors.length !== 0" class="alert alert-danger" role="alert">
                          <h5 class="alert-heading">Errors</h5>
                          <ul>
                            <li v-for="(e, x) in c.Errors" :key="`err-${i}-${x}`">{{ e }}</li>
                          </ul>
                        </div>
                        <div v-if="c.Warnings.length !== 0" class="alert alert-warning" role="alert">
                          <h5 class="alert-warning">Warnings</h5>
                          <ul>
                            <li v-for="(w, x) in c.Warnings" :key="`warn-${i}-${x}`">{{ w }}</li>
                          </ul>
                        </div>
                      </td>
                    </tr>
                  </template>
                  <tr v-for="(c, i) in p.contacts" :key="i">
                      <td>
                          {{ c.FirstName }}
                      </td>
                      <td>
                          {{ c.LastName }}
                      </td>
                      <td>
                          {{ c.MiddleInitial }}
                      </td>
                      <td>
                          {{ c.Age }}
                      </td>
                      <td>
                          {{ c.MailingAddress1 }}
                      </td>
                      <td>
                          {{ c.MailingAddress2 }}
                      </td>
                        <td>
                            {{ c.PostalCode }}
                        </td>
                      <td>
                          {{ c.PhoneNumber }}
                      </td>
                      <td>
                          {{ c.PhoneType }}
                      </td>
                  </tr>
              </tbody>
          </table>

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
        p.errorMessages = [];
        p.warningMessages = [];
        p.successMessages = [];
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
      var result = await data.uploadContactsCsv(property.file);
      property.errorMessages = result.errors;
      property.warningMessages = result.warnings;
      property.successMessages = result.success;
      property.fileContacts = result.contacts;
    }, 
    toggle(p) {
      p.isExpanded = !p.isExpanded;
    }
  }
}
</script>
<style scoped>
div.alert ul {
  margin: 0;
}
td div.alert {
  margin-bottom: inherit;
}
</style>