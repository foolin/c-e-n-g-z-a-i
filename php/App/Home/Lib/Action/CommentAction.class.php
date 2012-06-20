<?php
/**
 +------------------------------------------------------------------------------
 * 评论相关管理和操作
 +------------------------------------------------------------------------------
 * @author    foolin <foolin@126.com>
 * @version   2012-05-30 17:19:32
 * Copyright (c) 2012 cengzai.com All rights reserved.
 +------------------------------------------------------------------------------
 */
class CommentAction extends BaseAction {
    
    /**
     +----------------------------------------------------------
     * 发表评论
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
        $dbComment = D('Comment');
        $data = $dbComment->create();
        if(!$data){
            $this->ajaxReturn('', $dbComment->getError(), 0);
            return;
        }
        //判断源id是否正确
        if(empty($data['typeid']) || $data['typeid']<= 0){
            $this->ajaxReturn('', '非法操作，参数错误！', 0);
            return;
        }
        //检查源id是否存在
        if($data['type'] == CommentModel::TYPE_USER){
            $dbUser = M('User');
            $count = $dbUser->find($data['typeid']).count();
            if(empty($count) || $count<=0){
                $this->ajaxReturn('', '非法操作，源数据不存在！', 0);
                return;
            }
        }
        else if($data['type'] == CommentModel::TYPE_TWITTER){
            $dbTwitter = M('Twitter');
            $count = $dbTwitter->find($data['typeid']);
            if(empty($count) || $count<=0){
                $this->ajaxReturn('', '非法操作，源数据不存在！', 0);
                return;
            }
        }
        else if($data['type'] == CommentModel::TYPE_PHOTO){
            $dbPhoto = M('Photo');
            $count = $dbPhoto->find($data['typeid']).count();
            if(empty($count)){
                $this->ajaxReturn('', '非法操作，源数据不存在！', 0);
                return;
            }
        }
        else{
            $this->ajaxReturn('', '非法操作，类型错误！', 0);
            return;
        }
        
        $data['userid'] = $user['userid']; //取用户id
        if($dbComment->add($data)){
            if($data['type'] == CommentModel::TYPE_TWITTER){
                M('Twitter')->where(array('twitterid'=>$data['typeid']))->setInc('comments');
            }
            else if($data['type'] == CommentModel::TYPE_PHOTO){
                M('Photo')->where(array('photoid'=>$data['typeid']))->setInc('comments');
            }
            $this->ajaxReturn($data, '发表成功！', 1);
        }
        else{
            $this->ajaxReturn('', '发表失败！', 1);
        }
        
    }

    /**
     +----------------------------------------------------------
     * 获取列表
     +----------------------------------------------------------
     */
    function ajax_list(){
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
        $type = $this->_get('type');
        $typeid = $this->_get('typeid');
        if(empty($type) || empty($typeid) || $typeid<= 0){
            $this->ajaxReturn('', '非法操作，参数错误！', 0);
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
        $dbComment = M('Comment');
        $map['typeid'] = $typeid;
        $map['type'] = $type;
        $list = $dbComment->where($map)->order('commentid asc')->page("$page,$pagesize")->select();
        if(empty($list)){
            $this->ajaxReturn('', '暂无任何内容！', 0);
        }
        else{
            $this->assign('list', $list);
            $content = $this->fetch(); 
            $this->ajaxReturn($content, '加载成功！', 1);
        }
    }
}

?>