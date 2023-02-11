<template>
  <Teleport to="head">
    <title>Recharge Account</title>
  </Teleport>
  <div class="container">
    <h1>Recharge Account</h1>
    <form @submit.prevent="rechargeAccount" class="send_money-form">
      <div>
        <label for="amount">Amount</label>
        <input type="number" id="amount" v-model="money.amount" />
      </div>
      <div>
        <button type="submit">Recharge Account</button>
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
export default {
  name: "RechargeAccount",
  data(){
    return {
      money: {
        amount: ""
      },
      errors:{
      }
    }
  },
  mounted() {
  },
  methods: {
    rechargeAccount(){
      
    }
  }
}
</script>
<style scoped>

</style>