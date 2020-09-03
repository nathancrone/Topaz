<template>
  <div class="col mt-3">
    <div class="card shadow-sm rounded">
      <div class="card-header d-flex">
        <div
          class="flex-grow-1"
        >{{ clonedContact.lastName }}, {{ clonedContact.firstName }} {{ clonedContact.middleInitial }}</div>
      </div>
      <div class="card-body">
        <address>
          Age: {{ clonedContact.age }}
          <br />
          {{ clonedContact.mailingAddress1 }}
          <br />
          {{ clonedContact.mailingAddress2 }}
          <br />
          Dallas, TX {{ clonedContact.postalCode }}
          <br />
          {{ clonedContact.phoneNumber }}
        </address>
        <div class="form-group">
          <label :for="`phoneResponseType${clonedContact.inaccessibleContactId}`">Response</label>
          <input
            :id="`phoneResponseType${clonedContact.inaccessibleContactId}`"
            type="text"
            class="form-control"
            autocomplete="off"
            placeholder="enter result"
            list="phoneResponseTypeList"
            v-model="clonedContact.responseTypeSearch"
          />
        </div>
        <div v-if="selectedReponseType" class="form-group">
          <label for="notes">Notes</label>
          <textarea
            class="form-control"
            id="notes"
            rows="3"
            placeholder="notes"
            v-model="clonedContact.notes"
          ></textarea>
        </div>
        <a
          v-if="selectedReponseType"
          class="btn btn-primary mr-1"
          href="#"
          @click.prevent="save"
        >save</a>
      </div>
    </div>
  </div>
</template>

<script>
import { data } from "../shared";

export default {
  name: "PublisherInaccessibleAssignmentPhoneCard",
  props: {
    contact: {
      type: Object,
      default: () => {},
    },
    contactActivityType: {
      type: Number,
      default: () => -1,
    },
    phoneResponseTypes: {
      type: Array,
      default: () => [],
    },
  },
  data() {
    return {
      clonedContact: { ...this.contact },
      selectedReponseType: undefined,
    };
  },
  methods: {
    async save() {
      await data.saveInaccessibleContactPhoneActivity(
        this.clonedContact.inaccessibleContactId,
        this.contactActivityType,
        this.clonedContact.notes,
        this.selectedReponseType.phoneResponseTypeId
      );
    },
  },
  watch: {
    "clonedContact.responseTypeSearch": {
      immediate: true,
      handler(newValue) {
        // find available assignee that exactly matches
        const reponseType = this.phoneResponseTypes.find(
          ({ name }) => name.toLowerCase() === newValue.toLowerCase()
        );

        // if exact match found, set the selected response type
        // if not, clear the selected response type
        if (reponseType) {
          this.selectedReponseType = Object.assign({}, reponseType);
        } else {
          this.selectedReponseType = undefined;
        }
      },
    },
  },
};
</script>