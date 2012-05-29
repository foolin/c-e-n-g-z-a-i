Ext.define('CengZai.view.home.Index',{
	extend: 'Ext.navigation.View',
    xtype:'homeindex',
    id:'homeindex',
    
    requires: [
    	'Ext.dataview.NestedList',
    	'Ext.data.proxy.JsonP'
    ],
    
    config: {
    	title: '首页',
        iconCls: 'home',

        styleHtmlContent: true,
        scrollable: false,

        items: [
        	/*
	        {
	            docked: 'top',
	            xtype: 'titlebar',
	            title: '曾在网首页'
	        },
	        */
            { 
            	title: APP.NAME + '首页',

				xtype:'list',
				itemTpl: '<div>{name}<br />{uid}</div>',
			    data: [
			        { name: '周杰伦', uid:'90001', intro:'你开心，我快乐' },
			        { name: '蔡一丽', uid:'90002', intro:'你快乐，我开心' },
			        { name: '陈冠希', uid:'90003', intro:'哈哈哈，我就喜欢自拍' },
			        { name: '钟欣桐', uid:'90004', intro:'呵呵，冠希哥，俺支持你'  },
			        { name: '周杰伦', uid:'90001', intro:'你开心，我快乐' },
			        { name: '蔡一丽', uid:'90002', intro:'你快乐，我开心' },
			        { name: '陈冠希', uid:'90003', intro:'哈哈哈，我就喜欢自拍' },
			        { name: '钟欣桐', uid:'90004', intro:'呵呵，冠希哥，俺支持你'  },
			        { name: '周杰伦', uid:'90001', intro:'你开心，我快乐' },
			        { name: '蔡一丽', uid:'90002', intro:'你快乐，我开心' },
			        { name: '陈冠希', uid:'90003', intro:'哈哈哈，我就喜欢自拍' },
			        { name: '钟欣桐', uid:'90004', intro:'呵呵，冠希哥，俺支持你'  },
			        { name: '周杰伦', uid:'90001', intro:'你开心，我快乐' },
			        { name: '蔡一丽', uid:'90002', intro:'你快乐，我开心' },
			        { name: '陈冠希', uid:'90003', intro:'哈哈哈，我就喜欢自拍' },
			        { name: '钟欣桐', uid:'90004', intro:'呵呵，冠希哥，俺支持你'  },
			        { name: '周杰伦', uid:'90001', intro:'你开心，我快乐' },
			        { name: '蔡一丽', uid:'90002', intro:'你快乐，我开心' },
			        { name: '陈冠希', uid:'90003', intro:'哈哈哈，我就喜欢自拍' },
			        { name: '钟欣桐', uid:'90004', intro:'呵呵，冠希哥，俺支持你'  },
			        { name: '周杰伦', uid:'90001', intro:'你开心，我快乐' },
			        { name: '蔡一丽', uid:'90002', intro:'你快乐，我开心' },
			        { name: '陈冠希', uid:'90003', intro:'哈哈哈，我就喜欢自拍' },
			        { name: '钟欣桐', uid:'90004', intro:'呵呵，冠希哥，俺支持你'  },
			        { name: '周杰伦', uid:'90001', intro:'你开心，我快乐' },
			        { name: '蔡一丽', uid:'90002', intro:'你快乐，我开心' },
			        { name: '陈冠希', uid:'90003', intro:'哈哈哈，我就喜欢自拍' },
			        { name: '钟欣桐', uid:'90004', intro:'呵呵，冠希哥，俺支持你'  },
			        { name: '周杰伦', uid:'90001', intro:'你开心，我快乐' },
			        { name: '蔡一丽', uid:'90002', intro:'你快乐，我开心' },
			        { name: '陈冠希', uid:'90003', intro:'哈哈哈，我就喜欢自拍' },
			        { name: '钟欣桐', uid:'90004', intro:'呵呵，冠希哥，俺支持你'  },
			        { name: '周杰伦', uid:'90001', intro:'你开心，我快乐' },
			        { name: '蔡一丽', uid:'90002', intro:'你快乐，我开心' },
			        { name: '陈冠希', uid:'90003', intro:'哈哈哈，我就喜欢自拍' },
			        { name: '钟欣桐', uid:'90004', intro:'呵呵，冠希哥，俺支持你'  },
			        { name: '周杰伦', uid:'90001', intro:'你开心，我快乐' },
			        { name: '蔡一丽', uid:'90002', intro:'你快乐，我开心' },
			        { name: '陈冠希', uid:'90003', intro:'哈哈哈，我就喜欢自拍' },
			        { name: '钟欣桐', uid:'90004', intro:'呵呵，冠希哥，俺支持你'  },
			        { name: '周杰伦', uid:'90001', intro:'你开心，我快乐' },
			        { name: '蔡一丽', uid:'90002', intro:'你快乐，我开心' },
			        { name: '陈冠希', uid:'90003', intro:'哈哈哈，我就喜欢自拍' },
			        { name: '钟欣桐', uid:'90004', intro:'呵呵，冠希哥，俺支持你'  },
			        { name: '周杰伦', uid:'90001', intro:'你开心，我快乐' },
			        { name: '蔡一丽', uid:'90002', intro:'你快乐，我开心' },
			        { name: '陈冠希', uid:'90003', intro:'哈哈哈，我就喜欢自拍' },
			        { name: '钟欣桐', uid:'90004', intro:'呵呵，冠希哥，俺支持你'  },
			        { name: '周杰伦', uid:'90001', intro:'你开心，我快乐' },
			        { name: '蔡一丽', uid:'90002', intro:'你快乐，我开心' },
			        { name: '陈冠希', uid:'90003', intro:'哈哈哈，我就喜欢自拍' },
			        { name: '钟欣桐', uid:'90004', intro:'呵呵，冠希哥，俺支持你'  },
			        { name: '周杰伦', uid:'90001', intro:'你开心，我快乐' },
			        { name: '蔡一丽', uid:'90002', intro:'你快乐，我开心' },
			        { name: '陈冠希', uid:'90003', intro:'哈哈哈，我就喜欢自拍' },
			        { name: '钟欣桐', uid:'90004', intro:'呵呵，冠希哥，俺支持你'  },
			        { name: '周杰伦', uid:'90001', intro:'你开心，我快乐' },
			        { name: '蔡一丽', uid:'90002', intro:'你快乐，我开心' },
			        { name: '陈冠希', uid:'90003', intro:'哈哈哈，我就喜欢自拍' },
			        { name: '钟欣桐', uid:'90004', intro:'呵呵，冠希哥，俺支持你'  },
			        { name: '周杰伦', uid:'90001', intro:'你开心，我快乐' },
			        { name: '蔡一丽', uid:'90002', intro:'你快乐，我开心' },
			        { name: '陈冠希', uid:'90003', intro:'哈哈哈，我就喜欢自拍' },
			        { name: '钟欣桐', uid:'90004', intro:'呵呵，冠希哥，俺支持你'  },
			        { name: '周杰伦', uid:'90001', intro:'你开心，我快乐' },
			        { name: '蔡一丽', uid:'90002', intro:'你快乐，我开心' },
			        { name: '陈冠希', uid:'90003', intro:'哈哈哈，我就喜欢自拍' },
			        { name: '钟欣桐', uid:'90004', intro:'呵呵，冠希哥，俺支持你'  },
			        { name: '周杰伦', uid:'90001', intro:'你开心，我快乐' },
			        { name: '蔡一丽', uid:'90002', intro:'你快乐，我开心' },
			        { name: '陈冠希', uid:'90003', intro:'哈哈哈，我就喜欢自拍' },
			        { name: '钟欣桐', uid:'90004', intro:'呵呵，冠希哥，俺支持你'  },
			        { name: '周杰伦', uid:'90001', intro:'你开心，我快乐' },
			        { name: '蔡一丽', uid:'90002', intro:'你快乐，我开心' },
			        { name: '陈冠希', uid:'90003', intro:'哈哈哈，我就喜欢自拍' },
			        { name: '钟欣桐', uid:'90004', intro:'呵呵，冠希哥，俺支持你'  },
			    ],
			    listeners :{
			    	itemtap: function(_this, index, target, record , e){
			    		Ext.getCmp('homeindex').push({
			    			xtype:'panel',
			    			title:record.get('name'),
			    			tpl:'姓名：{name}<br />恋号：{uid}<br />{intro}',
			    			data:record.getData(),
			    			scrollable: true,
			    		})
			    	}
			    },
			    
			    
            }
        ]
    }
})
