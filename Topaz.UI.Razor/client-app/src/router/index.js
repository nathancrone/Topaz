import Vue from "vue";
import VueRouter from "vue-router";

Vue.use(VueRouter);

const parseProps = (r) => ({ id: parseInt(r.params.id) });

const routes = [
  {
    path: "/Publisher/StreetTerritories",
    name: "PublisherStreetTerritories",
    component: () =>
      import(
        /* webpackChunkName: "chunk-publisher" */ "../views/publisher-street-territories.vue"
      ),
  },
  {
    path: "/Publisher/StreetTerritories/Checkout",
    name: "PublisherStreetCheckout",
    component: () =>
      import(
        /* webpackChunkName: "chunk-publisher" */ "../views/publisher-street-checkout.vue"
      ),
  },
  {
    path: "/Publisher/BusinessTerritories",
    name: "PublisherBusinessTerritories",
    component: () =>
      import(
        /* webpackChunkName: "chunk-publisher" */ "../views/publisher-business-territories.vue"
      ),
  },
  {
    path: "/Publisher/BusinessTerritories/Checkout",
    name: "PublisherBusinessCheckout",
    component: () =>
      import(
        /* webpackChunkName: "chunk-publisher" */ "../views/publisher-business-checkout.vue"
      ),
  },
  {
    path: "/Publisher/InaccessibleTerritories",
    name: "PublisherInaccessibleTerritories",
    component: () =>
      import(
        /* webpackChunkName: "chunk-publisher" */ "../views/publisher-inaccessible-territories.vue"
      ),
  },
  {
    path: "/Publisher/InaccessibleTerritories/Checkout",
    name: "PublisherInaccessibleCheckout",
    component: () =>
      import(
        /* webpackChunkName: "chunk-publisher" */ "../views/publisher-inaccessible-checkout.vue"
      ),
  },
  {
    path: "/Publisher/InaccessibleTerritories/Assign/:id",
    name: "PublisherInaccessibleAssign",
    props: parseProps,
    component: () =>
      import(
        /* webpackChunkName: "chunk-publisher" */ "../views/publisher-inaccessible-assign.vue"
      ),
  },
  {
    path: "/Publisher/InaccessibleAssignments",
    name: "PublisherInaccessibleAssignments",
    component: () =>
      import(
        /* webpackChunkName: "chunk-publisher" */ "../views/publisher-inaccessible-assignments.vue"
      ),
  },
  {
    path: "/Admin/InaccessibleTerritories/ContactsImport/:id",
    name: "AdminContactsImport",
    props: parseProps,
    component: () =>
      import(
        /* webpackChunkName: "chunk-admin" */ "../views/admin-contacts-import.vue"
      ),
  },
  {
    path: "/Admin/Territory/:type",
    name: "AdminTerritory",
    props: true,
    component: () =>
      import(
        /* webpackChunkName: "chunk-admin" */ "../views/admin-territory.vue"
      ),
  },
  {
    path: "/Admin/Territory/:type/:id/activity",
    name: "AdminTerritoryActivity",
    props: true,
    component: () =>
      import(
        /* webpackChunkName: "chunk-admin" */ "../views/admin-territory-activity.vue"
      ),
  },
];

const router = new VueRouter({
  mode: "history",
  //base: process.env.BASE_URL,
  // i don't want the public path in the route so i explicitely set the router's base path to root
  base: "/",
  routes,
});

export default router;
