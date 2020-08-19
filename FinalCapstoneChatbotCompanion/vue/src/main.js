import Vue from 'vue'
import VueChatScroll from 'vue-chat-scroll'
import App from './App.vue'
import router from './router/index'
import store from './store/index'
import axios from 'axios'

Vue.use(VueChatScroll)

Vue.config.productionTip = false

axios.defaults.baseURL = process.env.VUE_APP_REMOTE_API;

new Vue({
  router,
  store,
  VueChatScroll,
  render: h => h(App)
}).$mount('#app')
