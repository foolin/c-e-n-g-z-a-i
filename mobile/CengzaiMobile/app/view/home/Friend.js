Ext.define('CengZai.view.home.Friend',{
	extend: 'Ext.Panel',
    xtype:'homefriend',
    
    requires: [
    ],
    
    config: {
    	title: '好友',
        iconCls: 'user',

        styleHtmlContent: true,
        scrollable: true,

        items: {
            docked: 'top',
            xtype: 'titlebar',
            title: '好友'
        },

        html: [
            "好友"
        ].join("")
    }
})
