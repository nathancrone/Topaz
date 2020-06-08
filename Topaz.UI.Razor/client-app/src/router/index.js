import Vue from "vue";
import VueRouter from "vue-router";
import PublisherStreetTerritories from "../views/publisher-street-territories.vue";
import PublisherStreetCheckout from "../views/publisher-street-checkout.vue";
import PublisherStreetCheckin from "../views/publisher-street-checkin.vue";
import PublisherStreetRework from "../views/publisher-street-rework.vue";

Vue.use(VueRouter);

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
    path: "/Publisher/StreetTerritories/Checkin/:id",
    name: "PublisherStreetCheckin",
    component: PublisherStreetCheckin,
    props: true,
  },
  {
    path: "/Publisher/StreetTerritories/Rework/:id",
    name: "PublisherStreetRework",
    component: PublisherStreetRework,
    props: true,
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
