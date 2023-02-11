<template>
  <Teleport to="head">
    <title>Recent Transactions</title>
  </Teleport>
  <div class="container">
    <h1>Recent Transactions</h1>
    <table class="recent_transactions-table">
      <thead>
      <tr>
        <th>No.</th>
        <th>Id</th>
        <th>Type</th>
        <th>Amount</th>
        <th>From</th>
        <th>To</th>
        <th>Date</th>
        <th>Time</th>
        <th>Status</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="(transaction, index) in transactions" :key="parseInt(transaction.dbTransactionId)">
        <td class="tb_no">{{ index }}</td>
        <td class="tb_sysId">{{ transaction.systemTransactionId }}</td>
        <td class="tb_type">{{ transaction.transactionType }}</td>
        <td class="tb_amount">{{ transaction.transactionAmount }}</td>
        <td class="tb_from">{{ transaction.fromUserId }}</td>
        <td class="tb_to">{{ transaction.toUserId }}</td>
        <td class="tb_date">{{ transaction.transactionDate.split("T")[0] }}</td>
        <td class="tb_time">{{ transaction.transactionDate.split("T")[1].split(":")[0] + ":" + transaction.transactionDate.split("T")[1].split(":")[1] }}</td>
        <td class="tb_status">{{ transaction.transactionStatus }}</td>
      </tr>
      </tbody>
    </table>
  </div>
</template>
<script>
import axios from "axios";

export default {
  name: "RecentTransactions",
  data() {
    return {
      transactions: [],
      messages: [],
      errors: []
    }
  },
  methods: {
    getRecentTransactions() {
      try {
        axios.get('v1/transactions/recent-transactions').then(response => {
          this.transactions = response.data;
        }).catch(error => {
          console.log(error);
        }).finally(() => {
          this.messages.push("Recent transactions retrieved");
          localStorage.setItem("messages", JSON.stringify(this.messages));
        });
      } catch (error) {
        console.log(error);
      }
    }
  },
  mounted() {
    this.getRecentTransactions();
  }
}
</script>
<style scoped>
</style>