var $FR = {
    addSuccess: function succ(json) {
        json = eval(json);
        if (!json) {
            alert("操作异常！");
            return;
        }
        if (json.id > 0) {
            $("a[frienduserid='" + json.id + "']").replaceWith('<a class=" muted" data-ajax="true" data-ajax-failure="$FR.delFail" data-ajax-success="$FR.delSuccess" frienduserid="' + json.id + '" href="/friend/frienddel?frienduserid=' + json.id + '&amp;time=' + new Date().getMilliseconds() + '" title="点击取消关注">已关注</a>');
            return;
        }
        else {
            alert(json.msg);
            return;
        }
    },
    addFail: function (res) {
        alert("操作失败！出现异常：" + res.responseText);
    },
    delSuccess: function succ(json) {
        json = eval(json);
        if (!json) {
            alert("操作异常！");
            return;
        }
        if (json.id > 0) {
            $("a[frienduserid='" + json.id + "']").replaceWith('<a class="" data-ajax="true" data-ajax-failure="$FR.addFail" data-ajax-success="$FR.addSuccess" frienduserid="' + json.id + '" href="/friend/friendadd?frienduserid=' + json.id + '&amp;time=' + new Date().getMilliseconds() + '" title="点击关注Ta">关注</a>');
            return;
        }
        else {
            alert(json.msg);
            return;
        }
    },
    delFail: function (res) {
        alert("操作失败！出现异常：" + res.responseText);
    }
}
