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
                url: '/SysManager/SysModules/GetListViewModel', //请求后台的URL（*）
                queryParams: t.queryParams, //传递参数（*）
                rowStyle: function (row, index) {
                    
                    if (row.PurviewSum == 0) {
                        console.log(row.PurviewSum);
                        return { classes: 'text-red' };
                    } else
                        return "";
                },
                columns: [
                    {
                        checkbox: true
                    }, {
                        field: 'ModuleName',
                        title: '模块名称'
                        
                    }, {
                        field: 'AreaName',
                        title: '区名称'
                    }, {
                        field: 'ControllerName',
                        title: '控制'

                    }, {
                        field: 'ActionName',
                        title: '操作'

                    }, {
                        field: 'IsExpand',
                        title: '是否展开'

                    }, {

                        title: '操作',
                        formatter: function (value, row, index) {
                            var str = "<a href='Edit/" + row.Id + "' class='btn btn-flat btn-xs btn-white'><i class='text-green glyphicon glyphicon-pencil'></i>编辑</a><a data-delete=1 data-id=" + row.Id + "  class='btn btn-flat btn-xs btn-white'><i class='text-red glyphicon glyphicon-remove'></i>删除</a>";
                            if (row.ParentId == "0")
                                str = str + "<a href='ModuleSort/" + row.Id + "' class='btn btn-flat btn-xs bg-olive'><i class='glyphicon glyphicon-pencil'></i>菜单排序</a>";
                            return str;
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

            $.get("/SysModules/Delete/", { Id: id }, function () {
                $("#SysApplicationTable").bootstrapTable('refresh');
            });

            layer.close(index);
        }, function (index, layero) {

            layer.close(index);
        });
    });
    //添加新的模块资源
    $(document).on("click", "#send_express_notice", function() {
        $.get("/SysManager/SysModules/AutoSaveRescoure", function(data) {
            layer.alert("成功");
        });
    });
});
