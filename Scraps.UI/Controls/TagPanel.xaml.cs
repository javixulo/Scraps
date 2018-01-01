using System.Windows;
using System.Windows.Controls;

namespace Scraps.UI.Controls
{
	public partial class TagPanel : UserControl
	{
		public static readonly DependencyProperty TagsProperty = DependencyProperty.Register("Tags", typeof(TagsCollection), typeof(TagPanel));

		public TagsCollection Tags
		{
			get => (TagsCollection)GetValue(TagsProperty);
			set => SetValue(TagsProperty, value);
		}

		public TagPanel()
		{
			InitializeComponent();

			Tags = new TagsCollection();
		}

		public void AddTag(string text, object obj = null)
		{
			Tags.Add(new TagControl { Text = text, Object = obj });
		}
	}
}
