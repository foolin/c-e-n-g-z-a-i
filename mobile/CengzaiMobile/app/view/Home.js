Ext.define("CengZai.view.Home", {
    extend: 'Ext.tab.Panel',
    xtype:'home',
    
    requires: [
        'Ext.TitleBar',
        'Ext.Video',
        'CengZai.view.home.Index',
        'CengZai.view.home.Inbox',
        'CengZai.view.home.Friend',
        'CengZai.view.home.Profile',
        'CengZai.view.home.Settings',
    ],
    
    config: {

        tabBarPosition: 'bottom',
        
        layout: {
			//type: 'card',
			animation : null
		},


        items: [
        	{
        		xtype:'homeindex',
        	},
        	{
        		xtype:'homeinbox',
        	},
        	{
        		xtype:'homefriend',
        	},
        	{
        		xtype:'homeprofile',
        	},
        	{
        		xtype:'homesettings',
        	},
        	/*
            {
                title: '首页2',
                iconCls: 'home',

                styleHtmlContent: true,
                scrollable: true,

                items: {
                    docked: 'top',
                    xtype: 'titlebar',
                    title: 'Welcome to Sencha Touch 2'
                },

                html: [
                    "You've just generated a new Sencha Touch 2 project. What you're looking at right now is the ",
                    "contents of <a target='_blank' href=\"app/view/Main.js\">app/view/Main.js</a> - edit that file ",
                    "and refresh to change what's rendered here."
                ].join("")
            },
            {
                title: '关于',
                iconCls: 'action',

                items: [
                    {
                        docked: 'top',
                        xtype: 'titlebar',
                        title: 'Getting Started'
                    },
                    {
                        xtype: 'panel',
                        html:'这是测试啊'
                    }
                ]
            }
            */
        ]
    }
});
