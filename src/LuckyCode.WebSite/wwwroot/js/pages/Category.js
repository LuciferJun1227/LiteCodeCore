var TableInit = function () {
    var t = new Object();
    t.queryParams = function (params) {
        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            pageSize: params.limit, //页面大小
            pageIndex: (params.offset == 0) ? 1 : (params.offset / params.limit) + 1, //页码
            //username: $("#txt_search_username").val()
        };
        return temp;
    };

    t.Init = function () {
        var tableindex = $('#Sys_DataGrid')
            .dataGrid({
                url: '/SysManager/Category/GetListViewModel', //请求后台的URL（*）
                queryParams: t.queryParams, //传递参数（*）
                columns: [
                    {
                        checkbox: true
                    }, {
                        field: 'Title',
                        title: '分类名称'
                    }, {
                        field: 'Description',
                        title: '描述'
                    }, {
                        field: 'CreateDate',
                        title: '创建时间',
                        formatter: AppUI.dateTimeFormat
                    }, {

                        title: '操作',
                        formatter: function (value, row, index) {
                            return "<a href='Edit/" + row.CategoryId + "' class='btn btn-flat btn-xs btn-white'><i class='text-green glyphicon glyphicon-pencil'></i>编辑</a><a data-delete=1 data-id=" + row.CategoryId + "  class='btn btn-flat btn-xs btn-white'><i class='text-red glyphicon glyphicon-remove'></i>删除</a>";
                        }
                    }
                ]
            });
        return tableindex;
    }
    return t;
}
$(document).ready(function () {

    var _t = new TableInit();
    var tindex = _t.Init();
    $(document).on("click", "#Sys_DataGrid a[data-delete=1]", function () {

        var id = $(this).data("id");
        layer.confirm('你确定要删除？', {
            btn: ['确定', '取消'] //按钮
        }, function (index, layero) {

            $.get("/SysManager/Category/Delete/", { Id: id }, function () {
                $("#Sys_DataGrid").bootstrapTable('refresh');
            });

            layer.close(index);
        }, function (index, layero) {

            layer.close(index);
        });
    });
});
