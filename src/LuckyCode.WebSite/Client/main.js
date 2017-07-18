import Vue from 'vue'
import VueRouter from 'vue-router'
import VueAxios from 'vue-axios'
import axios from 'axios'
import App from './app.vue';
import routes from './routes'
import store from './vuex/store'
import shared from './views/shared'
import components from './components'

//批量注册组件

Object.keys(shared).forEach((key) => {
    var name = key.replace(/(\w)/, (v) => v.toUpperCase()); //首字母大写
    Vue.component(`ss${name}`, shared[key]);
});

Vue.use(VueRouter);
Vue.use(VueAxios, axios);
Vue.use(components);

let router = new VueRouter({
    mode: "history",
    routes
});


var vm = new Vue({
    store,
    router,
    render: h => h(App)
});
vm.$mount('#app');

export default router