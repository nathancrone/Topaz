<template>
  <div>
    <div class="row mt-3">
      <ul class="nav nav-tabs">
        <li class="nav-item">
          <a
            class="nav-link"
            :class="{ active: activeView === availableViews.PHONE_WITHOUT_VM }"
            @click.prevent="setActiveView(availableViews.PHONE_WITHOUT_VM)"
            href="#"
            >{{ availableViews.PHONE_WITHOUT_VM.name }}</a
          >
        </li>
        <li class="nav-item">
          <a
            class="nav-link"
            :class="{ active: activeView === availableViews.PHONE_WITH_VM }"
            @click.prevent="setActiveView(availableViews.PHONE_WITH_VM)"
            href="#"
            >{{ availableViews.PHONE_WITH_VM.name }}</a
          >
        </li>
        <li class="nav-item">
          <a
            class="nav-link"
            :class="{ active: activeView === availableViews.LETTER }"
            @click.prevent="setActiveView(availableViews.LETTER)"
            href="#"
            >{{ availableViews.LETTER.name }}</a
          >
        </li>
        <li class="nav-item">
          <a
            class="nav-link"
            :class="{ active: activeView === availableViews.COMPLETE }"
            @click.prevent="setActiveView(availableViews.COMPLETE)"
            href="#"
            >{{ availableViews.COMPLETE.name }}</a
          >
        </li>
      </ul>
    </div>
    <div class="row mt-3">
      <div class="d-flex col">
        <div class="flex-grow-1">
          <div class="form-group form-check">
            <input
              type="checkbox"
              class="form-check-input mr-1"
              id="unassignedOnly"
            />
            <label class="form-check-label" for="unassignedOnly"
              >show only unassigned</label
            >
          </div>
        </div>
        <div class="mr-1">
          <autocomplete :suggestions="suggestions"></autocomplete>
        </div>
        <div>
          <a
            v-if="!isAssignMode"
            class="btn btn-primary mr-1"
            :class="{ disabled: !isAssignmentSelected }"
            href="#"
            @click.prevent="toggleAssignMode"
            >assign...</a
          >
          <a class="btn btn-primary" href="#" @click.prevent="loadAssignments"
            >refresh</a
          >
        </div>
      </div>
    </div>
    <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4">
      <template v-for="(a, i) in availableAssignments">
        <div :key="'c' + i" class="col mt-3">
          <div class="card shadow-sm rounded">
            <div class="card-header d-flex">
              <div class="flex-grow-1">
                {{ a.lastName }}, {{ a.firstName }} {{ a.middleInitial }}
              </div>
              <div class="form-check">
                <input
                  type="checkbox"
                  class="form-check-input"
                  v-model="a.selected"
                  :id="'cb' + i"
                />
              </div>
            </div>
            <div class="card-body">
              <address>
                Age: {{ a.age }}
                <br />
                {{ a.mailingAddress1 }}
                <br />
                {{ a.mailingAddress2 }}
                <br />
                Dallas, TX {{ a.postalCode }}
                <br />
                {{ a.phoneNumber }}
              </address>
            </div>
          </div>
        </div>
      </template>
    </div>
  </div>
</template>

<script>
import { data } from "../shared";
import autocomplete from "../components/autocomplete.vue";

const VIEWS = Object.freeze({
  PHONE_WITHOUT_VM: { type: "phone", name: "1st Call (no voicemail)" },
  PHONE_WITH_VM: { type: "vm", name: "2nd Call (leave voicemail)" },
  LETTER: { type: "letter", name: "Write Letters" },
  COMPLETE: { type: "none", name: "Done" },
});

export default {
  name: "PublisherInaccessibleAssign",
  props: {
    id: {
      type: Number,
      default: 0,
    },
  },
  components: { autocomplete },
  data() {
    return {
      availableViews: VIEWS,
      activeView: VIEWS.PHONE_WITHOUT_VM,
      availableAssignments: [],
      isAssignMode: false,
      suggestions: [
        { label: "Bangalore", value: 0 },
        { label: "Chennai", value: 1 },
        { label: "Cochin", value: 2 },
        { label: "Delhi", value: 3 },
        { label: "Kolkata", value: 4 },
        { label: "Mumbai", value: 5 },
      ],
    };
  },
  async created() {
    await this.loadAssignments();
  },
  computed: {
    isAssignmentSelected() {
      return this.availableAssignments.some((element) => {
        return element.selected === true;
      });
    },
  },
  methods: {
    async loadAssignments() {
      const assignments = await data.getAvailableInaccessibleAssignments(
        this.id,
        this.activeView.type
      );
      assignments.forEach(function(element) {
        element.selected = false;
      });
      this.availableAssignments = [];
      this.availableAssignments = assignments;
    },
    async setActiveView(v) {
      this.activeView = v;
      await this.loadAssignments();
    },
    toggleAssignMode() {
      this.isAssignMode = !this.isAssignMode;
    },
  },
};
</script>
