var ue = UE.getEditor('editor', {
    allowDivTransToP: false/*如何阻止div标签自动转换为p标签
背景*/
});

//加载文章分类
var loadCategories = function (vm) {
    abp.services.blog.category.getAllCategory(
        { async: false, timeout: 30000 }
    ).done(function (result, data) {
        if (data.success) {
            vm.items = result.categoryCollection;
            console.log("父分类加载成功!");
        } else {
            $.scojs_message(result, $.scojs_message.TYPE_ERROR);
        }
    });
};

//加载所有的文章状态
var loadPostState = function (vm) {
    abp.services.blog.post.getPostState(
       { async: false, timeout: 30000 }
   ).done(function (result, data) {
       if (data.success) {
           vm.postStateItems = result.values;
           vm.postStaeDict = result.dict;
           console.log("文章状态加载成功!");
       } else {
           $.scojs_message(result, $.scojs_message.TYPE_ERROR);
       }
   });
};

//加载评论设置状态
var loadCommonSettingState = function (vm) {
    abp.services.blog.post.getCommonSettingState(
       { async: false, timeout: 30000 }
   ).done(function (result, data) {
       if (data.success) {
           vm.commonSettingStateDict = result.dict;
           vm.commonSettingStateItems = result.values;
           console.log("评论设置状态加载成功!");
       } else {
           $.scojs_message(result, $.scojs_message.TYPE_ERROR);
       }
   });
};

//加载所有标签
var loadTags = function (vm) {
    abp.services.blog.category.getAllTag(
        { async: false, timeout: 30000 }
    ).done(function (result, data) {
        if (data.success) {
            vm.tagItems = result.categoryCollection;
            console.log("标签加载成功!");
        } else {
            $.scojs_message(result, $.scojs_message.TYPE_ERROR);
        }
    });
};

//加载待编辑的文章内容
var loadPost = function (vm) {
    var title = "", summary = "", address = "", password = "", postState = 0, commonSettingState = 0;

    var id = $("#postId").val();
    if (id.length > 0) {
        loadCategoriesByPostId(id, vm);
        abp.services.blog.post.getPostByIdOrEntryName(
            { idOrEntryName: id },
          { async: false, timeout: 30000 }
        ).done(function (result, data) {
            if (data.success) {
                var post = result.post;
                //赋值
                title = post.title;
                summary = post.except;
                address = post.entryName;
                password = post.password;
                postState = post.status;
                commonSettingState = post.commentStatus;
                $("#editor").html(post.content);
                $("#isShowRSS").attr("checked", post.isShowRSS);
                $("#isTop").attr("checked", post.isTop);
                $("#isOriginal").attr("checked", post.isOriginal);
                toastr.info("加载成功!");
            } else {
                $.scojs_message(result, $.scojs_message.TYPE_ERROR);
            }
        });
    }

    //为页面赋值
    vm.title = title;
    vm.summary = summary;
    vm.address = address;
    vm.password = password;
    vm.postState = postState;
    vm.commonSettingState = commonSettingState;
};

//根据文章Id加载所有的分类
var loadCategoriesByPostId = function (id, vm) {
    abp.services.blog.post.getCategoriesByPostId(
            { idOrEntryName: id },
          { async: true, timeout: 30000 }
        ).done(function (result, data) {
            if (data.success) {
                $(result.categoryCollection).each(function (i) {
                    var category = result.categoryCollection[i];
                    console.log($("#" + category.id));
                    $('#' + category.id).attr("checked", true);
                });
            } else {
                $.scojs_message(result, $.scojs_message.TYPE_ERROR);
            }
        });
};

//保存操作
var saveOperation = function (vm, url) {
    var idArray = [];//文章分类的集合
    $('input[name = "checkBoxCategory"]:checked').each(function () {
        idArray.push($(this).val());
    });

    var title = vm.title;

    var content = ue.getContent();

    var isShowRSS = $("#isShowRSS").attr("checked") == "checked";

    var isTop = $("#isTop").attr("checked") == "checked";

    var isOriginal = $("#isOriginal").attr("checked") == "checked";

    //校验用户的输入
    if (title.length <= 0) {
        toastr.info("请输入标题");
        return;
    }
    if (content.length <= 0) {
        toastr.info("请输入文章内容");
        return;
    }

    var id = $("#postId").val();

    abp.services.blog.post.createOrUpdatePost(
        { id: id, title: title, categoryArray: idArray, content: content, entryName: vm.address, except: vm.summary, password: vm.password, status: vm.postState, commentStatus: vm.commonSettingState, isShowRSS: isShowRSS, isTop: isTop, isOriginal: isOriginal },
        { async: false, timeout: 30000 }
        ).done(function (result, data) {
            if (data.success) {
                if (result.success) {
                    toastr.success("操作成功!");
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
    angular.module("default", []).controller("PostController", function ($scope) {

        loadCategories($scope);
        loadPostState($scope);
        loadCommonSettingState($scope);
        loadTags($scope);

        //加载已经存在的数据
        loadPost($scope);

        $scope.save = function (url) {
            saveOperation($scope, url);
        };

        $scope.cancel = function (url) {
            location.href = url;
        };
    });
})();