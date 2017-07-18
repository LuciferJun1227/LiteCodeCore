var TableInit = function () {
    var t = new Object();
    t.queryParams = function (params) {
        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            pageSize: params.limit, //页面大小
            pageIndex: (params.offset == 0) ? 1 : (params.offset / params.limit) + 1 //页码
            //username: $("#txt_search_username").val()
        };
        return temp;
    };

    t.Init = function () {
        var tableindex = $('#Sys_DataGrid')
            .dataGrid({
                url: '/SysManager/SysRoles/GetListViewModel', //请求后台的URL（*）
                queryParams: t.queryParams, //传递参数（*）
                columns: [
                    {
                        checkbox: true
                    }, {
                        field: 'RoleName',
                        title: '角色名称'
                    }, {
                        field: 'RoleType',
                        title: '类型'
                    }, {
                        field: 'RoleDescription',
                        title: '描述'

                    }, {
                        field: 'IsDelete',
                        title: '能否删除',
                        formatter: function (value, row, index) {
                            console.log(value);
                            return value == true ? '<i class=\" glyphicon glyphicon-ok\"></i>' : '<i class=\"glyphicon glyphicon-ban-circle\"></i>';
                        }
                    }, {
                        field: 'CreateTime',
                        title: '添加时间',
                        formatter: AppUI.dateTimeFormat
                    }, {

                        title: '操作',
                        formatter: function (value, row, index) {
                            return "<a href='Edit/" + row.Id + "' class='btn btn-flat btn-xs btn-white'><i class='text-green glyphicon glyphicon-pencil'></i>编辑</a><a data-delete=1 data-id=" + row.Id + "  class='btn btn-flat btn-xs btn-white'><i class='text-red glyphicon glyphicon-remove'></i>删除</a><a href='EditPurview/" + row.Id + "' class='btn btn-flat btn-xs btn-white'><i class='text-green glyphicon glyphicon-pencil'></i>编辑权限</a>";
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
            $.get("/SysRoles/Delete/", { Id: id }, function () {
                $("#Sys_DataGrid").bootstrapTable('refresh');
            });

            layer.close(index);
        }, function (index, layero) {
            layer.close(index);
        });
    });
});
