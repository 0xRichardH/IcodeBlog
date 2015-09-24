var pageIndex = 1, pageSize = 10;

//加载文章
var initPost = function (vm) {
    if (location.hash) {
        var hash = location.hash;
        var hashIndex = hash.substring(hash.indexOf("#") + 2, hash.length);
        if (/^\d+$/.test(hashIndex)) {
            pageIndex = hashIndex - 0;
        }
    }
    //异步加载文章



    //处理hash
    if (pageIndex === 1) {
        location.hash = "";
    } else {
        location.hash = "#p" + pageIndex;
    }

    pagerArray(100,vm);
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
    vm.prevEnable = pageIndex > 1;
    vm.nextEnable = pageIndex < totalPage;
};

(function () {

    var modules = angular.module('default', []);

    //所有文章
    modules.controller('MainController', function ($scope) {
        initPost($scope);//初始化加载数据

        //点击页码
        $scope.pageHash = function (data) {
            pageIndex = data;
            location.hash = "#p" + data;
            initPost($scope);
        };
        ////前一页
        //$scope.prevPager = function () {
        //    pageIndex--;
        //    initPost($scope, getSource);
        //};
        ////后一页
        //$scope.nextPager = function () {
        //    pageIndex++;
        //    initPost($scope, getSource);
        //};
    });

    //最新文章
    modules.controller('GetTop10PostController', function ($scope) {
        abp.ajax($.extend({
            url: 'api/services/blog/post/getTopTen'
        }, { async: false, timeout: 30000 })).done(function (data, result) {
            if (result.success) {
                $scope.items = data.items;
            }
        }
       );
    });

    //所有分类

})();