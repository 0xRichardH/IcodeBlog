var create = function (url) {
    location.href = url;
};

var edit = function (data, url) {
    location.href = url + "?id=" + data;
};

var remove = function (data, vm, getSource) {
    if (!confirm("确定删除？")) {
        return;
    }
    if (data.length <= 0) {
        toastr.error("操作失败!");
        return;
    }
    abp.services.blog.category.deleteCategory(
            { id: data },
            { async: false, timeout: 30000 }
        ).done(function (result, data) {
            if (data.success) {
                init(vm, getSource);
                toastr.success("操作成功!");
            } else {
                $.scojs_message(result, $.scojs_message.TYPE_ERROR);
            }
        });
};

(function () {
    initSource('default', 'CategoryListController', abp.services.blog.category.getTagByPage);
})();