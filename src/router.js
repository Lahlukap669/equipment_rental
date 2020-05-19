import Vue from 'vue';
import Router from 'vue-router';
import Login from './components/Login.vue';
import Info from './components/Info.vue';
import Home from './components/Home.vue';

Vue.use(Router);

const router = new Router({
    mode: 'history',
    routes: [
        { path: '/login', component: Login },
        { path: '/info', component: Info, meta: { requiresAuth: true }},
        { path: '/', component: Home, meta: { requiresAuth: true } },
        { path: '*', redirect: '/login' }
    ]
});

router.beforeEach((to, from, next) => {
    if (to.matched.some(recored => recored.meta.requiresAuth)) {
        if (localStorage.getItem('id')) {
            next();
            return;
        } else {
            next('/login');
        }
    } else {
        next();
    }
})

export default router;