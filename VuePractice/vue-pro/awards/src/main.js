// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
/* jshint esversion: 6 */
import Vue from 'vue'; /* 这里是引入vue文件 */


import VueResorce from 'vue-resource';

import router from './router'; /* 这里是引入vue的路由 */

Vue.use(VueResorce);

Vue.http.options.emulateJSON = true;

/* eslint-disable no-new */
new Vue({
    el: '#app',
    /* 定义作用范围就是index.html里的id为app的范围内 */
    data() {
        return {}
    },
    router
})