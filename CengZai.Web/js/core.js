var $FR = {
    addSuccess: function succ(json) {
        json = eval(json);
        if (!json) {
            alert("操作异常！");
            return;
        }
        if (json.id > 0) {
            $("a[frienduserid='" + json.id + "']").replaceWith("已关注");
            return;
        }
        else {
            alert(json.msg);
            return;
        }
    },
    addFail: function (res) {
        alert("操作失败！出现异常：" + res.responseText);
    }
}
