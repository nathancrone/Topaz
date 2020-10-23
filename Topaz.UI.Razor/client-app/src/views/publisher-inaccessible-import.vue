<template>
  <div>
      <div v-if="true" class="d-flex">
        <div class="flex-grow-1 mb-2">&nbsp;</div>
        <div class="flex-grow-1 flex-md-grow-0 mb-2">
          <button type="button" class="close" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
      </div>
      <div v-else class="w-100 alert alert-primary" role="alert">no properties found for this territory</div>
    <div :class="{ 'card': true }" 
      v-for="p in properties"
      :key="`card${p.inaccessiblePropertyId}`">
        <ul class="bg-light list-group list-group-flush">
            <a href="#" class="d-flex justify-content-between list-group-item list-group-item-action" v-on:click.prevent="toggle(p)">
            <div>
                <strong>{{ p.streetNumbers }} {{ p.street }}<template v-if="p.propertyName"> ({{ p.propertyName }})</template></strong>
            </div>
            <div>
                <span class="badge badge-secondary mr-2">{{p.contacts.length}}</span>
                <i :class="{'arrow': true, 'down': !p.isExpanded, 'up': p.isExpanded }"></i>
            </div>
            </a>
        </ul>
        <div :class="{'card-body': true, 'collapse': true, 'show': p.isExpanded}">
          <div v-if="p.errors.length !== 0" class="alert alert-danger" role="alert">
            <ul>
              <li v-for="(m, i) in p.errors" :key="i">{{ m }}</li>
            </ul>
          </div>
          <div v-if="p.warnings.length !== 0" class="alert alert-warning" role="alert">
            <ul>
              <li v-for="(m, i) in p.warnings" :key="i">{{ m }}</li>
            </ul>
          </div>
          <form v-if="p.fileContacts.length === 0 && p.contacts.length === 0">
              <div class="form-group">
                  <label :for="`fileInput${p.inaccessiblePropertyId}`">Import File</label>
                  <input type="file" accept=".csv" class="form-control-file" :id="`fileInput${p.inaccessiblePropertyId}`" @change="changeFile($event, p.inaccessiblePropertyId)">
              </div>
              <a v-if="p.file" class="btn btn-primary" href="#" role="button" @click.prevent="uploadFile(p.inaccessiblePropertyId)">Upload</a>
          </form>
          <table v-else class="table table-bordered">
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
                            {{ c.firstName }}
                        </td>
                        <td>
                            {{ c.lastName }}
                        </td>
                        <td>
                            {{ c.middleInitial }}
                        </td>
                        <td>
                            {{ c.age }}
                        </td>
                        <td>
                            {{ c.mailingAddress1 }}
                        </td>
                        <td>
                            {{ c.mailingAddress2 }}
                        </td>
                        <td>
                            {{ c.postalCode }}
                        </td>
                        <td>
                            {{ c.phoneNumber }}
                        </td>
                        <td>
                            {{ c.phoneType }}
                        </td>
                    </tr>
                    <tr :key="`row-message-${i}`" v-if="c.errors.length !== 0 || c.warnings.length !== 0">
                      <td colspan="9">
                        <div v-if="c.errors.length !== 0" class="alert alert-danger" role="alert">
                          <h5 class="alert-heading">Errors</h5>
                          <ul>
                            <li v-for="(e, x) in c.errors" :key="`err-${i}-${x}`">{{ e }}</li>
                          </ul>
                        </div>
                        <div v-if="c.warnings.length !== 0" class="alert alert-warning" role="alert">
                          <h5 class="alert-warning">Warnings</h5>
                          <ul>
                            <li v-for="(w, x) in c.warnings" :key="`warn-${i}-${x}`">{{ w }}</li>
                          </ul>
                        </div>
                      </td>
                    </tr>
                  </template>
                  <tr v-for="(c, i) in p.contacts" :key="i">
                      <td>
                          {{ c.firstName }}
                      </td>
                      <td>
                          {{ c.lastName }}
                      </td>
                      <td>
                          {{ c.middleInitial }}
                      </td>
                      <td>
                          {{ c.age }}
                      </td>
                      <td>
                          {{ c.mailingAddress1 }}
                      </td>
                      <td>
                          {{ c.mailingAddress2 }}
                      </td>
                      <td>
                          {{ c.postalCode }}
                      </td>
                      <td>
                          {{ c.phoneNumber }}
                      </td>
                      <td>
                          {{ c.phoneType.name }}
                      </td>
                  </tr>
              </tbody>
          </table>

          <button v-if="p.fileContacts.length === 0 && p.contacts.length !== 0 && !p.removeContacts" @click.prevent="p.removeContacts = true" type="button" class="btn btn-primary">Remove Contacts</button>

          <div v-if="p.fileContacts.length === 0 && p.contacts.length !== 0 && p.removeContacts" class="alert alert-warning" role="alert">
            <h5 class="alert-heading">Remove Contacts</h5>
            <p>Are you sure you want to remove these {{ p.contacts.length }} contacts from this property?</p>
            <hr>
            <button type="button" class="btn btn-secondary btn-sm mr-1" @click.prevent="p.removeContacts = false">Cancel</button>
            <button type="button" class="btn btn-primary btn-sm" @click.prevent="removeContactsConfirm(p.inaccessiblePropertyId)">Confirm</button>
          </div>

          <div v-if="p.fileContacts.length > p.rowErrors" :class="{ 'alert': true, 'alert-success': !(p.rowErrors|p.rowWarnings), 'alert-warning': (p.rowErrors|p.rowWarnings) }" role="alert">
            <h5 class="alert-heading">Please Review...</h5>
            <p v-if="!(p.rowErrors|p.rowWarnings)">There are {{ p.fileContacts.length }} contacts in this file. Are you ready to import these contacts?</p>
            <p v-else>There are {{ p.fileContacts.length }} contacts in this file. {{p.rowErrors}} contacts have errors and will not be imported. {{ p.rowWarnings }} contacts with warnings have some information that cannot be imported. Are you sure you want to import these contacts?</p>
            <hr>
            <button type="button" class="btn btn-secondary btn-sm mr-1" @click.prevent="fileContactCancel(p.inaccessiblePropertyId)">Cancel</button>
            <button type="button" class="btn btn-primary btn-sm" @click.prevent="fileContactConfirm(p.inaccessiblePropertyId)">Confirm</button>
          </div>

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
        p.errors = [];
        p.warnings = [];
        p.rowErrors = 0;
        p.rowWarnings = 0;
        p.file = null;
        p.fileContacts = [];
        p.removeContacts = false;
        p.contacts = (p.contacts) ? p.contacts : [];
      });
      this.properties = [...properties];
    }, 
    toggle(p) {
      p.isExpanded = !p.isExpanded;
    },
    changeFile(evt, id) {
      var property = this.properties.find((p) => p.inaccessiblePropertyId === id);
      property.file = (evt.target.files.length !== 0) ? evt.target.files[0] : null;
    }, 
    async uploadFile(id) {
      var property = this.properties.find((p) => p.inaccessiblePropertyId === id);
      var result = await data.uploadContactsCsv(property.file);
      property.errors = result.errors;
      property.warnings = result.warnings;
      property.rowErrors = result.rowErrors;
      property.rowWarnings = result.rowWarnings;
      property.fileContacts = result.rows;
    }, 
    fileContactCancel(id) {
      var property = this.properties.find((p) => p.inaccessiblePropertyId === id);
      property.errors = [];
      property.warnings = [];
      property.rowErrors = 0;
      property.rowWarnings = 0;
      property.file = null;
      property.fileContacts = [];
    }, 
    async fileContactConfirm(id) {
      var property = this.properties.find((p) => p.inaccessiblePropertyId === id);
      var result = await data.uploadContacts(id, property.fileContacts);
      property.errors = [];
      property.warnings = [];
      property.rowErrors = 0;
      property.rowWarnings = 0;
      property.file = null;
      property.fileContacts = [];
      property.contacts = result;
    }, 
    async removeContactsConfirm(id) {
      var property = this.properties.find((p) => p.inaccessiblePropertyId === id);
      await data.removePropertyContactList(id);
      property.errors = [];
      property.warnings = [];
      property.rowErrors = 0;
      property.rowWarnings = 0;
      property.file = null;
      property.fileContacts = [];
      property.contacts = [];
      property.removeContacts = false;
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