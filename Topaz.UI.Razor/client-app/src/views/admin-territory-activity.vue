<template>
  <div>
    <div class="d-flex">
      <div class="flex-grow-1 mb-2">&nbsp;</div>
      <div class="flex-grow-1 flex-md-grow-0 mb-2">
        <button
          @click="$router.go(-1)"
          type="button"
          class="close"
          aria-label="Close"
        >
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
    </div>
    <h1>hello world</h1>
  </div>
</template>

<script>
export default {
  name: "AdminTerritoryActivity",
  props: {
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
