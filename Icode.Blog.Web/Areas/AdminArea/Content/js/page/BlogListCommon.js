var pageIndex = 1;
var pageSize = 10;

//加载数据
var init = function (vm, getSource) {
    getSource(
        { pageindex: pageIndex, pagesize: pageSize },
        { async: false, timeout: 30000 }
    ).done(function (result, data) {
        if (data.success) {
            vm.items = result.items;
            pageIndex = result.pageIndex;
            pagerArray(result.totalCount, vm);
            toastr.info("加载成功!");
        } else {
            $.scojs_message(result, $.scojs_message.TYPE_ERROR);
        }
    });
};

// 生成页码
var pagerArray = function (totalCount, vm) {
    var totalPage = totalCount / pageSize;
    var pagerArray = [];
    for (var i = 0; i < totalPage; i++) {
        var isActive = false;
        if (pageIndex === i + 1) {
            isActive = true;
        }
        pagerArray.push({ index: i + 1, isActive: isActive });
    }
    vm.pageItems = pagerArray;
    vm.prevShow = pageIndex > 1;
    vm.nextShow = pageIndex < totalPage;
};

var initSource = function (modeule, controller, getSource) {
    angular.module(modeule, [])
        .controller(controller, function ($scope) {
            init($scope, getSource);

            //点击页码
            $scope.pagerClick = function (index) {
                if (pageIndex === index) return;
                pageIndex = index;
                init($scope, getSource);
            };
            //前一页
            $scope.prevPager = function () {
                pageIndex--;
                init($scope, getSource);
            };
            //后一页
            $scope.nextPager = function () {
                pageIndex++;
                init($scope, getSource);
            };

            //新增分类
            $scope.create = create;

            //编辑
            $scope.edit = edit;

            //删除
            $scope.remove = function(data) {
                remove(data, $scope, getSource);
            };
        });
}