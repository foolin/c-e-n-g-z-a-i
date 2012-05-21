Ext.define('CengZai.view.Login',{
	extend:'Ext.Panel',
	xtype:'login',
	id:'login',
	
	requires:[
		'CengZai.view.login.LoginForm',
		'CengZai.view.login.RegisterForm'
	],
	

	config:{
		
		layout: {
			type: 'card',
			animation : {
				type:'slide', 
				direction:'left'
			}
		},

		defaults: {
			
			scrollable: true
		},
    	items:[

        	{
				xtype:'loginform',
			},
			{
				xtype:'registerform',
			}

		]
	}
});
