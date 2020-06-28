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
          >{{ availableViews.PHONE_WITHOUT_VM.name }}</a>
        </li>
        <li class="nav-item">
          <a
            class="nav-link"
            :class="{ active: activeView === availableViews.PHONE_WITH_VM }"
            @click.prevent="setActiveView(availableViews.PHONE_WITH_VM)"
            href="#"
          >{{ availableViews.PHONE_WITH_VM.name }}</a>
        </li>
        <li class="nav-item">
          <a
            class="nav-link"
            :class="{ active: activeView === availableViews.LETTER }"
            @click.prevent="setActiveView(availableViews.LETTER)"
            href="#"
          >{{ availableViews.LETTER.name }}</a>
        </li>
        <li class="nav-item">
          <a
            class="nav-link"
            :class="{ active: activeView === availableViews.COMPLETE }"
            @click.prevent="setActiveView(availableViews.COMPLETE)"
            href="#"
          >{{ availableViews.COMPLETE.name }}</a>
        </li>
      </ul>
    </div>
    <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4">
      <template v-for="(a, i) in availableAssignments">
        <div :key="'c' + i" class="col mt-3">
          <div class="card shadow-sm rounded">
            <div class="card-body">
              <address>
                <strong>{{ a.lastName }}, {{ a.firstName }} {{ a.middleInitial }}</strong>
                <br />
                Age: {{ a.age }}
                <br />
                {{ a.mailingAddress1 }}, {{ a.mailingAddress2 }}
                <br />
                Dallas, TX {{a.postalCode}}
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

const VIEWS = Object.freeze({
  PHONE_WITHOUT_VM: { name: "1st Call (no voicemail)" },
  PHONE_WITH_VM: { name: "2nd Call (leave voicemail)" },
  LETTER: { name: "Write Letters" },
  COMPLETE: { name: "Done" }
});

export default {
  name: "PublisherInaccessibleAssign",
  data() {
    return {
      availableViews: VIEWS,
      activeView: VIEWS.PHONE_WITHOUT_VM,
      availableAssignments: [],
      items: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
    };
  },
  async created() {
    await this.loadAssignments();
  },
  computed: {},
  methods: {
    async loadAssignments() {
      this.availableAssignments = [];
      this.availableAssignments = await data.getAvailableInaccessibleAssignments();
    },
    async setActiveView(v) {
      this.activeView = v;
    }
  }
};
</script>
