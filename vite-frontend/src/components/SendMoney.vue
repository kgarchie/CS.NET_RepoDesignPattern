<template>
  <Teleport to="head">
    <title>Send Money</title>
  </Teleport>
  <div class="container">
    <h1>Send Money</h1>
    <form @submit.prevent="sendMoney" class="send_money-form">
      <div>
        <label for="amount">Amount</label>
        <input type="number" id="amount" v-model="money.amount" />
      </div>
      <div>
        <label for="phone">Phone</label>
        <input type="text" id="phone" v-model="money.phone" />
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
  name: "SendMoney",
  data(){
    return {
      money: {
        amount: "",
        phone: "",

        // For testing only, to be populated dynamically after login
        fromUserId: 1,
        toUserId: 2
      },
      server_response: [],
      errors: [],
      messages: []
    }
  },
  mounted(){},
  methods: {
    async sendMoney(){
      if (this.money.amount === "" || this.money.phone === ""){
        if (this.errors.length > 0){
          this.errors = [];
        }
        this.errors.push("Please fill in all fields");
        return;
      }
      this.money = {
        amount: parseInt(this.money.amount),
        phone: this.money.phone,
      }
      try {
        const response = await axios.post("/v1/transactions/send-money", this.money);
        this.server_response = response.data;
        this.messages.push(this.server_response.message);

        if (this.server_response.statusCode === "success"){
          this.money.amount = "";
          this.money.phone = "";
          alert("Money sent successfully")
        }
      } catch (error) {
        this.errors.push(error);
      }
    }
  }
}
</script>
<style scoped>

</style>