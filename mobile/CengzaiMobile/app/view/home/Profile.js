Ext.define('CengZai.view.home.Profile',{
	extend: 'Ext.Panel',
    xtype:'homeprofile',
    
    requires: [
    ],
    
    config: {
    	title: '档案',
        iconCls: 'compose',

        styleHtmlContent: true,
        scrollable: true,

        items: {
            docked: 'top',
            xtype: 'titlebar',
            title: '档案'
        },

        html: [
            "档案"
        ].join("")
    }
})
