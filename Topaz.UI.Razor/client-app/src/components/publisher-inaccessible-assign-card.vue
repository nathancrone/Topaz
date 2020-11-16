<template>
  <div class="col mt-3">
    <div
      class="card shadow-sm rounded"
      :class="{
        'border-warning':
          clonedContact.doNotContactPhone || clonedContact.doNotContactLetter,
        'border-danger':
          clonedContact.doNotContactPhone && clonedContact.doNotContactLetter,
      }"
    >
      <div class="card-header d-flex">
        <div class="flex-grow-1">
          {{ clonedContact.lastName }}, {{ clonedContact.firstName }}
          {{ clonedContact.middleInitial }}
        </div>
        <div class="form-check">
          <input
            type="checkbox"
            class="form-check-input"
            v-model="clonedContact.selected"
            :id="`cb${clonedContact.inaccessibleContactId}`"
            :disabled="
              clonedContact.doNotContactPhone &&
                clonedContact.doNotContactLetter
            "
          />
        </div>
      </div>
      <div class="card-body">
        <div class="d-flex w-100 justify-content-end">
          <span
            v-if="
              clonedContact.doNotContactPhone &&
                !clonedContact.doNotContactLetter
            "
            class="badge badge-warning"
            >Do Not Phone</span
          >
          <span
            v-if="
              !clonedContact.doNotContactPhone &&
                clonedContact.doNotContactLetter
            "
            class="badge badge-warning"
            >Do Not Write</span
          >
          <span
            v-if="
              clonedContact.doNotContactPhone &&
                clonedContact.doNotContactLetter
            "
            class="badge badge-danger"
            >Do Not Phone or Write</span
          >
          <span
            v-if="clonedContact.assignPublisher"
            class="badge badge-secondary"
            >{{ clonedContact.assignPublisher.firstName }}
            {{ clonedContact.assignPublisher.lastName }}</span
          >
        </div>
        <address>
          Age: {{ clonedContact.age }}
          <br />
          {{ clonedContact.mailingAddress1 }}
          <template
            v-if="
              clonedContact.mailingAddress2 &&
                clonedContact.mailingAddress2 != ''
            "
          >
            <br />
            {{ clonedContact.mailingAddress2 }}
          </template>
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
                :class="{
                  up: contactActivityExpanded,
                  down: !contactActivityExpanded,
                }"
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
                <small class="font-weight-bold"
                  >{{ displayDate(a.activityDate) }} -
                  {{ a.publisher.firstName }} {{ a.publisher.lastName }}</small
                >
              </div>
              <div
                v-if="
                  a.contactActivityTypeId === 1 ||
                    (a.contactActivityTypeId === 2 && a.phoneResponseType)
                "
                class="mb-1 d-flex w-100"
              >
                <small>{{ a.phoneResponseType.name }}</small>
              </div>
              <div v-else class="mb-1 d-flex w-100">
                <small>{{ a.contactActivityType.name }}</small>
              </div>
              <div v-if="a.notes !== ''" class="d-flex w-100">
                <small class="font-italic">{{ a.notes }}</small>
              </div>
              <div v-if="a.inaccessibleTerritoryExportId">
                <small class="font-italic"
                  >export #{{ a.inaccessibleTerritoryExportId }}</small
                >
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
import { parseISO, format } from "date-fns";

export default {
  name: "PublisherInaccessibleAssignCard",
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
      return format(parseISO(date), "MMM dd, yyyy");
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
