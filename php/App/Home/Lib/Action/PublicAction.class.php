<?php
/**
 +------------------------------------------------------------------------------
 * PublicAction 公共方法
 +------------------------------------------------------------------------------
 * @author    foolin <foolin@126.com>
 * @version   2012-05-09 20:14:11
 * Copyright (c) 2012 cengzai.com All rights reserved.
 +------------------------------------------------------------------------------
 */
class PublicAction extends BaseAction {
	
    /**
     +----------------------------------------------------------
     * 验证码
     +----------------------------------------------------------
     */
	function verify(){
		import("ORG.Util.Image");
		Image::buildImageVerify();
	}

    /**
     +----------------------------------------------------------
     * 取用户卡信息
     +----------------------------------------------------------
     */
    function ajax_user_card(){
        if(!$this->isAjax()){
            $this->ajaxReturn('', '非法操作', 0);
            return;
        }
        $login_user = $this->get_login_user();
        if(empty($login_user)){
            $this->ajaxReturn('', '您尚未登录', 0);
            return;
        }
        $this->assign('login_user', $login_user);
        $userid = $this->_get('userid');
        if(empty($userid) || !is_numeric($userid)){
            $this->ajaxReturn('', '参数错误', 0);
            return;
        }
        $user = M('User')->find($userid);
        if(empty($user)){
            $this->ajaxReturn('', '用户不存在！', 0);
            return;
        }
        $this->assign('user', $user);
        $content = $this->fetch();
        $this->ajaxReturn($content, '加载成功', 1);
        return;
    }
    
    
     /**
     +----------------------------------------------------------
     * 读取用户头像
     * 参数：
     * @avatar--加密后的头像名（必须）
     * @w--图片显示最大宽度（选填）
     * @h--图片显示最大高度（选填）
     * foolin 2012-06-13 23:26:48
     +----------------------------------------------------------
     */
    function get_avatar(){
        $encrypt_avatar = $this->_get('avatar');
        $file_name = des_decrypt($encrypt_avatar);
        $file_path = $_SERVER['DOCUMENT_ROOT']."/Public/img/noavatar.jpg";
        if(!empty($file_name)){
            $file_path = $_SERVER['DOCUMENT_ROOT']."/upload/$file_name";
        }
        // 最大宽高 
        list($src_width, $src_height) = $imginfo = getimagesize($file_path);
        if ($imginfo === false) {
            return false;
        }
        $dst_width = $this->_get('w');
        $dst_height = $this->_get('h');
        if(empty($dst_width) && empty($dst_height)){
            $imgdata=fread(fopen($file_path,'rb'),filesize($file_path));
            header("content-type:$imginfo");  
            echo $imgdata;
            exit;
        }
        else{
            
            //对图片高度处理
            if ($dst_width && ($src_width < $src_height)) 
            {
                //高比宽大，高为200,kuan宽按比例缩小 
                $dst_width = ($dst_height / $src_height) * $src_width; 
            }
            else { 
                $dst_height = ($dst_width / $src_width) * $src_height; 
            }
            //读取图片类型
            $imgtype = strtolower(substr(image_type_to_extension($imginfo[2]), 1));
            // 载入原图
            $createfun = 'ImageCreateFrom' . ($imgtype == 'jpg' ? 'jpeg' : $imgtype);
            $src_image = $createfun($file_path);
            //创建目标图片
            if ($imgtype != 'gif' && function_exists('imagecreatetruecolor')){
                $dst_image = imagecreatetruecolor($dst_width, $dst_height);
            } 
            else{
                $dst_image = imagecreate($dst_width, $dst_height);
            } 
            // 复制图片
            if (function_exists("ImageCopyResampled")){
                imagecopyresampled($dst_image, $src_image, 0, 0, 0, 0, $dst_width, $dst_height, $src_width, $src_height);
            }
            else{
               imagecopyresized($dst_image, $src_image, 0, 0, 0, 0, $dst_width, $dst_height, $src_width, $src_height);
            }
            //对gif处理
            if ('gif' == $type || 'png' == $type) {
                //imagealphablending($thumbImg, false);//取消默认的混色模式
                //imagesavealpha($thumbImg,true);//设定保存完整的 alpha 通道信息
                $background_color = imagecolorallocate($dst_image, 0, 255, 0);  //  指派一个绿色
                imagecolortransparent($dst_image, $background_color);  //  设置为透明色，若注释掉该行则输出绿色的图
            }
            // 对jpeg图形设置隔行扫描
            if ('jpg' == $imgtype || 'jpeg' == $imgtype){
                imageinterlace($dst_image, TRUE);
            }
            //输出图像
            $imagefun = 'image' . ($imgtype == 'jpg' ? 'jpeg' : $imgtype);
            $imagefun($dst_image);
            //释放图像
            imagedestroy($dst_image);
            imagedestroy($src_image);
        }
    }

    

}

?>