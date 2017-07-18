<template>
    <td ref="cell"></td>
</template>
<script>
    import Vue from "vue";
    import * as utils from "../../common/utils";
    export default {
        name: "TableCell",
        props: {
            column: Object,
            row: Object,
            counter: {
                type: Number
            }
        },
        data() {
            return {
                context: this.$parent.currentContext
            };
        },
        methods: {
            compile() {
                const $parent = this.context;
                if (this.column.template) {
                    let methods = {};
                    const cell = document.createElement('div');
                    Object.keys($parent).forEach(key => {
                        const func = $parent[key];
                        if (typeof (func) === 'function' && (func.name === 'boundFn' || func.name === 'n')) {
                            methods[key] = func;
                        }
                    });
                    this.$el.innerHTML = '';
                    cell.innerHTML = this.column.template;
                    const res = Vue.compile(cell.outerHTML);
                    const component = new Vue({
                        render: res.render,
                        staticRenderFns: res.staticRenderFns,
                        methods: methods,
                        data() {
                            return $parent._data;
                        }
                    });
                    component.row = this.row;
                    component.column = this.column;
                    this.$refs.cell.appendChild(component.$mount().$el);
                } else {
                    this.$refs.cell.innerHTML = this.row[this.column.name];
                }
            },
            destroy() {

            }
        }, beforeDestroy() {
            this.destroy();
        },
        watch: {
            counter(curVal, oldVal) {
                this.destroy();
                this.compile();

            }
        },
        mounted() {
            this.$nextTick(() => {
                this.compile();
            });
        },

    }
</script>
