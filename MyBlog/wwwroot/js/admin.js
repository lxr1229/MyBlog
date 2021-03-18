function getCategoryList() {
    $.ajax({
        url: "/Admin/CategoryList",
        type: "Post",
        success: function (res) {
            console.log(res)
            if (typeof res == "string") {
                $("#admin_manage").html(res);
            }
            else {
            }
        }
    });
}
function getCategoryEditLayer(id) {
    $.ajax({
        url: "/Admin/GetCategoryEditLayer",
        type: "Get",
        data: { id: id },
        success: function (res) {
            layer.open({
                type: 1,
                area: ['500px', '300px'],
                title: '编辑分类'
                , content: res,//通过这个进入表单
                shade: 0.3,
                btn: []
            })
        }
    })
}

function editCategory() {
    $.ajax({
        url: "/Admin/EditCategory",
        type: "Post",
        data: { CategoryId: $("#CategoryId").val(), CategoryName: $("#CategoryName").val() },
        dataType: "json",
        success: function (res) {
            //if (typeof res == "string") {
            //    if (id.length > 0) {
            //        $('#category_' + id).replaceWith(res);
            //    } else {
            //        $('tbody tr').prepend(res);
            //    }
            //}
            //else {
            //    layer.msg(res.message, { icon: 7, time: 1000 });
            //}
            if (res.success) {
                layer.closeAll();
                location.reload();
                layer.msg(res.message, { icon: 1, time: 1000 });
            } else {
                layer.msg(res.message, { icon: 7, time: 1000 });
            }
        }
    })
}
function deleteCategory(id) {
    layer.confirm("确认删除此数据吗", { btn: ["确定", "取消"] }, function () {
        $.ajax({
            url: "/Admin/DeleteCategory",
            type: "Post",
            data: { id: id },
            dataType: "json",
            success: function (res) {
                if (res.success) {
                    $('#category_' + id).remove();
                    layer.msg(res.message, { icon: 1, time: 1000 });
                }
                else {
                    layer.msg(res.message, { icon: 2, time: 1000 });
                }
            }
        });
    })
}

function getPostList() {
    $.ajax({
        url: "/Admin/PostList",
        type: "Post",
        success: function (res) {
            if (typeof res == "string") {
                $("#admin_manage").html(res)
            }
            else {
            }
        }
    });
}
function deletePost(id) {
    layer.confirm("确认删除此数据吗", { btn: ["确定", "取消"] }, function () {
        $.ajax({
            url: "/Admin/DeletePost",
            type: "Post",
            data: { id: id },
            dataType: "json",
            success: function (res) {
                if (res.success) {
                    $('#post_' + id).remove();
                    layer.msg(res.message, { icon: 1, time: 1000 });
                }
                else {
                    layer.msg(res.message, { icon: 2, time: 1000 });
                }
            }
        });
    })
}
