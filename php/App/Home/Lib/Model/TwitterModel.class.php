<?php
/**
 +------------------------------------------------------------------------------
 * t_twitter实体
 +------------------------------------------------------------------------------
 * @author    foolin <foolin@126.com>
 * @version   2012-05-30 17:27:58
 * Copyright (c) 2012 cengzai.com All rights reserved.
 +------------------------------------------------------------------------------
 */
class TwitterModel extends Model {
    
    const TYPE_TEXT      =   0;       //文本
    const TYPE_IMAGE      =   1;      //图片
    const TYPE_VIDEO      =   2;      //视频
    
    
    //自动验证
    protected $_validate = array(
        array('text','require','内容必须不能为空！'), //默认情况下用正则进行验证
    );
    
    //自动完成
    protected $_auto = array (
        array('createtime',"now_datetime",1,'function'), // 对regtime字段在更新的时候写入当前时间戳
        array('createip','get_client_ip',1,'function'), //取ip
    );
}

?>