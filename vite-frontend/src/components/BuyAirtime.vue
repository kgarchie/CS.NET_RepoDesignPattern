<template>
  <Teleport to="head">
    <title>Buy Airtime</title>
  </Teleport>
  <div class="container">
    <h1>Buy Airtime</h1>
    <div class="messages">
      <ul>
        <li v-for="message in messages" :key="message">{{ message }}</li>
      </ul>
    </div>
    <form @submit.prevent="buyAirtime" class="buy_airtime-form">
      <div>
        <label for="amount">Amount</label>
        <input type="number" id="amount" v-model="airtime.amount" />
      </div>
      <div>
        <label for="phone">Phone</label>
        <input type="text" id="phone" v-model="airtime.phone" />
      </div>
      <div>
        <button type="submit">Buy Airtime</button>
      </div>
      <div class="form-errors">
        <div v-if="errors">
          <ul>
            <li v-for="error in errors" :key="error">{{ error.toString() }}</li>
          </ul>
        </div>
      </div>
    </form>
  </div>
</template>

<script>
import axios from "axios";
export default {
name: "BuyAirtime",
  data() {
  return {
    airtime: {
      amount: "",
      phone: "",
    },
    server_response: [],
    errors: [],
    messages: []
  };
},
  mounted() {},
  methods: {
    async buyAirtime() {
      if (this.airtime.amount === "" || this.airtime.phone === ""){
        this.errors.push("Please fill in all fields");
        return;
      }
      this.airtime = {        
        amount: parseInt(this.airtime.amount),
        phone: this.airtime.phone,
        
        // for testing only to be populated dynamically after login
        fromUserId: 1,
        toUserId: 10
      };
      
      axios.post(
        "v1/transactions/buy-airtime",
        this.airtime
      ).then((response) => {
        this.server_response = response.data;
        if(this.server_response.statusCode === 200){
          alert("Airtime bought successfully");
        }
      }).catch((error) => {
        this.errors = error.response.data.errors;
      });
    },
  },
}
</script>

<style scoped>

</style>