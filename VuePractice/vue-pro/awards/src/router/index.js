/* jshint esversion: 6 */
import Vue from 'vue'; /* 引用vue文件 */
import VueRouter from 'vue-router'; /* 引用vue路由模块，并赋值给变量Router */
// import HelloWorld from '@/components/HelloWorld'; /* 英文HelloWorld.vue模版，并赋值给变量Hello,这里是“@”相当于“../” */
// import Home from '../pages/home';
// import Detail from '../pages/goodsDetail'
// import Msg from '../components/Message.vue'

Vue.use(VueRouter); /* 使用路由 */

export default new VueRouter({
    routes: [{
        path: '/',
        component: resolve => require(['../pages/home'], resolve),
        // component: resolve => require(['../pages/testcom'], resolve),
        meta: {
            title: 'home'
        }
    }]
});