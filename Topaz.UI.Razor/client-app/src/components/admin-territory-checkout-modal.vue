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
                <h5 v-else scope="row">
                  Check Out {{ territory.territoryCode }}
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
                      v-model="publisherSearch"
                      autocomplete="off"
                      placeholder="enter publisher"
                      list="availablePublisherList"
                    />
                    <datalist id="availablePublisherList">
                      <option v-for="(match, i) in publisherMatches" :key="i">{{
                        match.name
                      }}</option>
                    </datalist>
                  </div>
                  <div class="form-group">
                    <label class="control-label">Date</label>
                    <input
                      class="form-control"
                      type="datetime-local"
                      v-model="checkoutDate"
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
                  :class="{
                    disabled: !checkoutDate || !selectedPublisher,
                  }"
                  @click.prevent="confirm"
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
import { data } from "../shared";
import { differenceInDays, parseISO, format } from "date-fns";
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
      checkoutDate: null,
      publisherSearch: "",
      publisherSearchToken: undefined,
      availablePublishers: [],
      selectedPublisher: undefined,
    };
  },
  methods: {
    close() {
      this.$emit("close");
    },
    confirm() {
      let checkoutDate = parseISO(this.checkoutDate);

      if (checkoutDate) {
        // difference less than 1 day
        if (differenceInDays(new Date(), checkoutDate) < 1) {
          checkoutDate = null;
        }
      }

      this.$emit("confirm", {
        territoryId: this.territory.territoryId,
        publisherId: this.selectedPublisher.id,
        checkoutDate: checkoutDate,
      });
      this.publisherSearch = "";
      this.publisherSearchToken = undefined;
      this.availablePublishers = [];
      this.selectedPublisher = undefined;
    },
    async loadPublishers(token) {
      const publishers = await data.getPublisherSelectOptions(token);
      this.availablePublishers = [...publishers];
    },
  },
  watch: {
    open: {
      immediate: true,
      handler(val) {
        if (val) {
          this.checkoutDate = format(new Date(), "yyyy-MM-dd'T'hh:mm");
        }
      },
    },
    async publisherSearch(after) {
      // find available publisher that exactly matches
      const publisher = this.availablePublishers.find(
        ({ name }) => name.toLowerCase() === after.toLowerCase()
      );

      // if exact match found, set the selected publisher
      // if not, clear the selected publisher
      if (publisher) {
        this.selectedPublisher = Object.assign({}, publisher);
      } else {
        this.selectedPublisher = undefined;
      }

      if (after.length < 3 || this.selectedPublisher) {
        return;
      }
      if (
        this.publisherSearchToken &&
        after.indexOf(this.publisherSearchToken) !== -1
      ) {
        return;
      }
      this.publisherSearchToken = after;
      await this.loadPublishers(after);
    },
  },
  computed: {
    publisherMatches() {
      return this.availablePublishers.filter((p) => {
        return (
          p.name.toLowerCase().indexOf(this.publisherSearch.toLowerCase()) >= 0
        );
      });
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
