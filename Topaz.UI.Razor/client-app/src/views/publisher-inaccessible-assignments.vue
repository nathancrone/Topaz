<template>
  <div>
    <datalist id="phoneResponseTypeList">
      <option v-for="(type, r) in phoneResponseTypes" :key="r">{{ type.name }}</option>
    </datalist>
    <div class="row mt-3">
      <ul class="nav nav-tabs">
        <li class="nav-item" v-if="assignmentsPhoneWithoutVoicemail.length != 0">
          <a
            class="nav-link"
            :class="{ active: activeView === availableViews.PHONE_WITHOUT_VM }"
            @click.prevent="setActiveView(availableViews.PHONE_WITHOUT_VM)"
            href="#"
          >
            {{ availableViews.PHONE_WITHOUT_VM.name }}
            <span
              class="badge badge-secondary"
            >{{ assignmentsPhoneWithoutVoicemail.length }}</span>
          </a>
        </li>
        <li class="nav-item" v-if="assignmentsPhoneWithVoicemail.length != 0">
          <a
            class="nav-link"
            :class="{ active: activeView === availableViews.PHONE_WITH_VM }"
            @click.prevent="setActiveView(availableViews.PHONE_WITH_VM)"
            href="#"
          >
            {{ availableViews.PHONE_WITH_VM.name }}
            <span
              class="badge badge-secondary"
            >{{ assignmentsPhoneWithVoicemail.length }}</span>
          </a>
        </li>
        <li class="nav-item" v-if="assignmentsLetter.length != 0">
          <a
            class="nav-link"
            :class="{ active: activeView === availableViews.LETTER }"
            @click.prevent="setActiveView(availableViews.LETTER)"
            href="#"
          >
            {{ availableViews.LETTER.name }}
            <span
              class="badge badge-secondary"
            >{{ assignmentsLetter.length }}</span>
          </a>
        </li>
      </ul>
    </div>
    <div
      v-if="activeView === availableViews.PHONE_WITHOUT_VM"
      class="row row-cols-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4"
    >
      <PublisherInaccessibleAssignmentPhoneCard
        v-for="(a, i) in assignmentsPhoneWithoutVoicemail"
        :key="'c' + i"
        :contact="a"
        :contactActivityType="1"
        :phoneResponseTypes="phoneResponseTypes"
      />
    </div>
    <div
      v-if="activeView === availableViews.PHONE_WITH_VM"
      class="row row-cols-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4"
    >
      <PublisherInaccessibleAssignmentPhoneCard
        v-for="(a, i) in assignmentsPhoneWithVoicemail"
        :key="'c' + i"
        :contact="a"
        :contactActivityType="2"
        :phoneResponseTypes="phoneResponseTypes"
      />
    </div>
    <div
      v-if="activeView === availableViews.LETTER"
      class="row row-cols-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4"
    >
      <template v-for="(a, i) in assignmentsLetter">
        <div :key="'c' + i" class="col mt-3">
          <div class="card shadow-sm rounded">
            <div class="card-header d-flex">
              <div class="flex-grow-1">{{ a.lastName }}, {{ a.firstName }} {{ a.middleInitial }}</div>
              <div class="form-check">
                <input type="checkbox" class="form-check-input" v-model="a.selected" :id="'cb' + i" />
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
import PublisherInaccessibleAssignmentPhoneCard from "../components/publisher-inaccessible-assignment-phone-card";

const VIEWS = Object.freeze({
  PHONE_WITHOUT_VM: { type: "phone", name: "1st Call (no voicemail)" },
  PHONE_WITH_VM: { type: "vm", name: "2nd Call (leave voicemail)" },
  LETTER: { type: "letter", name: "Write Letters" },
});

export default {
  name: "PublisherInaccessibleAssignments",
  data() {
    return {
      availableViews: VIEWS,
      activeView: VIEWS.PHONE_WITHOUT_VM,
      phoneResponseTypes: [],
      assignmentsPhoneWithoutVoicemail: [],
      assignmentsPhoneWithVoicemail: [],
      assignmentsLetter: [],
    };
  },
  components: {
    PublisherInaccessibleAssignmentPhoneCard,
  },
  async created() {
    await this.loadPhoneResponseTypes();
    await this.loadAssignments();

    if (this.assignmentsPhoneWithoutVoicemail.length != 0) {
      this.activeView = VIEWS.PHONE_WITHOUT_VM;
    } else if (this.assignmentsPhoneWithVoicemail.length != 0) {
      this.activeView = VIEWS.PHONE_WITH_VM;
    } else if (this.assignmentsLetter.length != 0) {
      this.activeView = VIEWS.LETTER;
    }
  },
  methods: {
    async loadPhoneResponseTypes() {
      const types = await data.getPhoneResponseTypes();
      this.phoneResponseTypes = types;
    },
    async loadAssignments() {
      const assignments = await data.currentUserInaccessibleAssignments();
      this.assignmentsPhoneWithoutVoicemail = [];
      this.assignmentsPhoneWithVoicemail = [];
      this.assignmentsLetter = [];

      assignments.phoneWithoutVoicemail.forEach(function (a) {
        a.responseTypeSearch = "";
        a.notes = "";
      });
      this.assignmentsPhoneWithoutVoicemail = assignments.phoneWithoutVoicemail;

      assignments.phoneWithVoicemail.forEach(function (a) {
        a.responseTypeSearch = "";
        a.notes = "";
      });
      this.assignmentsPhoneWithVoicemail = assignments.phoneWithVoicemail;

      this.assignmentsLetter = assignments.letter;
    },
    async setActiveView(v) {
      this.activeView = v;
    },
  },
};
</script>