<script setup>
import {RouterLink, RouterView} from 'vue-router'
</script>

<template>
  <div id="wrapper">
    <nav class="top-nav">
      <ul class="menu">
        <li class="menu-item">
          <RouterLink to="/">Home</RouterLink>
        </li>
        <li class="menu-item">
          <RouterLink to="/buy-airtime">Buy Airtime</RouterLink>
        </li>
        <li class="menu-item">
          <RouterLink to="/send-money/">Send Money</RouterLink>
        </li>
        <li class="menu-item">
          <RouterLink to="/recharge-account">Recharge Account</RouterLink>
        </li>
        <li class="menu-item">
          <RouterLink to="/recent-transactions">Recent Transactions</RouterLink>
        </li>
      </ul>
    </nav>
    <section class="main">
      <div class="messages">
        <div v-if="messages">
          <p>{{ messages[messages.length - 1] }}</p>
        </div>
      </div>
      <RouterView/>
    </section>
    <footer>
    </footer>
  </div>
</template>

<script>
export default {
  name: "App",
  data() {
    return {
      messages: []
    }
  },
  methods: {
    getMessages() {
      if (localStorage.getItem("messages")) {
        this.messages = JSON.parse(localStorage.getItem("messages"));
      } else {
        console.log("no messages")
      }
    }
  },
  mounted() {
    this.getMessages();
  },
  watch: {
    messages: function () {
      console.log("messages changed");
    }
  }
}

window.onbeforeunload = function () {
  if (localStorage.getItem("messages")) {
    localStorage.removeItem("messages");
  }
}

window.onload = function () {
  if (localStorage.getItem("messages")) {
    localStorage.removeItem("messages");
  }
}
</script>

<style scoped>
nav ul {
  color: white;
}
</style>
