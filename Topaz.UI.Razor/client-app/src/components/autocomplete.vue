<template>
  <div style="position:relative">
    <ul
      class="dropdown-menu"
      :class="{ show: openSuggestion }"
      style="width:100%"
    >
      <li
        v-for="(suggestion, i) in matches"
        class="dropdown-item"
        :class="{ active: isActive(suggestion) }"
        :key="i"
      >
        <a href="#" @click.prevent="suggestionClick(suggestion)">{{
          suggestion.label
        }}</a>
      </li>
    </ul>
    <input class="form-control" type="text" v-model="search" @input="change" />
  </div>
</template>
<script>
export default {
  props: {
    suggestions: {
      type: Array,
      required: true,
    },
  },
  data() {
    return {
      search: "",
      selection: null,
      open: false,
    };
  },
  computed: {
    matches() {
      return this.suggestions.filter((s) => {
        return s.label.indexOf(this.search) >= 0;
      });
    },
    openSuggestion() {
      return (
        this.selection !== "" && this.matches.length != 0 && this.open === true
      );
    },
  },
  methods: {
    //For highlighting element
    isActive(suggestion) {
      return suggestion === this.selection;
    },

    //When the user changes input
    change() {
      if (this.open == false) {
        this.open = true;
        this.selection = null;
      }
    },

    //When one of the suggestion is clicked
    suggestionClick(suggestion) {
      this.selection = suggestion;
      this.search = this.selection.label;
      this.open = false;
    },
  },
};
</script>
