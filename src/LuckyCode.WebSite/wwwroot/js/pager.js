Vue.component('pagebar', {
    template: '<div>显示第 {{startRow}} 到第 {{endRow}} 条记录，总共 {{totalRowCount}} 条记录 ' +
        '<a href="javascript:;" v-on:click="firstPage" v-show="currentPageIndex>1">首页</a> ' +
        '<a href="javascript:;" v-on:click="prePage" v-show="currentPageIndex>1">上一页</a> ' +
        '<a href="javascript:;" v-on:click="nextPage" v-show="totalRowCount>endRow">下一页</a> ' +
        '<a href="javascript:;" v-on:click="lastPage" v-show="totalRowCount>endRow">末页</a> ' +
        '</div>',
    props: ['currentPageIndex', 'totalRowCount', 'pageSize'],
    data: function () {
        return {
        }
    },
    computed: {
        startRow: function () {
            var currentPageIndex = this.currentPageIndex;
            var pageSize = this.pageSize;

            return ((currentPageIndex - 1) * pageSize) + 1;
        },
        endRow: function () {
            var currentPageIndex = this.currentPageIndex;
            var pageSize = this.pageSize;
            var totalRowCount = this.totalRowCount;

            var endRow = (currentPageIndex + 0) * pageSize;
            if (endRow > totalRowCount) {
                endRow = totalRowCount;
            }

            return endRow;
        },
    },
    methods: {
        prePage: function () {
            var preIndex = this.currentPageIndex - 1;
            if (preIndex < 1) {
                return;
            }
            this.$emit('paged', preIndex);
            console.log('go to prePage', preIndex);
        },
        nextPage: function () {
            var currentPageIndex = this.currentPageIndex;
            var pageSize = this.pageSize;
            var totalRowCount = this.totalRowCount;
            var totalPage = Math.ceil(totalRowCount / pageSize);
            var nextPage = currentPageIndex + 1;
            if (nextPage > totalPage) {
                return;
            }
            this.$emit('paged', nextPage);
            console.log('go to nextPage', nextPage);
        },
        firstPage: function () {
            this.$emit('paged', 1);
        },
        lastPage: function () {
            var pageSize = this.pageSize;
            var totalRowCount = this.totalRowCount;
            var totalPage = Math.ceil(totalRowCount / pageSize);
            this.$emit('paged', totalPage);
        }
    }

});