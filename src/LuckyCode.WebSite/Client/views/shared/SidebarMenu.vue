<template>
    <ul :class="nodeClass||'sidebar-menu'">
        <li v-for="menu in model" :class="{treeview:menu.children}">
            <a :href="menu.path" @click.prevent="navigateTo(menu)">
                <i :class="menu.icon" v-if="menu.icon"></i>
                <span>{{menu.title}}</span>
                <span class="pull-right-container" v-if="menu.children">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
            </a>
            <sidebar-menu v-if="menu.children" :model="menu.children" node-class="treeview-menu" />
        </li>
    </ul>
</template>
<script>
    export default {
        name: 'SidebarMenu',
        props: ["model", "nodeClass"],
        methods: {
            navigateTo: function (menu) {
                if (!menu.path) {
                    return false;
                }
                if (this.$router && (menu.router === undefined || menu.router == true)) {
                    this.$router.push(menu.path);
                } else {
                    window.location.href = menu.path;
                }
            }
        }
    }
</script>