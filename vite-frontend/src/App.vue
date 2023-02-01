<script setup>
import {RouterLink, RouterView} from 'vue-router'
</script>

<template>
  <div id="wrapper">
    <nav class="top-nav">
      <ul class="menu">
        <li class="menu-item"><RouterLink to="/">Home</RouterLink></li>
        <li class="menu-item"><RouterLink to="/buy-airtime">Buy Airtime</RouterLink></li>
        <li class="menu-item"><RouterLink to="/send-money/">Send Money</RouterLink></li>
        <li class="menu-item"><RouterLink to="/recharge-account">Recharge Account</RouterLink></li>
        <li class="menu-item"><RouterLink to="/recent-transactions">Recent Transactions</RouterLink></li>
      </ul>
    </nav>
    <section class="main">
      <RouterView />
    </section>
    <footer>
    </footer>
  </div>
</template>

<style scoped>
  nav ul{
    color: white;
  }
</style>
