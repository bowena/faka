$(function () {

    //1.初始化Table
    var oTable = new TableInit();
    oTable.Init();

    //2.初始化Button的点击事件
    var oButtonInit = new ButtonInit();
    oButtonInit.Init();

});


var TableInit = function () {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#tb_departments').bootstrapTable({
            url: '/KaMi/GetKaMis',         //请求后台的URL（*）
            method: 'get',                      //请求方式（*）
            toolbar: '#toolbar',                //工具按钮用哪个容器
            striped: true,                      //是否显示行间隔色
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            sortable: false,                     //是否启用排序
            sortOrder: "asc",                   //排序方式
            queryParams: oTableInit.queryParams,//传递参数（*）
            sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 10,                       //每页的记录行数（*）
            pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
            search: false,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            strictSearch: true,
            showColumns: true,                  //是否显示所有的列
            showRefresh: true,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行
            height: 500,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "ID",                     //每一行的唯一标识，一般为主键列
            showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            columns: [{
                checkbox: true
            }, {
                field: 'Id',
                title: '卡密Id'
            }, {
                field: 'Content',
                title: '卡密内容'
            }, {
                field: 'ProductType_Id',
                title: '类别Id'
            }, {
                field: 'ProductTypeName',
                title: '类别名称'
            }, {
                field: 'Product_Id',
                title: '产品Id'
            }, {
                field: 'ProductName',
                title: '产品名称'
            }, {
                field: 'Remark',
                title: '备注'
            }, ]
        });
    };

    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            departmentname: $("#txt_search_departmentname").val(),
        };
        return temp;
    };
    return oTableInit;
};


var ButtonInit = function () {
    var oInit = new Object();
    var postdata = {};
    oInit.Init = function () {
        $("#btn_add").click(function () {
            $("#myModalLabel").text("新增");
            $("#myModal").find(".form-control").val("");
            $('#myModal').modal()

            $("#myModal .btn-primary").unbind("click").click(function () {
                postdata.Product_Id = $("#txt_modal_product").val() * 1;
                postdata.ProductType_Id = $("#txt_modal_productType").val() * 1;
                postdata.Remark = $("#txt_modal_remark").val();
                postdata.Content = $("#txt_modal_kamis").val();
                $.ajax({
                    type: "post",
                    url: "/KaMi/Add",
                    data: { "proT": JSON.stringify(postdata) },
                    success: function (data, status) {
                        if (data == "success") {
                            toastr.success("成功");
                            $('#myModal').modal('hide');
                            $("#tb_departments").bootstrapTable('refresh');
                        }
                        else if (data == "empty") {
                            toastr.warning("请输入有效的名称")
                        }
                        else {
                            toastr.error("失败，请重试")
                        }
                    },
                    error: function () {
                        toastr.error('Error');
                    },
                    complete: function () {

                    }
                });
            });
        });

        $("#btn_edit").click(function () {
            var arrselections = $("#tb_departments").bootstrapTable('getSelections');
            if (arrselections.length > 1) {
                toastr.warning('只能选择一行进行编辑');

                return;
            }
            if (arrselections.length <= 0) {
                toastr.warning('请选择有效数据');

                return;
            }
            $("#myModalLabel").text("编辑");
            $("#txt_modal_content").val(arrselections[0].Content);
            $("#txt_modal_remark").val(arrselections[0].Remark);
            //$("#txt_modal_productType").val(arrselections[0].ProductType_Id);
            //$("#txt_modal_product").val(arrselections[0].Product_Id);
            postdata.Id = arrselections[0].Id;
            $('#myModal').modal();

            $("#myModal .btn-primary").unbind("click").click(function () {
                postdata.Content = $("#txt_modal_content").val();
                postdata.Remark = $("#txt_modal_remark").val();
                postdata.ProductType_Id = $("#txt_modal_productType").val() * 1;
                postdata.Product_Id = $("#txt_modal_product").val() * 1;
                $.ajax({
                    type: "post",
                    url: "/KaMi/Update",
                    data: { "proM": JSON.stringify(postdata) },
                    success: function (data, status) {
                        if (data == "success") {
                            toastr.success("成功");
                            $('#myModal').modal('hide');
                            $("#tb_departments").bootstrapTable('refresh');
                        }
                        else if (data == "empty") {
                            toastr.warning("请输入有效的名称")
                        }
                        else {
                            toastr.error("失败，请重试")
                        }
                    },
                    error: function () {
                        toastr.error('Error');
                    },
                    complete: function () {

                    }
                });
            });
        });

        $("#btn_delete").click(function () {

            var arrselections = $("#tb_departments").bootstrapTable('getSelections');
            if (arrselections.length <= 0) {
                toastr.warning('请选择有效数据');
                return;
            }

            Ewin.confirm({ message: "确认要删除选择的数据吗？" }).on(function (e) {
                if (!e) {
                    return;
                }
                $.ajax({
                    type: "post",
                    url: "/KaMi/Delete",
                    data: { "pId": JSON.stringify(arrselections) },
                    success: function (data, status) {
                        if (data == "success") {
                            toastr.success('删除成功');
                            $("#tb_departments").bootstrapTable('refresh');
                        }
                    },
                    error: function () {
                        toastr.error('Error');
                    },
                    complete: function () {

                    }

                });
            });
        });

        $("#txt_modal_productType").change(function () {
            $("#txt_modal_product").empty();
            $.ajax({
                type: "post",
                url: "/KaMi/InitProduct",
                data: { "proT": this.value },
                success: function (data) {

                    var proList = eval(data);
                    if (proList.length > 0) {
                       
                        $(proList).each(function (i) {
                            $("#txt_modal_product").append("<option value='" + this.Id + "'>"+ this.Name +"</option>");
                        });
                    }
                },
                error: function () {
                    toastr.error('Error');
                },
                complete: function () {

                }
            });

        });

        $("#btn_query").click(function () {
            $("#tb_departments").bootstrapTable('refresh');
        });

    };

    return oInit;
};
