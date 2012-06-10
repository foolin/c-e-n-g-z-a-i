<?php
/**
 +------------------------------------------------------------------------------
 * 提交Twitter的表单功能
 +------------------------------------------------------------------------------
 * @author    foolin <foolin@126.com>
 * @version   2012-05-09 20:14:11
 * Copyright (c) 2012 cengzai.com All rights reserved.
 +------------------------------------------------------------------------------
 */
class TpostWidget extends Widget{ 
    public function render($data){ 
		$conent = $this->renderFile('tpost',$data);
  		return $conent;
    } 
}
?>

