<template>
    <div class="wrapper">
        <ss-header></ss-header>
        <ss-sidebar></ss-sidebar>
        <div class="content-wrapper">
            <router-view></router-view>
        </div>
        <ss-footer></ss-footer>
        <ss-control-sidebar></ss-control-sidebar>
        <!-- Add the sidebar's background. This div must be placed immediately after the control sidebar -->
        <div class="control-sidebar-bg"></div>
    </div>
</template>
<style lang="scss">
    @import './styles/app';
</style>
