<template>
  <div>
    <div class="row mt-3">
      <ul class="nav nav-tabs flex-column flex-md-row">
        <li class="nav-item">
          <router-link
            tag="a"
            :class="{ 'nav-link': true, active: type === 'street' }"
            :to="{ name: 'AdminTerritory', params: { type: 'street' } }"
            >Street</router-link
          >
        </li>
        <li class="nav-item">
          <router-link
            tag="a"
            :class="{ 'nav-link': true, active: type === 'inaccessible' }"
            :to="{ name: 'AdminTerritory', params: { type: 'inaccessible' } }"
            >Inaccessible</router-link
          >
        </li>
      </ul>
    </div>
    <div class="row mt-3 no-gutters">
      <div class="d-flex flex-wrap col">
        <div class="flex-grow-1 mb-2">
          &nbsp;
        </div>
        <div class="flex-grow-1 flex-md-grow-0 mb-2">
          <div class="btn-group btn-group-sm" role="group" aria-label="filter">
            <button type="button" class="btn btn-secondary active">
              All
            </button>
            <button type="button" class="btn btn-secondary">
              Checked Out
            </button>
            <button type="button" class="btn btn-secondary">
              Checked In
            </button>
          </div>
        </div>
      </div>
    </div>
    <div>
      <div class="row no-gutters">
        <div class="col">
          <h3>Territories</h3>
          <table class="table table-sm">
            <thead>
              <tr>
                <th scope="col">Territory</th>
                <th scope="col">Publisher</th>
                <th scope="col">Checked Out</th>
                <th scope="col">Checked In</th>
                <th scope="col">&nbsp;</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <th scope="row">N-008</th>
                <td>Nathan Crone</td>
                <td>10/10/2020</td>
                <td>11/10/2020</td>
                <td class="text-right">
                  <a
                    class="btn btn-sm btn-primary mr-1"
                    href="#"
                    @click.prevent="isCheckoutModalOpen = true"
                    >check out</a
                  >
                  <router-link
                    tag="a"
                    class="btn btn-sm btn-primary"
                    :to="{
                      name: 'AdminTerritoryActivity',
                      params: { id: 123 },
                    }"
                    >activity</router-link
                  >
                </td>
              </tr>
              <tr>
                <th scope="row">S-021</th>
                <td>Beverly Cofer</td>
                <td>11/10/2020</td>
                <td>-</td>
                <td class="text-right">
                  <a
                    class="btn btn-sm btn-primary mr-1"
                    href="#"
                    @click.prevent="isCheckinModalOpen = true"
                    >check in</a
                  >
                  <router-link
                    tag="a"
                    class="btn btn-sm btn-primary"
                    :to="{
                      name: 'AdminTerritoryActivity',
                      params: { id: 123 },
                    }"
                    >activity</router-link
                  >
                </td>
              </tr>
              <tr>
                <th scope="row">N-010</th>
                <td colspan="3" class="text-center">-</td>
                <td class="text-right">
                  <a
                    class="btn btn-sm btn-primary mr-1"
                    href="#"
                    @click.prevent="isCheckoutModalOpen = true"
                    >check out</a
                  >
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>

    <AdminTerritoryCheckinModal
      @close="isCheckinModalOpen = false"
      :open="isCheckinModalOpen"
    />

    <AdminTerritoryCheckoutModal
      @close="isCheckoutModalOpen = false"
      :open="isCheckoutModalOpen"
    />
  </div>
</template>

<script>
import AdminTerritoryCheckinModal from "../components/admin-territory-checkin-modal";
import AdminTerritoryCheckoutModal from "../components/admin-territory-checkout-modal";

export default {
  name: "AdminTerritory",
  props: {
    type: {
      type: String,
      default: "street",
    },
  },
  data() {
    return {
      isCheckinModalOpen: false,
      isCheckoutModalOpen: false,
    };
  },
  components: {
    AdminTerritoryCheckinModal,
    AdminTerritoryCheckoutModal,
  },
  watch: {
    isCheckinModalOpen: {
      handler(after) {
        if (after) this.isCheckoutModalOpen = !after;
      },
    },
    isCheckoutModalOpen: {
      handler(after) {
        if (after) this.isCheckinModalOpen = !after;
      },
    },
  },
};
</script>
