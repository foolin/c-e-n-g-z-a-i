Ext.define("CengZai.view.Main", {
    extend: 'Ext.Panel',
    
    requires: [
    ],
    config: {
    	layout: 'card',
        items: [
        	{
        		xtype:'login'
        	},
        	{
        		title:'home',
        		html:'home'
        	}
        ]
    }
});
