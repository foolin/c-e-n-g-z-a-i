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
            $("a[frienduserid='" + json.id + "']").replaceWith('<a class="" data-ajax="true" data-ajax-failure="$FR.addFail" data-ajax-success="$FR.addSuccess" frienduserid="' + json.id + '" href="/friend/friendadd?frienduserid=' + json.id + '&amp;time=' + new Date().getMilliseconds() + '" title="点击关注Ta">关注Ta</a>');
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
};


(function (d) {
    d['okValue'] = '确定';
    d['cancelValue'] = '取消';
    d['title'] = '消息';
    // [more..]
})($.dialog.defaults);


function onAjaxSuccess(e) {
    if (e.id == "success") {
        $.dialog({
            title: '操作成功',
            content: e.msg,
            lock: true,
            time: 3000,
            focus: false,
            beforeunload: function () {
                location.href = location.href;
                return true;
            }
        });
    }
    else {
        $.dialog({
            title: '操作失败',
            content: e.msg,
            lock: true,
            time: 3000,
            focus: false
        });
    }
}

/*********** 定期更新消息 **********/
$(document).ready(function () {
    //更新私信箱
    intervalUpdateInboxNewCount();
    setInterval(intervalUpdateInboxNewCount, 180 * 1000);  //3分钟更新一次
    //更新是否有追求者
    updateLoverNewCount();
});
//更新私信箱
function intervalUpdateInboxNewCount() {
    $.getJSON("/inbox/ajaxgetnewcount?t=" + new Date().getMilliseconds(), function (json) {
        if (json.id == "success") {
            var count = parseInt(json.msg);
            if (count > 0) {
                $("#inboxNewCount").html(count).show();
                return;
            }
            $("#inboxNewCount").html(0).hide();
        }
    });
}
//更新是否有追求者
function updateLoverNewCount() {
    $.getJSON("/lover/ajaxgetnewcount?t=" + new Date().getMilliseconds(), function (json) {
        if (json.id == "success") {
            var count = parseInt(json.msg);
            if (count > 0) {
                $("#loverNewCount").html(count).show();
                return;
            }
            $("#loverNewCount").html(0).hide();
        }
    });
}

