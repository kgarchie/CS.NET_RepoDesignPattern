import {createRouter, createWebHistory} from 'vue-router'
import HomeView from "../src/components/HomeView.vue";

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: '/',
            name: 'Home',
            component: HomeView
        },
        {
            path: '/buy-airtime',
            name: 'BuyAirtime',
            component: () => import('../src/components/BuyAirtime.vue')
        },
        {
            path: '/send-money',
            name: 'SendMoney',
            component: () => import('../src/components/SendMoney.vue')
        },
        {
            path: '/recharge-account',
            name: 'RechargeAccount',
            component: () => import('../src/components/RechargeAccount.vue')
        },
        {
            path: '/recent-transactions',
            name: 'RecentTransactions',
            component: () => import('../src/components/RecentTransactions.vue')
        }
    ]
})

export default router