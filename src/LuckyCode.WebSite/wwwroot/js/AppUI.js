;(function(AppUI,$) {
    AppUI.dataGridActionFormat = function (value, row, index) {
        return "<a href='Edit/" + row.Id + "' class='btn btn-white btn-sm'><i class='text-green glyphicon glyphicon-pencil'></i>编辑</a><a data-delete=1 data-id=" + row.Id + "  class='btn btn-white btn-sm'><i class='text-red glyphicon glyphicon-remove'></i>删除</a>";
    }
    //时间格式化
    AppUI.dateTimeFormat=function(value, row, index) {
        var time1 = $.format.date(value,"yyyy-MM-dd");
        return time1;
    }
    AppUI.Sussess = function (data) {
        if (data.success)
            $('#ChangePassword_01').modal('hide');
        else {
            $('#ChangePassword_01').empty();
            $('#ChangePassword_01').html(data);
            $.validator.unobtrusive.parse('form');
        }
    }
    AppUI.ShowModal=function(url) {
        $('#ChangePassword_01').load(url, function (str) {
            $('#ChangePassword_01').modal({ show: true });
            $.validator.unobtrusive.parse('form');
        });
    }
})(window.AppUI = window.AppUI || {},jQuery);

$.fn.dataGrid = function (options) {
    var defaults= {
        method: 'get', //请求方式（*）
        //toolbar: '#usertoolbar', //工具按钮用哪个容器
        striped: true, //是否显示行间隔色
        cache: false, //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
        pagination: true, //是否显示分页（*）
        sortable: false, //是否启用排序
        sortOrder: "asc", //排序方式
        
        sidePagination: "server", //分页方式：client客户端分页，server服务端分页（*）
        pageNumber: 1, //初始化加载第一页，默认第一页
        pageSize: 15, //每页的记录行数（*）
        pageList: [10, 25, 50, 100], //可供选择的每页的行数（*）
        search: false, //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
        // strictSearch: true,
        // showColumns: true, //是否显示所有的列
        // showRefresh: true, //是否显示刷新按钮
        minimumCountColumns: 2, //最少允许的列数
        clickToSelect: true, //是否启用点击选中行
        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
        uniqueId: "Id", //每一行的唯一标识，一般为主键列
        //showToggle: true, //是否显示详细视图和列表视图的切换按钮
        cardView: false, //是否显示详细视图
        detailView: false //是否显示父子表
    }
    var _options = $.extend(defaults, options);
    var $element = $(this);
    return $element.bootstrapTable(_options);
};