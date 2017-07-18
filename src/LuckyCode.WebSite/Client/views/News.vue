<template>
    <div>
        <ss-table id="table1"
                  url="/api/NewsArticle"
                  :pageSize=10>
            <ss-col name="ArticleId" title="ArticleId" />
            <ss-col name="Title" title="Title" />
            <ss-col name="CreateDate" title="CreateDate" />
            <ss-col title="操作" width="130px">
                <a class="btn btn-default" $@click="editRow(row)">
                    编辑
                    <i class="fa fa-edit"></i>
                </a>
                <a class="btn btn-danger" $@click="deleteRow(row.ArticleId)">
                    删除
                    <i class="fa fa-trash"></i>
                </a>
            </ss-col>
        </ss-table>

    </div>
</template>
<script>
    import tableAPI from "../components/table/TableAPI";
    export default {
        data() {
            return {
                rows: [],
                visible: false
            }
        },
        
        methods: {
            editRow(row) {
                layer.open({
                    //type:0,
                    title: `正在编辑'${row.Title}'`,
                    shadeClose: false,
                    shade: [0.5, '#808080'],
                    maxmin: true,
                    area: ["90%", "90%"],
                    content: "表单还没弄...",
                    resize: false
                });
            },
            deleteRow(id) {
                tableAPI.deleteRow({
                    url: "/xxx",
                    params: `id=${id}`,
                    confirm: "是否删除记录",
                    confirmTitle: "删除提示",
                });
            },
            show: function () {

            }

        }

    }
</script>