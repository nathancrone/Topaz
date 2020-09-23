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
          <template
            v-if="clonedContact.mailingAddress2 && clonedContact.mailingAddress2 != ''"
          >
            <br />
            {{ clonedContact.mailingAddress2 }}
          </template>
          <br />
          Dallas, TX {{ clonedContact.postalCode }}
          <br />
          <a
            v-if="clonedContact.phoneNumber"
            :href="`tel:${clonedContact.phoneNumber.replace(/\D/g, '')}`"
          >{{ clonedContact.phoneNumber }}</a>
        </address>
        <div v-if="contactActivityType === 1 || contactActivityType === 2" class="form-group">
          <label :for="`phoneResponseType${clonedContact.inaccessibleContactId}`">Response</label>
          <input
            :id="`phoneResponseType${clonedContact.inaccessibleContactId}`"
            type="text"
            class="form-control"
            autocomplete="off"
            placeholder="enter result"
            list="phoneResponseTypeList"
            v-model="responseTypeSearch"
          />
        </div>
        <div v-else class="form-group form-check">
          <input
            type="checkbox"
            class="form-check-input"
            :id="`letterSent${clonedContact.inaccessibleContactId}`"
            v-model="letterSent"
          />
          <label
            class="form-check-label"
            :for="`letterSent${clonedContact.inaccessibleContactId}`"
          >Letter sent?</label>
        </div>
        <div v-if="selectedResponseType || letterSent" class="form-group">
          <label for="notes">Notes</label>
          <textarea
            class="form-control"
            id="notes"
            rows="3"
            placeholder="notes"
            v-model="clonedContact.notes"
            maxlength="140"
          ></textarea>
        </div>
        <a
          v-if="selectedResponseType || letterSent"
          class="btn btn-primary mr-1 mb-4"
          href="#"
          @click.prevent="save"
        >save</a>
        <ul class="list-group">
          <li
            @click.prevent="toggle"
            class="list-group-item list-group-item-action flex-column align-items-start"
          >
            <div class="d-flex w-100 justify-content-end">
              <i
                class="arrow"
                :class="{ up: contactActivityExpanded, down: !contactActivityExpanded }"
              ></i>
            </div>
          </li>
          <template v-if="contactActivityExpanded">
            <li
              v-if="contactActivity.length === 0 && contactActivityLoaded"
              class="list-group-item flex-column align-items-start"
            >
              <div class="mb-1 d-flex w-100">
                <small>no history recorded</small>
              </div>
            </li>
            <li
              v-for="a in contactActivity"
              :key="`item${a.inaccessibleContactActivityId}`"
              class="list-group-item flex-column align-items-start"
            >
              <div class="mb-1 d-flex w-100">
                <small
                  class="font-weight-bold"
                >{{ displayDate(a.activityDate) }} - {{ a.publisher.firstName }} {{ a.publisher.lastName }}</small>
              </div>
              <div
                v-if="a.contactActivityTypeId === 1 || a.contactActivityTypeId === 2 && a.phoneResponseType"
                class="mb-1 d-flex w-100"
              >
                <small>{{ a.phoneResponseType.name }}</small>
              </div>
              <div v-if="a.contactActivityTypeId === 3" class="mb-1 d-flex w-100">
                <small>{{ a.contactActivityType.name }}</small>
              </div>
              <div class="d-flex w-100">
                <small class="font-italic">{{ a.notes }}</small>
              </div>
            </li>
          </template>
        </ul>
      </div>
    </div>
  </div>
</template>

<script>
import { data } from "../shared";
import { format } from "date-fns";

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
      contactActivityExpanded: false,
      contactActivityLoaded: false,
      contactActivity: [],
      responseTypeSearch: "",
      selectedResponseType: undefined,
      letterSent: false,
    };
  },
  methods: {
    async loadActivity() {
      const activity = await data.getContactActivity(
        this.clonedContact.inaccessibleContactId
      );
      this.contactActivity = [...activity];
    },
    async save() {
      this.letterSent
        ? await data.saveInaccessibleContactLetterActivity(
            this.clonedContact.inaccessibleContactId,
            this.clonedContact.notes
          )
        : await data.saveInaccessibleContactPhoneActivity(
            this.clonedContact.inaccessibleContactId,
            this.contactActivityType,
            this.clonedContact.notes,
            this.selectedResponseType.phoneResponseTypeId
          );
      this.$emit("saved");
    },
    toggle() {
      this.contactActivityExpanded = !this.contactActivityExpanded;
    },
    displayDate(date) {
      return format(data.parseDate(date), "MM/dd/yyyy");
    },
  },
  watch: {
    responseTypeSearch: {
      immediate: true,
      handler(after) {
        // find available assignee that exactly matches
        const responseType = this.phoneResponseTypes.find(
          ({ name }) => name.toLowerCase() === after.toLowerCase()
        );

        // if exact match found, set the selected response type
        // if not, clear the selected response type
        if (responseType) {
          this.selectedResponseType = Object.assign({}, responseType);
        } else {
          this.selectedResponseType = undefined;
        }
      },
    },
    contact: {
      handler(after) {
        this.clonedContact = { ...after };
      },
    },
    contactActivityExpanded: {
      async handler(after) {
        if (after && !this.contactActivityLoaded) {
          await this.loadActivity();
          this.contactActivityLoaded = true;
        }
      },
    },
  },
};
</script>