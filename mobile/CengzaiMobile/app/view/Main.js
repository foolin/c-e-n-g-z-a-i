Ext.define("CengZai.view.Main", {
    extend: 'Ext.Panel',
    xtype:'main',
    id:'main',
    
    requires: [
    ],
    config: {
    	ui:'dark',
    	layout: 'card',
        items: [
        	{
        		xtype:'login'
        	}
        	,
        	{
        		xtype:'home'
        	}
        ]
    }
});
