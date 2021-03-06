﻿using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using AGS.API;

namespace AGS.Engine
{
    public class InspectorTreeNodeProvider : ITreeNodeViewProvider
    {
        private ITreeNodeViewProvider _provider;
        private IGameFactory _factory;
        private static int _nextNodeId;

        public InspectorTreeNodeProvider(ITreeNodeViewProvider provider, IGameFactory factory)
        {
            _provider = provider;
            _factory = factory;
        }

        public void BeforeDisplayingNode(ITreeStringNode item, ITreeNodeView nodeView, bool isCollapsed, bool isHovered, bool isSelected)
        {
            _provider.BeforeDisplayingNode(item, nodeView, isCollapsed, isHovered, isSelected);
        }

        public ITreeNodeView CreateNode(ITreeStringNode item, IRenderLayer layer)
        {
            var view = _provider.CreateNode(item, layer);
            var node = item as IInspectorTreeNode;
            if (node == null) return view;

            var layout = view.HorizontalPanel.GetComponent<IStackLayoutComponent>();
            layout.AbsoluteSpacing = 10f;
			int nodeId = Interlocked.Increment(ref _nextNodeId);
			var itemTextId = (item.Text ?? "") + "_" + nodeId;
            node.Editor.AddEditorUI("InspectorEditor_" + itemTextId, view, node.Property);

            var propertyChanged = node.Property.Object as INotifyPropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged.PropertyChanged += (sender, e) => 
                {
                    if (e.PropertyName != node.Property.Name) return;
                    node.Property.Refresh();
                    node.Editor.RefreshUI();
                };
            }

            return view;
        }
    }
}
