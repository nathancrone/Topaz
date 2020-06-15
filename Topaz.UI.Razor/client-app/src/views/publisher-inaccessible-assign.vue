<template>
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
  </ul>
</template>

<script>
//import { data } from "../shared";

const VIEWS = Object.freeze({
  PHONE_WITHOUT_VM: { name: "Call (no voicemail)" },
  PHONE_WITH_VM: { name: "Call (leave voicemail)" },
  LETTER: { name: "Letters" }
});

export default {
  name: "PublisherInaccessibleAssign",
  data() {
    return {
      availableViews: VIEWS,
      activeView: VIEWS.PHONE_WITHOUT_VM,
      territories: []
    };
  },
  // async created() {
  //   await this.loadTerritories();
  // },
  computed: {
    territoryRows() {
      return this.territories.map(x => {
        return Object.assign(
          {},
          { ...x },
          {
            territoryCode: [x.streetTerritoryCode, x.territoryCode].join(" / ")
          }
        );
      });
    }
  },
  methods: {
    async setActiveView(v) {
      this.activeView = v;
    }
  }
};
</script>
