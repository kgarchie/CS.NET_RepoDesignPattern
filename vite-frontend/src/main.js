import { createApp } from 'vue'

import axios from 'axios'
import router from '/router'

import './assets/css/main.css'

import App from './App.vue'

axios.defaults.baseURL = 'http://localhost:5000/api'


const app = createApp(App)

app.use(router)

app.mount('#app')
