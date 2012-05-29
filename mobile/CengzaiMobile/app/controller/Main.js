Ext.define('CengZai.controller.Main', {
    extend: 'Ext.app.Controller',
    
    config: {
        refs: {
            main:'main'
        },
        control: {
            
        }
    },
    
    //called when the Application is launched, remove if not needed
    launch: function(app) {
    	console.log('哈哈，初始化！')
    	//this.getMain().setActiveItem(1);
    	/*
        var isLogin = false;
        if(isLogin){
        	this.getMain().setActiveItem(1);
        }
        else{
        	this.getMain().setActiveItem(0);
        }
        */
    },
    
    /************ 登录操作 ***********/
   showLogin: function(){
   		this.getMain().setActiveItem(1);
   },
   showHome: function(){
   		this.getMain().setActiveItem(0);
   }
});