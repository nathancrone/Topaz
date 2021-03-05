<template>
  <div v-if="open">
    <transition name="modal">
      <div class="modal-mask">
        <div class="modal-wrapper">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 scope="row">
                  Edit Activity
                </h5>
                <button
                  type="button"
                  class="close"
                  data-dismiss="modal"
                  aria-label="Close"
                >
                  <span aria-hidden="true" @click.prevent="close">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <form>
                  <div class="form-group">
                    <label class="control-label">Publisher</label>
                    <input
                      type="text"
                      class="form-control"
                      autocomplete="off"
                      placeholder="enter publisher"
                      list="availablePublisherList"
                    />
                    <datalist id="availablePublisherList"></datalist>
                  </div>
                  <div class="form-group">
                    <label class="control-label">Checkout Date</label>
                    <input class="form-control" type="datetime-local" />
                  </div>
                  <div class="form-group">
                    <label class="control-label">Checkin Date</label>
                    <input class="form-control" type="datetime-local" />
                  </div>
                </form>
              </div>
              <div class="modal-footer">
                <button
                  type="button"
                  class="btn btn-secondary"
                  @click.prevent="close"
                >
                  Close
                </button>
                <button
                  type="button"
                  class="btn btn-primary"
                  :class="{
                    disabled: !checkinDate,
                  }"
                  @click.prevent="confirm"
                >
                  Check In
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </transition>
  </div>
</template>

<script>
//import { differenceInDays, parseISO, format } from "date-fns";
export default {
  name: "AdminTerritoryActivityModal",
  props: {
    open: {
      type: Boolean,
      default: () => false,
    },
  },
  data() {
    return {};
  },
  methods: {
    close() {
      this.$emit("close");
    },
    confirm() {
      this.$emit("confirm", {});
    },
  },
  watch: {
    open: {
      immediate: true,
      handler(val) {
        if (val) {
          //this.checkinDate = format(new Date(), "yyyy-MM-dd'T'hh:mm");
        }
      },
    },
  },
};
</script>

<style scoped>
.modal-mask {
  position: fixed;
  z-index: 9998;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: table;
  transition: opacity 0.3s ease;
}

.modal-wrapper {
  display: table-cell;
  vertical-align: middle;
}
</style>
