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
    <div class="row no-gutters">
      <div class="col">
        <h3>Activity - E-015 / I-1006</h3>
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
            <tr>
              <td>Nathan Crone</td>
              <td>Oct 08, 2020</td>
              <td>Nov 14, 2020</td>
              <td class="text-right">
                <a class="btn btn-sm btn-primary mr-1" href="#">edit</a>
                <a class="btn btn-sm btn-primary mr-1" href="#">delete</a>
              </td>
            </tr>
            <tr>
              <td>Patricia Crone</td>
              <td>Oct 08, 2019</td>
              <td>Nov 14, 2019</td>
              <td class="text-right">
                <a class="btn btn-sm btn-primary mr-1" href="#">edit</a>
                <a class="btn btn-sm btn-primary mr-1" href="#">delete</a>
              </td>
            </tr>
            <tr>
              <td>Dana Crone</td>
              <td>Oct 08, 2018</td>
              <td>Nov 14, 2018</td>
              <td class="text-right">
                <a class="btn btn-sm btn-primary mr-1" href="#">edit</a>
                <a class="btn btn-sm btn-primary mr-1" href="#">delete</a>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script>
import { data } from "../shared";
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
    };
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
  },
  async created() {
    await this.loadActivity();
  },
};
</script>
