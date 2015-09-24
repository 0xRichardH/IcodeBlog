(function () {

    $("#EmailAddressInput").focus(function () {
        $("#errorSpan").text("");
    });

    $("#ValidateInput").focus(function () {
        $("#errorSpan").text("");
    });

    $('#LoginButton').click(function (e) {
        e.preventDefault();
        var email = $('#EmailAddressInput').val();
        if (email.length <= 0) {
            $("#errorSpan").text("邮箱地址不可为空");
            return;
        }
        if (!/^[a-z0-9]+([._\\-]*[a-z0-9])*@([a-z0-9]+[-a-z0-9]*[a-z0-9]+.){1,63}[a-z0-9]+$/.test(email)) {
            $("#errorSpan").text("邮箱格式不正确");
            return;
        }
        var vcode = $("#ValidateInput").val();
        if (vcode.length <= 0) {
            $("#errorSpan").text("请输入验证码");
            return;
        }

        $(this).addClass('disabled');
        abp.ajax({
            url: abp.appPath + 'account/login',
            type: 'POST',
            data: JSON.stringify({
                email: email,
                host: $('#host').val(),
                sessionId: $('#sessionId').val(),
                returnUrl: $('#returnUrl').val(),
                rememberMe: $('#RememberMeInput').is(':checked'),
                validateCode: vcode
            })
        }).fail(function (error) {
            $("#LoginButton").removeClass('disabled');
            $("#errorSpan").text("校验失败");
        });
    });


    $('#SendButton').click(function (e) {
        e.preventDefault();

        var email = $('#EmailAddressInput').val();
        if (email.length <= 0) {
            $("#errorSpan").text("邮箱地址不可为空");
            return;
        }
        if (!/^[a-z0-9]+([._\\-]*[a-z0-9])*@([a-z0-9]+[-a-z0-9]*[a-z0-9]+.){1,63}[a-z0-9]+$/.test(email)) {
            $("#errorSpan").text("邮箱格式不正确");
            return;
        }

        //发送邮件
        abp.ajax({
            url: abp.appPath + 'account/sendEmail',
            type: 'POST',
            data: JSON.stringify({
                email: $('#EmailAddressInput').val(),
                host: $('#host').val(),
                sessionId: $('#sessionId').val(),
                returnUrl: $('#returnUrl').val()
            })
        }).done(function (result,data) {
                //文本框设置为已读
                $('#EmailAddressInput').attr("readonly", "readonly");
                $('#SendButton').text("发送成功").addClass("disabled");
                $("#errorSpan").text("验证码发送成功");
        }).fail(function (error) {
            alert(JSON.stringify(error));
        });
        //$(this).addClass('disabled');
        //倒计时
    });
})();