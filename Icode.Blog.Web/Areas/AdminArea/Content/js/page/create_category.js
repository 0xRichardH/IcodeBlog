//加载父分类
var loadParentCategory = function (vm, isCategory) {
    var func = abp.services.blog.category.getAllCategory;
    if (!isCategory) {
        func = abp.services.blog.category.getAllTag;
    }
    func(
        { async: false, timeout: 30000 }
    ).done(function (result, data) {
        if (data.success) {
            vm.items = result.categoryCollection;
            toastr.info("父分类加载成功!");
        } else {
            $.scojs_message(result, $.scojs_message.TYPE_ERROR);
        }
    });
};

//保存
var saveCategory = function (vm, id, url, isCategory) {
    var data = { parentId: vm.parentCategory, categoryName: vm.categoryName, description: vm.description };
    var func = abp.services.blog.category.createCategory;
    if (id.length > 0) {
        data.id = id;
        func = abp.services.blog.category.updateCategory;
    }
    if (!isCategory) {
        data.taxonomy = 1;
    }
    func(
        data,
        { async: false, timeout: 30000 }
        )
        .done(function (result, data) {
            if (data.success) {
                if (result.success) {
                    toastr.success("操作成功");
                    location.href = url;
                } else {
                    toastr.error(result.message);
                }
            } else {
                $.scojs_message(result, $.scojs_message.TYPE_ERROR);
            }
        });
};

(function () {
    angular.module('default', [])
        .controller('CreateCategory', function ($scope) {
            var id = $("#categoryId").val();
            var isCategory = $("#isCategory").val();
            loadParentCategory($scope, isCategory);

            //默认值
            var categoryName = "";
            var parentCategory = "--";
            var description = "";

            if (id.length > 0) {
                //请求待编辑的数据GetCategory
                abp.services.blog.category.getCategory(
               { id: id },
               { async: false, timeout: 30000 }
               )
               .done(function (result, data) {
                   if (data.success) {
                       categoryName = result.category.categoryName;
                       description = result.category.description;
                       var parentId = result.category.parentId;
                       parentCategory = parentId == null ? "--" : parentId;
                       toastr.info("编辑信息加载成功");
                   } else {
                       $.scojs_message(result, $.scojs_message.TYPE_ERROR);
                   }
               });
            }

            $scope.categoryName = categoryName;
            $scope.parentCategory = parentCategory;
            $scope.description = description;

            $scope.save = function (url) {
                var categoryName = $scope.categoryName;
                //校验用户输入
                if (categoryName.length <= 0) {
                    toastr.error("请输入名称!");
                    return;
                }
                if (categoryName.length > 36) {
                    toastr.error("名称长度不可超过36个字!");
                    return;
                }
                //保存到数据库
                saveCategory($scope, id, url, isCategory);
            };

            $scope.cancel = function (url) {
                location.href = url;
            };
        });
})();