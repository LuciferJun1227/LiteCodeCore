//系统菜单配置
export default [{
        title: "首页",
        path: "home",
        icon: "fa fa-home"
    },
    {
        title: "系统管理",
        icon: "fa fa-asterisk",
        children: [{
            title: "资讯管理",
            path: "news",
            icon: "fa fa-list"
        }]
    }
];