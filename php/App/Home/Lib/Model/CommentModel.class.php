<?php
/**
 +------------------------------------------------------------------------------
 * t_comment实体
 +------------------------------------------------------------------------------
 * @author    foolin <foolin@126.com>
 * @version   2012-05-31 14:58:16
 * Copyright (c) 2012 cengzai.com All rights reserved.
 +------------------------------------------------------------------------------
 */
class CommentModel extends Model {
    
    const TYPE_USER           =   0;   //留言
    const TYPE_TWITTER        =   1;   //说说
    const TYPE_PHOTO          =   2;   //图片

    //自动验证
    protected $_validate = array(
        array('text','require','内容不能为空！'), //默认情况下用正则进行验证
    );
    
    //自动完成
    protected $_auto = array (
        array('createtime',"now_datetime",1,'function'), // 对regtime字段在更新的时候写入当前时间戳
        array('createip','get_client_ip',1,'function'), //取ip
        array('parentid','0',1), //默认为0
    );
}

?>