//var pageIndex = 1;
//var pageSize = 10;

////加载数据
//var init = function (vm) {
//    abp.services.blog.category.getCategoryByPage(
//        { pageindex: pageIndex, pagesize: pageSize },
//        { async: false, timeout: 30000 }
//    ).done(function (result, data) {
//        if (data.success) {
//            vm.items = result.items;
//            pageIndex = result.pageIndex;
//            pagerArray(result.totalCount, vm);
//            toastr.info("加载成功!");
//        } else {
//            $.scojs_message(result, $.scojs_message.TYPE_ERROR);
//        }
//    });
//};

//// 生成页码
//var pagerArray = function (totalCount, vm) {
//    var totalPage = totalCount / pageSize;
//    var pagerArray = [];
//    for (var i = 0; i < totalPage; i++) {
//        var isActive = false;
//        if (pageIndex === i + 1) {
//            isActive = true;
//        }
//        pagerArray.push({ index: i + 1, isActive: isActive });
//    }
//    vm.pageItems = pagerArray;
//    vm.prevShow = pageIndex !== 1;
//    vm.nextShow = pageIndex === totalPage;
//};

///**
//*分类页面
//**/
//(function () {
//    angular.module('default', [])
//        .controller("CategoryListController", function ($scope) {
//            init($scope);

//            //点击页码
//            $scope.pagerClick = function (index) {
//                if (pageIndex === index) return;
//                pageIndex = index;
//                init($scope);
//            };
//            //前一页
//            $scope.prevPager = function () {
//                pageIndex--;
//                init($scope);
//            };
//            //后一页
//            $scope.nextPager = function () {
//                pageIndex++;
//                init($scope);
//            };

//            //新增分类
//            $scope.netCategory = function () {
//                alert("new");
//            };

//            //编辑
//            $scope.edit = function (id) {
//                alert("编辑" + id);
//            };

//            //删除
//            $scope.remove = function (id) {
//                alert("删除" + id);
//            };
//        });
//})();

var create = function (url) {
    location.href = url;
};

var edit = function (data,url) {
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
    initSource('default', 'CategoryListController', abp.services.blog.category.getCategoryByPage);
})();