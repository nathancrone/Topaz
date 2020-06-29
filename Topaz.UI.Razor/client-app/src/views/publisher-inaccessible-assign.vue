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
    <div class="row mt-3">
      <div class="d-flex col">
        <div class="flex-grow-1">
          <div class="form-group form-check">
            <input type="checkbox" class="form-check-input mr-1" id="unassignedOnly" />
            <label class="form-check-label" for="unassignedOnly">show only unassigned</label>
          </div>
        </div>
        <div>
          <a class="btn btn-primary disabled" href="#">assign...</a>
          <a class="btn btn-primary mr-1" href="#">refresh</a>
        </div>
      </div>
    </div>
    <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4">
      <template v-for="(a, i) in availableAssignments">
        <div :key="'c' + i" class="col mt-3">
          <div class="card shadow-sm rounded">
            <div class="card-header d-flex">
              <div class="flex-grow-1">{{ a.lastName }}, {{ a.firstName }} {{ a.middleInitial }}</div>
              <div class="form-check">
                <input type="checkbox" class="form-check-input" :id="'cb' + i" />
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
  props: {
    id: {
      type: Number,
      default: 0
    }
  },
  data() {
    return {
      availableViews: VIEWS,
      activeView: VIEWS.PHONE_WITHOUT_VM,
      availableAssignments: []
    };
  },
  async created() {
    await this.loadAssignments();
  },
  computed: {},
  methods: {
    async loadAssignments() {
      this.availableAssignments = [];
      this.availableAssignments = await data.getAvailableInaccessibleAssignments(
        this.id
      );
    },
    async setActiveView(v) {
      this.activeView = v;
    }
  }
};
</script>
