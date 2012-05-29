Ext.define('CengZai.view.login.RegisterForm',{
	extend:'Ext.form.Panel',
	xtype:'registerform',
	
	requires:['Ext.form.FieldSet','Ext.field.Password'],
	
	config:{
		animation: 'slide',
		items:[
			{
				xtype: 'titlebar',
			    docked: 'top',
			    title: APP.NAME,
			    items: [
			        {
			            align: 'left',
			            text:	'返回登录',
			            ui: 'back',
			            handler: function(){
			            	Ext.getCmp('login').setActiveItem(0);
			            }
			        }
			    ]
			},
	        {
	            xtype: 'fieldset',
	            title: '新用户注册',
	            items: [
	                {
	                    xtype: 'emailfield',
	                    name : 'email',
	                    label: '邮 箱',
	                    placeHolder:'请填写常用邮箱'
	                },
	                {
	                    xtype: 'passwordfield',
	                    name : 'password',
	                    label: '密 码',
	                    placeHolder:'请输入6-12位密码'
	                },
	                {
	                    xtype: 'textfield',
	                    name : 'name',
	                    label: '姓 名',
	                    placeHolder:'请填写真实姓名'
	                },
	                {
	                    xtype: 'selectfield',
	                    label: '性  别',
	                    options: [
	                        {text: '帅哥',  value: '1'},
	                        {text: '美女',  value: '2'}
	                    ]
	                }
	            ]
	        },
            {
                xtype: 'button',
                text:'注册',
                ui:'confirm',
                handler:function(){
                	
                }
            }
		]
	}
});
