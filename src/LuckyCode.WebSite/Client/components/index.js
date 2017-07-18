//公用组件库
import Vue from "vue";
import Table from "./table";

const components = {
    Table
};
 
const install = function (Vue, opts = {}) {
    Object.keys(components).forEach((key) => {
        Vue.component("ss" + key, components[key]);
    });

};
if (typeof window !== 'undefined' && window.Vue) {
    install(window.Vue);
}

export default Object.assign(components, { install });