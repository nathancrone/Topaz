<template>
  <div class="col mt-3">
    <div class="card shadow-sm rounded">
      <div class="card-header d-flex">
        <div
          class="flex-grow-1"
        >{{ clonedContact.lastName }}, {{ clonedContact.firstName }} {{ clonedContact.middleInitial }}</div>
        <div class="form-check">
          <input
            type="checkbox"
            class="form-check-input"
            v-model="clonedContact.selected"
            :id="`cb${clonedContact.inaccessibleContactId}`"
          />
        </div>
      </div>
      <div class="card-body">
        <div class="d-flex w-100 justify-content-end">
          <span
            v-if="clonedContact.assignPublisher"
            class="badge badge-secondary"
          >{{ clonedContact.assignPublisher.firstName }} {{ clonedContact.assignPublisher.lastName }}</span>
        </div>
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
  name: "PublisherInaccessibleAssignPhoneCard",
  props: {
    contact: {
      type: Object,
      default: () => {},
    },
  },
  data() {
    return {
      clonedContact: { ...this.contact },
      contactActivityExpanded: false,
      contactActivityLoaded: false,
      contactActivity: [],
    };
  },
  methods: {
    toggle() {
      this.contactActivityExpanded = !this.contactActivityExpanded;
    },
    async loadActivity() {
      const activity = await data.getContactActivity(
        this.clonedContact.inaccessibleContactId
      );
      this.contactActivity = [...activity];
    },
    displayDate(date) {
      return format(data.parseDate(date), "MM/dd/yyyy");
    },
  },
  watch: {
    "clonedContact.selected": {
      immediate: true,
      handler() {
        this.$emit("change", this.clonedContact);
      },
    },
    contact: {
      handler(after) {
        this.clonedContact = { ...after };
      },
    },
    "contact.selected": {
      immediate: true,
      handler(after) {
        this.clonedContact.selected = after;
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