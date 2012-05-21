Ext.define('CengZai.view.login.LoginForm',{
	extend:'Ext.form.Panel',
	xtype:'loginform',
	
	requires:[
		'Ext.TitleBar',
		'Ext.form.FieldSet',
		'Ext.field.Password',
		'Ext.field.Email',
		'Ext.field.Select',
		'Ext.Anim'
	],
	
	config:{
		animation: 'slide',
		items:[
			{
				xtype: 'titlebar',
			    docked: 'top',
			    title: '曾在网',
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
	                    xtype: 'textfield',
	                    name : 'email',
	                    label: '邮 箱',
	                    placeHolder:'请输入邮箱'
	                },
	                {
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
                	Ext.Msg.alert('测试啊')
                }
            }
		]
	}
});
