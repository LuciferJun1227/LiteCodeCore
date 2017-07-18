// var moment = require('moment');

Vue.component('uploadFile', {
    template: '<span><button type="button" class="btn btn-default" v-on:click="selectFile">{{buttonTitle}}</button><input type="file" style="display:none" v-on:change="fileChange"/></span>',
    data: function () {
        return {
            buttonTitle: '上传文件'
        }
    },
    methods: {
        selectFile: function () {
            var fileInput = this.$el.querySelector('input');
            fileInput.click();
        },
        fileChange: function () {
            var that = this;
            var fileInput = this.$el.querySelector('input');
            var file = fileInput.files[0];
            if (file) {
                that.buttonTitle = '正在上传';
                var formBody = new FormData();
                formBody.append('file', file);
                formBody.append('name', file.name);
                Vue.http.post('/sysmanager/home/ProcessUpload', formBody, {
                    headers: {
                        'Content-Type': 'multipart/form-data'
                    }
                }).then(function (res) {
                    console.log(res.body);
                    that.buttonTitle = "上传成功";
                    setTimeout(function () {
                        that.buttonTitle = '上传文件';
                    }, 1500)
                    that.$emit('uploaded', res.body);
                }, function (res) {
                    console.log('upload file error');
                    that.buttonTitle = "上传失败";
                    setTimeout(function () {
                        that.buttonTitle = '上传文件';
                    }, 1500);
                });
            }
        }
    }
});

Vue.component('switchCheckbox', {
    template:
    '<div v-on:click="onClick" style="width:80px;height:30px" >' +
    '<div v-bind:style="!value?uncheckedStyleLeft:checkedStyleLeft" style="width:40px;height:30px;float:left;text-align: center; padding-top: 4px;">{{value?"":"否"}}</div>' +
    '<div v-bind:style="!value?uncheckedStyleRight:checkedStyleRight" style="width:40px;height:30px;float:right;text-align: center; padding-top: 4px;">{{value?"是":""}}</div>' +
    '</div>',
    props: ['value'],

    data: function () {
        return {
            uncheckedStyleLeft: {
                backgroundColor: '#fff',
                border: '1px solid #ddd'
            },
            checkedStyleLeft: {
                backgroundColor: '#00a65a',
                border: '1px solid #00a65a'
            },
            uncheckedStyleRight: {
                backgroundColor: '#ddd',
                border: '1px solid #ddd'
            },
            checkedStyleRight: {
                backgroundColor: '#fff',
                border: '1px solid #00a65a'
            }
        };
    },
    methods: {
        onClick: function () {
            this.value = !this.value;
            this.$emit('input', this.value);
        },
        updateValue: function () {
            console.log('switchCheckbox value change', this.value);
        }
    }
});

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

var vm = new Vue({
    el: '#bannerApp',
    
    data: {
        banners: [],
        totalItems: 0,
        currentPageIndex: 1,
        pageSize: 10,
        totalRowCount: 0,
        editOrAddObj: {}
    },
    created: function () {
        console.log('newsbanner page created .');

        this.loadBanners();

    },
    filters: {
        dateFormat: function (value, format) {
            if (value) {
                return moment(value).format(format)
            }
        }
    },
    methods: {
        loadBanners: function () {
            var that = this;

            //get data
            Vue.http.get('GetListViewModel', {
                params: {
                    pageIndex: that.currentPageIndex,
                    pageSize: that.pageSize,
                    _: Date.parse(new Date())
                }
            }).then(
                function (res) {
                    console.log(res.body);
                    that.banners = res.body.rows;
                    that.totalRowCount = res.body.total;
                }
                , function (res) {
                    console.log('get data error');
                });
        },
        deleteBanner: function (bannerId) {
            var that = this;

            if (bannerId) {
                layer.confirm('你确定要删除？', {
                    btn: ['确定', '取消'] //按钮
                }, function (index, layero) {

                    Vue.http.get('Delete/id' + bannerId).then(
                        function (res) {
                            if (res.body) {
                                for (var index in that.banners) {
                                    var banner = that.banners[index];
                                    if (bannerId === banner.Id) {
                                        that.banners.splice(index, 1);
                                    }
                                }
                            }
                            layer.close(index);
                        },
                        function (res) {
                            console.log('delete error.');
                        }
                    );

                }, function (index, layero) {
                    layer.close(index);
                });
            }
        },
        pageChange: function (pageIndex) {
            this.currentPageIndex = pageIndex;
            this.loadBanners();
        },
        switchCheckboxChange: function (checked) {
            this.editOrAddObj.IsDeleted = checked;
        },
        editBannerModal: function (banner) {
            this.editOrAddObj = banner;
            $('#bannerEdit').modal('show');
        },
        addBannerModal: function () {
            this.editOrAddObj = {
                Title: '',
                Url: '',
                ImageUrl: '',
                Sort: '',
                IsDeleted: false
            };
            $('#bannerEdit').modal('show');
        },
        valid: function () {
            var invalid = {};
            invalid.Sort = this.editOrAddObj.Sort === undefined || this.editOrAddObj.Sort === '';

            this.$set(this.editOrAddObj, 'invalid', invalid);

            for (var prop in invalid) {
                var val = invalid[prop];
                if (val) {
                    return false;
                }
            }

            return true;
        },
        saveBanner: function () {
            var val = this.valid();
            if (!val) {
                return;
            }

            if (this.editOrAddObj.Id) {
                //edit
                Vue.http.post('Edit', this.editOrAddObj).then(
                    function (res) {
                        if (res.body) {
                            layer.alert('编辑Banner成功！');
                            $('#bannerEdit').modal('hide');
                        }
                        else {
                            console.log('edit fail.');
                        }
                    },
                    function (res) {
                        console.log('edit error.');
                    }
                );
            }
            else {
                //add
                Vue.http.post('Create', this.editOrAddObj).then(
                    function (res) {
                        if (res.body) {
                            layer.alert('添加Banner成功！');
                            $('#bannerEdit').modal('hide');
                        }
                        else {
                            console.log('create fail.');
                        }
                    },
                    function (res) {
                        console.log('create error.');
                    }
                );
            }
        },
        fileUploaded: function (result) {
            if (result.success) {
                this.editOrAddObj.ImageUrl = result.Url;
            }
        }
    }

}
)