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
          <li class="list-group-item list-group-item-action flex-column align-items-start">
            <div class="d-flex w-100 justify-content-end">
              <i class="arrow up"></i>
            </div>
          </li>
          <template>
            <li class="list-group-item flex-column align-items-start">
              <div class="mb-1 d-flex w-100">
                <small class="font-weight-bold">8/8/2020 - John Publisher</small>
              </div>
              <div class="mb-1 d-flex w-100">
                <small>Voicemail - No Name</small>
              </div>
              <div class="d-flex w-100">
                <small class="font-italic">different language. possibly vietnamese</small>
              </div>
            </li>
            <li class="list-group-item flex-column align-items-start">
              <div class="mb-1 d-flex w-100">
                <small class="font-weight-bold">8/8/2020 - John Publisher</small>
              </div>
              <div class="mb-1 d-flex w-100">
                <small>Voicemail - No Name</small>
              </div>
              <div class="d-flex w-100">
                <small class="font-italic">different language. possibly vietnamese</small>
              </div>
            </li>
            <li class="list-group-item flex-column align-items-start">
              <div class="mb-1 d-flex w-100">
                <small class="font-weight-bold">8/8/2020 - John Publisher</small>
              </div>
              <div class="mb-1 d-flex w-100">
                <small>Voicemail - No Name</small>
              </div>
              <div class="d-flex w-100">
                <small class="font-italic">different language. possibly vietnamese</small>
              </div>
            </li>
          </template>
        </ul>
      </div>
    </div>
  </div>
</template>

<script>
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
      contactActivity: [],
    };
  },
  methods: {},
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
  },
};
</script>