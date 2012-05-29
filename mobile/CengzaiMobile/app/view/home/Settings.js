Ext.define('CengZai.view.home.Settings',{
	extend: 'Ext.Panel',
    xtype:'homesettings',
    
    requires: [
    ],
    
    config: {
    	title: '设置',
        iconCls: 'settings',

        styleHtmlContent: true,
        scrollable: true,

        items: {
            docked: 'top',
            xtype: 'titlebar',
            title: '设置'
        },

        html: [
            "设置"
        ].join("")
    }
})
