<template>
    <div :id="tableId" class="ss-table ss-table-fixed">
        <div class="ss-table-header">
            <table class="table table-bordered">
                <colgroup>
                    <col v-for="column in columns" :width="column.width||'auto'" />
                </colgroup>
                <thead>
                    <tr>
                        <th v-for="column in columns" v-text="column.title"></th>
                    </tr>
                </thead>
            </table>
        </div>
        <div class="ss-table-content">
            <table class="table table-bordered table-striped table-hover">
                <colgroup>
                    <col v-for="column in columns" :width="column.width||'auto'" />
                </colgroup>
                <tbody>
                    <tr v-for="(row,index) in currentRows">
                        <table-cell v-for="column in columns" :column="column" :row="row" :counter="counter" />
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="ss-table-paging">
            <!--todo:分页需要提取-->
            <div :id="paginationId" class="simple-pagination"></div>
        </div>
    </div>
</template>
<script>
    import Vue from "vue";
    import { vNodesToHtml, newGuid } from "../../common/utils";
    import TableCell from "./TableCell";
    import "./simplePagination.js";

    export default {
        name: "Table",
        data() {
            return {
                currentContext: this.context,
                currentPageSize: this.pageSize,
                currentPageIndex: this.pageIndex,
                currentRows: this.rows,
                counter: 0
            };
        },
        computed: {
            tableId: function () {
                return this.id || ("table-" + newGuid());
            },

            paginationId: function () {
                return this.tableId + "-pagination";
            },
            pageUrl: function () {
                return `${this.url}/pageindex/${this.currentPageIndex}/pagesize/${this.currentPageSize}`;
            }
        },
        props: {
            id: String,
            url: {
                type: String
            },
            pageSize: {
                //type: Number,
                default: 20
            },
            pageIndex: {
                //type: Number,
                default: 0
            },
            columns: {
                type: Array,
                require: true,
                default: function () {
                    return [];
                }
            },
            rows: {
                type: Array,
                default: function () {
                    return [];
                }
            },
            context: {
                type: Object
            }

        },
        methods: {
            load: function () {
                if (!this.url) {
                    console.warn("未指定table组件url")
                    return;
                }
                var $pagination = $(`#${this.paginationId}`);
                $pagination.pagination({
                    items: 0,
                    itemsOnPage: this.currentPageSize,
                    currentPage: this.currentPageIndex,
                    cssStyle: 'light-theme',
                    onPageClick: function (pageNumber) {
                        that.currentPageIndex = pageNumber - 1;
                        send();
                    }
                });
                var that = this;
                function send() {
                    that.$http.get(`${that.pageUrl}`)
                        .then(response => {
                            var data = response.data;
                            that.currentRows = data.items;
                            $pagination.pagination("updateItems", data.total || 0);
                            that.updateCounter();
                        });
                }
                send();
            }
            ,
            updateCounter() {
                this.counter++;
            }
        },

        created() {
            if (!this.context)
                this.currentContext = this.$parent;
            var that = this;
            var $slots = this.$slots.default;
            $slots.forEach(function (slot) {
                if (slot.tag === "ss-col") {
                    let column = {};
                    if (slot.data && slot.data.attrs) {
                        column = slot.data.attrs;
                    }
                    if (slot.staticClass) {
                        column.class = slot.staticClass;
                    }
                    if (slot.children) {
                        column.template = vNodesToHtml(slot.children);
                    }
                    that.columns.push(column);
                }
            });


        },
        watch: {

        },
        mounted() {
            this.load();
        },
        components: {
            TableCell
        }
    }
</script>
<style src="./table.scss" lang="scss"></style>