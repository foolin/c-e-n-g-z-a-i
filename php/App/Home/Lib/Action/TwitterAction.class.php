<?php
/**
 +------------------------------------------------------------------------------
 * TwitterAction  说说相关管理和操作
 +------------------------------------------------------------------------------
 * @author    foolin <foolin@126.com>
 * @version   2012-05-30 17:19:32
 * Copyright (c) 2012 cengzai.com All rights reserved.
 +------------------------------------------------------------------------------
 */
class TwitterAction extends BaseAction {
    
    /**
     +----------------------------------------------------------
     * 发表说说
     +----------------------------------------------------------
     */
    function ajax_post(){
        //判断是否Ajax提交
        if(!$this->isAjax()){
            $this->ajaxReturn('', '非法操作！', 0);
            return;
        }
        $user = $this->get_login_user();
        //判断是否登录
        if(empty($user)){
            $this->ajaxReturn('', '你尚未登录或者登录超时！', 0);
            return;
        }
        //创建数据库并插入表
        $mTwittier = D('Twitter');
        $data = $mTwittier->create();
        if(!$data){
            $this->ajaxReturn('', $mTwittier->getError(), 0);
            return;
        }
        $data['type'] = TwitterModel::TYPE_TEXT;
        $data['userid'] = $user['userid']; //取用户id
        if($mTwittier->add($data)){
            $this->ajaxReturn($data, '发布成功！', 1);
        }
        else{
            $this->ajaxReturn($data, '发布失败！', 1);
        }
    }

    /**
     +----------------------------------------------------------
     * 获取列表
     +----------------------------------------------------------
     */
    function ajax_friends_list(){
        //判断是否Ajax提交
        if(!$this->isAjax()){
            $this->ajaxReturn('', '非法操作！', 0);
            return;
        }
        //判断是否登录
        $user = $this->get_login_user();
        if(empty($user)){
            $this->ajaxReturn('', '你尚未登录或者登录超时！', 0);
            return;
        }
        $page = $this->_get('page');
        if(empty($page) || $page<0){
            $page = 1;
        }
        $pagesize = $this->_get('pagesize');
        if(empty($pagesize) || $pagesize<0){
            $pagesize = 20;
        }
        
        $dbTwitter = D('Twitter');
        $list = NULL;
        $userid = $this->_get('userid');
        if($userid > 0){
            $list = $dbTwitter->relation(true)->where(array("userid"=>"$userid"))->order('createtime desc')->page("$page,$pagesize")->select();
        }
        else{
            $dbFriend = M('Friend');
            $mapFriend['userid'] = $user['userid'];
            $mapFriend['type'] = array('egt', 0);
            $friends = $dbFriend->where($mapFriend)->field('frienduserid')->select();
            
            if(!empty($friends)){
                $friendids = array($user['userid']);    //把自己加上
                foreach ($friends as $key => $value) {
                    array_push($friendids, $value['frienduserid']);
                }
                $mapTwitter['userid'] = array('in', $friendids);
                $list = $dbTwitter->relation(true)->where($mapTwitter)->order('createtime desc')->page("$page,$pagesize")->select();
            }
            else{
                $list = $dbTwitter->relation(true)->order('createtime desc')->page("$page,$pagesize")->select();
            }
        }
        //dump($list);return;
        if(empty($list)){
            $this->ajaxReturn('', '暂无任何说说！', 0);
        }
        else{
            $this->assign('list', $list);
            $content = $this->fetch("ajax_list"); 
            $this->ajaxReturn($content, '加载成功！', 1);
        }
    }


    /**
     +----------------------------------------------------------
     * 获取列表
     +----------------------------------------------------------
     */
    function ajax_user_list()
    {
        //判断是否Ajax提交
        if(!$this->isAjax()){
            $this->ajaxReturn('', '非法操作！', 0);
            return;
        }
        $userid = $this->_get('userid');
        if(!is_numeric($userid)){
            //判断是否登录
            $user = $this->get_login_user();
            if(empty($user)){
                $this->ajaxReturn('', '你尚未登录或者登录超时！', 0);
                return;
            }
            $userid = $user['userid'];
        }
        $page = $this->_get('page');
        if(empty($page) || $page<0){
            $page = 1;
        }
        $pagesize = $this->_get('pagesize');
        if(empty($pagesize) || $pagesize<0){
            $pagesize = 20;
        }
        $dbTwitter = D('Twitter');
        $list = NULL;
        $mapTwitter['userid'] = $userid;
        $list = $dbTwitter->relation(true)->where($mapTwitter)->order('createtime desc')->page("$page,$pagesize")->select();
        //dump($list);return;
        if(empty($list)){
            $this->ajaxReturn('', '暂无任何说说！', 0);
        }
        else{
            $this->assign('list', $list);
            $content = $this->fetch("ajax_list"); 
            $this->ajaxReturn($content, '加载成功！', 1);
        }
    }
}

?>