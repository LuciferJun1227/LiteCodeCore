import * as utils from "./utils";
/**
  * 弹出窗口
  * @returns {} 
  */
export function show(options) {
    options = window.jQuery.extend({
        //popupId: null,
        title: document.title || "window",
        href: null,
        params: null,
        width: utils.defaultUnit(options.width, "90%"),
        height: utils.defaultUnit(options.height, "90%")
    }, options);
    if (!options.href) {
        throw new Error("请指定弹窗的链接属性");
    }
    var popupId = utils.newGuid();
    //追加id参数到链接中
    options.href = utils.appendParam("popupId", popupId, options.href);

    //追加自定义参数到当前链接中
    options.href = utils.appendParamsFromExp(options.href, options.params);

    var container = window;

    var index = container.layer.open({
        type: 2,
        title: options.title,
        shadeClose: false,
        shade: [0.5, '#808080'],
        maxmin: true,
        area: [options.width, options.height],
        content: options.href,
        end: closed,
        resize: false
    });

    function closed() {


    }

}
