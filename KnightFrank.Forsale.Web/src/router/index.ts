import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import Login from '../views/Login.vue'
import PropertyList from '../views/Admin/PropertyList.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/propertyDetails/:id',
      name: 'propertyDetails',
      component: () => import('../views/PropertyDetails.vue'),
    },
    {
      path: '/login',
      name: 'login',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../views/Login.vue'),
    },
    {
      path: '/admin/propertyList',
      name: 'propertyList',
      component: () => import('../views/Admin/PropertyList.vue'),
    }
  ],
})

export default router
