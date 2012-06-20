<?php
/**
 +------------------------------------------------------------------------------
 * FriendAction 用户朋友
 +------------------------------------------------------------------------------
 * @author    foolin <foolin@126.com>
 * @version   2012-06-14 17:01:42
 * Copyright (c) 2012 cengzai.com All rights reserved.
 +------------------------------------------------------------------------------
 */
class FriendAction extends BaseAction {
    
    /**
     +----------------------------------------------------------
     * 朋友列表
     +----------------------------------------------------------
     */
    function index(){
        $this->tpl('登录');
    }

    /**
     +----------------------------------------------------------
     * 发送私信
     +----------------------------------------------------------
     */
    function relation_link(){
        if(!is_numeric($login_userid) || !is_numeric($check_userid)){
            return "unkown";
        }
        $dbUser = M('Friend');
        $follow = $dbUser->find(array('userid'=>$login_userid, 'frienduserid'=>$check_userid));
        $fans = $dbUser->find(array('userid'=>$check_userid, 'frienduserid'=>$login_userid));
        if(!empty($follow) && $follow['type']>=0 && !empty($fans) && $fans['type']>0){
            return 'friend';
        }
        else if(!empty($follow) && $follow['type']>=0){
            return 'follow';
        }
        else if(!empty($fans) && $fans['type']>0){
            return 'fans';
        }
        else{
            return "unkown";
        }
    }
    


     /**
     +----------------------------------------------------------
     * ajax添加朋友
     +----------------------------------------------------------
     */
    function ajax_follow_add(){
        if(!$this->isAjax()){
            $this->ajaxReturn('', '非法操作！', 0);
            return;
        }
        $login_user = $this->get_login_user();
        if(empty($login_user)){
            $this->ajaxReturn('', '您尚未登录！', 0);
            return;
        }
        $friend_userid = $this->_get('userid');
        if(!is_numeric($friend_userid)){
            $this->ajaxReturn('', '用户不存在！', 0);
            return;
        }
        $dbFriend = M('Friend');
        $follow = $dbFriend->where(array('userid'=>$login_user['userid'], 'frienduserid'=>$friend_userid))->find();
        $fans = $dbFriend->where(array('userid'=>$friend_userid, 'frienduserid'=>$login_user['userid']))->find();
        $isadd = FALSE;
        
        if(empty($follow)){
            $follow = array(
                'userid' => $login_user['userid'],
                'frienduserid' => $friend_userid,
                'type' => 0,    //朋友类型：0=认识，1=互相认识，-1=黑名单
                'relation' => '',
                'createtime'=> date('Y-m-d H:i:s')
            );
            $follow['id'] = $dbFriend->add($follow);
            $isadd = TRUE;
        }
        
        if(!empty($fans) && $fans['type'] == 0){
            $follow['type'] = 1;
            $dbFriend->save($follow);
            $fans['type'] = 1;
            $dbFriend->save($fans);
        }
        else if(!$isadd){
            $dbFriend->save($follow);
        }
        
        /*
        $data = '';
        
        if($follow['type'] == 1){
            $data = "<a href=". U('Friend/ajax_follow_add', "userid=$userid") ." action='friends' title='点击取消认识'>互相认识</a>";
        }
        else if($follow['type'] == 0){
            $data = "<a href=". U('Friend/ajax_follow_del', "userid=$userid") ." action='friends' title='点击取消认识'>已认识</a>";
        }
        else if($follow['type'] == -1){
            $data = "<a href=". U('Friend/ajax_follow_del', "userid=$userid") ." action='friends' title='点击取消黑名单'>黑名单</a>";
        }
        else{
            $data = "读取错误";
        }*/
        $data = follow_link($login_user['userid'], $friend_userid);
        $this->ajaxReturn($data, "操作成功", 1);
    }



    /**
     +----------------------------------------------------------
     * ajax删除朋友
     +----------------------------------------------------------
     */
    function ajax_follow_del(){
        if(!$this->isAjax()){
            $this->ajaxReturn('', '非法操作！', 0);
            return;
        }
        $login_user = $this->get_login_user();
        if(empty($login_user)){
            $this->ajaxReturn('', '您尚未登录！', 0);
            return;
        }
        $friend_userid = $this->_get('userid');
        if(!is_numeric($friend_userid)){
            $this->ajaxReturn('', '用户不存在！', 0);
            return;
        }
        $dbFriend = M('Friend');
        $follow = $dbFriend->where(array('userid'=>$login_user['userid'], 'frienduserid'=>$friend_userid))->find();
        $fans = $dbFriend->where(array('userid'=>$friend_userid, 'frienduserid'=>$login_user['userid']))->find();
        if(empty($follow)){
            $this->ajaxReturn('', '尚未加为好友！', 0);
            return;
        }
        if(!empty($fans) && $fans['type'] == 1){
            $fans['type'] = 0;
            $dbFriend->save($fans);
        }
        //dump($follow); die;
        $dbFriend->where("id=".$follow['id'])->delete();
        
        //$data = "<a href=". U('Friend/ajax_follow_add', "userid=$userid") ." action='follow' title='点击加为认识'>认识</a>";
        $data = follow_link($login_user['userid'], $friend_userid);
        $this->ajaxReturn($data, "操作成功", 1);
    }
    
}

?>