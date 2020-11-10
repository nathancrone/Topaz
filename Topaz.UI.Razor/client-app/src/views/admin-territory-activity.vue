<template>
<h1>hello world</h1>
</template>

<script>
export default {
  name: "AdminTerritoryActivity",
  props: {
    type: {
      type: String,
      default: 'street',
    },
    id: {
      type: Number,
      default: 0,
    },
  },
  data() {
    return {};
  },
};
</script>
