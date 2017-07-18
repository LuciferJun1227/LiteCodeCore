import axios from 'axios'
//这里封装一些表格常用的api ...
export default {
    editRow(options) {

    },
    deleteRow(options) {
        return new Promise(function (resolve, reject) {
            if (options.confirm) {
                window.layer.confirm(options.confirm, { icon: 3, title: "确认删除" }, function (index) {
                    window.layer.msg("删除" + options.params);

                    window.layer.close(index);
                });
            }
          
        });


    }
}