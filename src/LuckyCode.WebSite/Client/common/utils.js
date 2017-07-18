//工具类集合
export function vNodeToHtml(vNode) {
    return vNodesToHtml([vNode]);
}
/**
 * 将vue 虚拟dom(VNode)集合转换为对应的html
 * @param {any} vNodes 虚拟dom(VNode)集合
 */
export function vNodesToHtml(vNodes) {
    let result = [];
    function _vNodeToHtml(vNode) {
        if (!vNode)
            return;
        if (vNode.tag) {
            result.push(`<${vNode.tag}`);
            var data = vNode.data;
            var attrs = data.attrs;
            if (data.staticClass) {
                result.push(` class='${data.staticClass}'`);
            }
            if (data.staticStyle) {
                var styles = [];
                for (var styleName in data.staticStyle) {
                    if (data.staticStyle.hasOwnProperty(styleName)) {
                        styles.push(`${styleName}=${data.staticStyle[styleName]}`);
                    }
                }
                result.push(` style='${styles.join(";")}'`);
            }
            if (attrs) {
                for (let name in attrs) {
                    if (attrs.hasOwnProperty(name)) {
                        result.push(` ${name}='${attrs[name]}'`);
                    }
                }
            }
            result.push(' />');
        } else {
            if (vNode.text) {
                result.push(vNode.text);
            }
        }
        if (vNode.children && vNode.children.length > 0) {
            vNode.children.forEach(function (child) {
                _vNodeToHtml(child);
            });
        }
        if (vNode.tag) {
            result.push(`</${vNode.tag}>`);
        }
    }

    vNodes.forEach(function (node) {
        _vNodeToHtml(node);
    });
    var template = result.join('');
    template = template.replace(/\$\{([^\}]+)\}/ig, "{{$1}}");
    template = template.replace(/\$\:/ig, ":");
    template = template.replace(/\$\@/ig, "@");
    return template;
}

/**
 * 将表达式格式 a=2&c=1 或者 a:2,b:3 转换为对应json
 * @param {string} expression 
 * @param {Array} outputParams 输出参数方式,如果不填,则可以获取返回值
 * @returns {Object<>}  [{name:"a",value:2},{name:"c",value:1}]
 */
export function getJSONFromExpression(expression) {
    if (!expression) {
        return {};
    }
    var data = {};

    function processExpression(separator1, separator2) {
        if (expression.indexOf(separator2) === -1) {
            return;
        }
        var firstParts = expression.split(separator1);
        for (var i = 0; i < firstParts.length; i++) {
            var secondParts = firstParts[i].split(separator2);
            if (secondParts.length === 2) {
                data[secondParts[0]] = secondParts[1];
            }
        }
    }

    processExpression("&", "=");

    processExpression(",", ":");

    return data;
}

export function defaultUnit(value, defaultValue) {
    if (value && /^\d+$/.test(value)) {
        return value + (defaultValue || "px");
    }
    return defaultValue;
}

function S4() {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
}

export function newGuid() {
    return (S4() + S4() + S4() + S4() + S4() + S4() + S4() + S4());
}