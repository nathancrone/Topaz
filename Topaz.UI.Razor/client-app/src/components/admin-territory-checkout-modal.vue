<template>
  <div v-if="open">
    <transition name="modal">
      <div class="modal-mask">
        <div class="modal-wrapper">
          <div class="modal-dialog" role="document">
            <div class="modal-content" v-if="territory">
              <div class="modal-header">
                <h5 v-if="territory.streetTerritoryCode">
                  Check Out {{ territory.streetTerritoryCode }} /
                  {{ territory.territoryCode }}
                </h5>
                <h5 v-else scope="row">Check Out {{ t.territoryCode }}</h5>
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
                    <select class="form-control">
                      <option value="">-- Select Publisher --</option>
                      <option value="38">Alexander, Carol</option>
                    </select>
                  </div>
                  <div class="form-group">
                    <label class="control-label">Date</label>
                    <input
                      class="form-control"
                      type="datetime-local"
                      v-model="checkOutDate"
                    />
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
                  @click.prevent="save"
                >
                  Check Out
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
import { format } from "date-fns";
export default {
  name: "AdminTerritoryCheckoutModal",
  props: {
    territory: {
      type: Object,
      default: null,
    },
    open: {
      type: Boolean,
      default: () => false,
    },
  },
  data() {
    return {
      checkOutDate: null,
    };
  },
  methods: {
    close() {
      this.$emit("close");
    },
    save() {
      this.$emit("close");
    },
  },
  watch: {
    open: {
      immediate: true,
      handler(val) {
        if (val) {
          this.checkOutDate = format(new Date(), "yyyy-MM-dd'T'hh:mm");
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
