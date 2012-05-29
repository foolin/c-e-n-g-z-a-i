Ext.define('CengZai.view.login.LoginForm',{
	extend:'Ext.form.Panel',
	xtype:'loginform',
	id:'loginform',
	
	requires:[
		'Ext.TitleBar',
		'Ext.form.FieldSet',
		'Ext.field.Password',
		'Ext.field.Email',
		'Ext.field.Select',
		'Ext.Anim',
		'Ext.data.JsonP'
	],
	
	config:{
		animation: 'slide',
		items:[
			{
				xtype: 'titlebar',
			    docked: 'top',
			    title: APP.NAME,
			    items: [
			        {
			            align: 'right',
			            text:	'立刻注册',
			            ui: 'forward',
			            handler: function(){
			            	/*
			            	var card = Ext.getCmp('login');
			            	card.getLayout().setAnimation({type: 'slide', direction: 'right'});
							card.setActiveItem(1);
							card.getLayout().setAnimation({type: 'slide', direction: 'left'});
							*/
			            	Ext.getCmp('login').setActiveItem(1);
			            }
			        }
			    ]
			},
	        {
	            xtype: 'fieldset',
	            title: '用户登录',
	            items: [
	                {
	                	id: 'loginEmail',
	                    xtype: 'textfield',
	                    name : 'email',
	                    label: '邮 箱',
	                    placeHolder:'请输入邮箱'
	                },
	                {
	                	id: 'loginPassword',
	                    xtype: 'passwordfield',
	                    name : 'password',
	                    label: '密 码',
	                    placeHolder:'请输入6-12位密码'
	                }
	            ]
	        },
            {
                xtype: 'button',
                text:'登录',
                ui: 'confirm',
                handler: function(){
                	var login = Ext.getCmp('loginform');
                	var formdata = login.getValues();

                	if(!login.getMasked()){
                		login.setMasked({
	                		xtype: 'loadmask',
	                		message: '正在登录...',
	                		indicator:true
	                	});
                	}
                	
                	setTimeout("Ext.getCmp('loginform').unmask();Ext.getCmp('main').setActiveItem(1);", 3000);
                	
                	/*
			        // Make the JsonP request
			        Ext.data.JsonP.request({
			            url: CONFIG.BASE_URL + '/account/dologin',
			            callbackKey: 'jsonp',
			            params: formdata,
			            success: function(result, request) {
			                // Unmask the viewport
			                Ext.getCmp('loginform').unmask();
							console.log(result);
			                //alert(result);
			                Ext.getCmp('main').setActiveItem(1);
			            }
			        });
			        */
                	
                	//capturePhoto();
                	/*
                	
                	else{
                		login.unmask();
                	}
                	*/
                	//Ext.Msg.alert('测试啊')
                	
                }
            },
            {
            	xtype:'panel',
            	html:'<img style="display:none;width:60px;height:60px;" id="smallImage" src="" />'
    				+ '<img style="display:none;" id="largeImage" src="" />'
            }
		]
	}
});
