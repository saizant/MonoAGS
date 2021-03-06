﻿using System;
using AGS.API;

namespace AGS.Engine
{
    public class InspectorTreeNode : AGSComponent, IInspectorTreeNode, ICustomSearchItem
    {
		private readonly IStringItem _item;

		public InspectorTreeNode(InspectorProperty property, IInspectorPropertyEditor editor)
		{
			TreeNode = new AGSTreeNode<ITreeStringNode>(this);
			_item = new AGSStringItem();
            Text = property.Name;
            Property = property;
            Editor = editor;
		}

		public ITextConfig HoverTextConfig { get => _item.HoverTextConfig; set => _item.HoverTextConfig = value; }

		public ITextConfig IdleTextConfig { get => _item.IdleTextConfig; set => _item.IdleTextConfig = value; }

		public string Text { get => _item.Text; set => _item.Text = value; }

        public ICustomProperties Properties => _item.Properties;

        public ITreeNode<ITreeStringNode> TreeNode { get; }

        public InspectorProperty Property { get; }

        public IInspectorPropertyEditor Editor { get; }

        public bool Contains(string searchText)
        {
            string text = Text?.ToLowerInvariant() ?? "";
            if (text.Contains(searchText)) return true;
            text = Property.Value?.ToLowerInvariant() ?? "";
            return text.Contains(searchText);
        }
    }
}
