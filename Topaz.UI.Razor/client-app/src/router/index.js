import Vue from "vue";
import VueRouter from "vue-router";
import PublisherStreetTerritories from "../views/publisher-street-territories.vue";
import PublisherStreetCheckout from "../views/publisher-street-checkout.vue";
import PublisherInaccessibleTerritories from "../views/publisher-inaccessible-territories.vue";
import PublisherInaccessibleCheckout from "../views/publisher-inaccessible-checkout.vue";
import PublisherInaccessibleAssign from "../views/publisher-inaccessible-assign.vue";

Vue.use(VueRouter);

const parseProps = (r) => ({ id: parseInt(r.params.id) });

const routes = [
  {
    path: "/Publisher/StreetTerritories",
    name: "PublisherStreetTerritories",
    component: PublisherStreetTerritories,
  },
  {
    path: "/Publisher/StreetTerritories/Checkout",
    name: "PublisherStreetCheckout",
    component: PublisherStreetCheckout,
  },
  {
    path: "/Publisher/InaccessibleTerritories",
    name: "PublisherInaccessibleTerritories",
    component: PublisherInaccessibleTerritories,
  },
  {
    path: "/Publisher/InaccessibleTerritories/Checkout",
    name: "PublisherInaccessibleCheckout",
    component: PublisherInaccessibleCheckout,
  },
  {
    path: "/Publisher/InaccessibleTerritories/Assign/:id",
    name: "PublisherInaccessibleAssign",
    props: parseProps,
    component: PublisherInaccessibleAssign,
  },
  // {
  //   path: '/about',
  //   name: 'About',
  //   // route level code-splitting
  //   // this generates a separate chunk (about.[hash].js) for this route
  //   // which is lazy-loaded when the route is visited.
  //   component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  // }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

export default router;
