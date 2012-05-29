Ext.define('CengZai.view.home.Inbox',{
	extend: 'Ext.Panel',
    xtype:'homeinbox',
    
    requires: [
    ],
    
    config: {
    	title: '私聊',
        iconCls: 'organize',

        styleHtmlContent: true,
        scrollable: true,

        items: {
            docked: 'top',
            xtype: 'titlebar',
            title: '私聊'
        },

        html: [
            "私聊"
        ].join("")
    }
})
