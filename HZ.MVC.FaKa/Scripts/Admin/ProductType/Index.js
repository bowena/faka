﻿       $(function () {

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
            url: '/ProductType/GetProductTypes',         //请求后台的URL（*）
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
            search: true,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
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
                title: '类别Id'
            }, {
                field: 'ProductName',
                title: '类别名称'
            }, {
                field: 'UpdateTime',
                title: '更新时间',
                formatter: function (value, row, index) {
                    var time = row.UpdateTime;
                    if (time == null) {
                        return "";
                    } else {
                        var sj = parseInt(time.replace(/\D/igm, ""));
                        var rq = new Date(sj);
                        return rq.toLocaleDateString();
                    }
                }
            }, ]
        });
    };

    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            departmentname: $("#txt_search_departmentname").val(),
            statu: $("#txt_search_statu").val()
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

            postdata.Id = "";

            $("#myModal .btn-primary").click(function () {
                $.ajax({
                    type: "post",
                    url: "/ProductType/Add",
                    data: { "proT": $("#myModal").find(".form-control").val() },
                    success: function (data, status) {
                        if (data == "success") {
                            alert("成功");
                            $('#myModal').modal('hide');
                            $("#tb_departments").bootstrapTable('refresh');
                        }
                        else {
                            alert("失败，请重试")
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
            $("#txt_departmentname").val(arrselections[0].ProductName);

            postdata.Id = arrselections[0].Id;
            $('#myModal').modal();
        });

        $("#btn_delete").click(function () {
  
            var arrselections = $("#tb_departments").bootstrapTable('getSelections');
            if (arrselections.length <= 0) {
                alert('请选择有效数据');
                return;
            }

            Ewin.confirm({ message: "确认要删除选择的数据吗？" }).on(function (e) {
                if (!e) {
                    return;
                }
                $.ajax({
                    type: "post",
                    url: "/ProductType/Delete",
                    data: { "pId": JSON.stringify(arrselections) },
                    success: function (data, status) {
                        if (data == "success") {
                            alert('提交数据成功');
                            $("#tb_departments").bootstrapTable('refresh');
                        }
                    },
                    error: function () {
                        alert('Error');
                    },
                    complete: function () {

                    }

                });
            });
        });

        $("#btn_submit").click(function () {
            postdata.ProductName = $("#txt_departmentname").val();
            $.ajax({
                type: "post",
                url: "/Home/GetEdit",
                data: { "": JSON.stringify(postdata) },
                success: function (data, status) {
                    if (status == "success") {
                        toastr.success('提交数据成功');
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

        $("#btn_query").click(function () {
            $("#tb_departments").bootstrapTable('refresh');
        });

    };

    return oInit;
};
(function ($) {

    window.Ewin = function () {
        var html = '<div id="[Id]" class="modal fade" role="dialog" aria-labelledby="modalLabel">' +
                              '<div class="modal-dialog modal-sm">' +
                                  '<div class="modal-content">' +
                                      '<div class="modal-header">' +
                                          '<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>' +
                                          '<h4 class="modal-title" id="modalLabel">[Title]</h4>' +
                                      '</div>' +
                                      '<div class="modal-body">' +
                                      '<p>[Message]</p>' +
                                      '</div>' +
                                       '<div class="modal-footer">' +
        '<button type="button" class="btn btn-default cancel" data-dismiss="modal">[BtnCancel]</button>' +
        '<button type="button" class="btn btn-primary ok" data-dismiss="modal">[BtnOk]</button>' +
    '</div>' +
                                  '</div>' +
                              '</div>' +
                          '</div>';


        var dialogdHtml = '<div id="[Id]" class="modal fade" role="dialog" aria-labelledby="modalLabel">' +
                              '<div class="modal-dialog">' +
                                  '<div class="modal-content">' +
                                      '<div class="modal-header">' +
                                          '<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>' +
                                          '<h4 class="modal-title" id="modalLabel">[Title]</h4>' +
                                      '</div>' +
                                      '<div class="modal-body">' +
                                      '</div>' +
                                  '</div>' +
                              '</div>' +
                          '</div>';
        var reg = new RegExp("\\[([^\\[\\]]*?)\\]", 'igm');
        var generateId = function () {
            var date = new Date();
            return 'mdl' + date.valueOf();
        }
        var init = function (options) {
            options = $.extend({}, {
                title: "操作提示",
                message: "提示内容",
                btnok: "确定",
                btncl: "取消",
                width: 200,
                auto: false
            }, options || {});
            var modalId = generateId();
            var content = html.replace(reg, function (node, key) {
                return {
                    Id: modalId,
                    Title: options.title,
                    Message: options.message,
                    BtnOk: options.btnok,
                    BtnCancel: options.btncl
                }[key];
            });
            $('body').append(content);
            $('#' + modalId).modal({
                width: options.width,
                backdrop: 'static'
            });
            $('#' + modalId).on('hide.bs.modal', function (e) {
                $('body').find('#' + modalId).remove();
            });
            return modalId;
        }

        return {
            alert: function (options) {
                if (typeof options == 'string') {
                    options = {
                        message: options
                    };
                }
                var id = init(options);
                var modal = $('#' + id);
                modal.find('.ok').removeClass('btn-success').addClass('btn-primary');
                modal.find('.cancel').hide();

                return {
                    id: id,
                    on: function (callback) {
                        if (callback && callback instanceof Function) {
                            modal.find('.ok').click(function () { callback(true); });
                        }
                    },
                    hide: function (callback) {
                        if (callback && callback instanceof Function) {
                            modal.on('hide.bs.modal', function (e) {
                                callback(e);
                            });
                        }
                    }
                };
            },
            confirm: function (options) {
                var id = init(options);
                var modal = $('#' + id);
                modal.find('.ok').removeClass('btn-primary').addClass('btn-success');
                modal.find('.cancel').show();
                return {
                    id: id,
                    on: function (callback) {
                        if (callback && callback instanceof Function) {
                            modal.find('.ok').click(function () { callback(true); });
                            modal.find('.cancel').click(function () { callback(false); });
                        }
                    },
                    hide: function (callback) {
                        if (callback && callback instanceof Function) {
                            modal.on('hide.bs.modal', function (e) {
                                callback(e);
                            });
                        }
                    }
                };
            },
            dialog: function (options) {
                options = $.extend({}, {
                    title: 'title',
                    url: '',
                    width: 800,
                    height: 550,
                    onReady: function () { },
                    onShown: function (e) { }
                }, options || {});
                var modalId = generateId();

                var content = dialogdHtml.replace(reg, function (node, key) {
                    return {
                        Id: modalId,
                        Title: options.title
                    }[key];
                });
                $('body').append(content);
                var target = $('#' + modalId);
                target.find('.modal-body').load(options.url);
                if (options.onReady())
                    options.onReady.call(target);
                target.modal();
                target.on('shown.bs.modal', function (e) {
                    if (options.onReady(e))
                        options.onReady.call(target, e);
                });
                target.on('hide.bs.modal', function (e) {
                    $('body').find(target).remove();
                });
            }
        }
    }();
})(jQuery);