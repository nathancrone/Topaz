<template>
  <div v-if="territory">
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
    <div class="row no-gutters">
      <div class="col">
        <h3>Activity - {{ territory.territoryCode }}</h3>
        <table class="table table-sm">
          <thead>
            <tr>
              <th scope="col">Publisher</th>
              <th scope="col">Checked Out</th>
              <th scope="col">Checked In</th>
              <th scope="col">&nbsp;</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="a in territory.activity" :key="a.territoryActivityId">
              <td>{{ a.publisher.firstName }} {{ a.publisher.lastName }}</td>
              <td>{{ displayDate(a.checkOutDate) }}</td>
              <td v-if="a.checkInDate">{{ displayDate(a.checkInDate) }}</td>
              <td v-else>-</td>
              <td class="text-right">
                <a
                  class="btn btn-sm btn-primary mr-1"
                  href="#"
                  @click.prevent="activityEdit(a)"
                  >edit</a
                >
                <a class="btn btn-sm btn-primary mr-1" href="#">delete</a>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <AdminTerritoryActivityModal
      @close="isActivityModalOpen = false"
      @confirm="activityEditConfirm"
      :open="isActivityModalOpen"
    />
  </div>
</template>

<script>
import { data } from "../shared";
import { parseISO, format } from "date-fns";
import AdminTerritoryActivityModal from "../components/admin-territory-activity-modal";
export default {
  name: "AdminTerritoryActivity",
  props: {
    type: {
      type: String,
      required: true,
    },
    id: {
      type: Number,
      default: 0,
    },
  },
  data() {
    return {
      territory: null,
      isActivityModalOpen: false,
    };
  },
  components: {
    AdminTerritoryActivityModal,
  },
  methods: {
    async loadActivity() {
      this.territory = null;
      if (this.type === "street") {
        this.territory = await data.getStreetActivity(this.id);
      } else if (this.type === "inaccessible") {
        this.territory = await data.getInaccessibleActivity(this.id);
      }
    },
    displayDate(date) {
      return format(parseISO(date), "MMM dd, yyyy");
    },
    activityEdit() {
      this.isActivityModalOpen = true;
    },
    async activityEditConfirm() {
      this.isActivityModalOpen = false;
    },
  },
  async created() {
    await this.loadActivity();
  },
};
</script>
