using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Abstractions;
using System.Linq;
using System.Windows;
using Scraps.Lib;
using Scraps.Model;
using Scraps.Model.Other;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Core;
using MessageBox = System.Windows.MessageBox;

namespace Scraps.UI.Windows
{
	public partial class RenamingWindow : INotifyPropertyChanged
	{
		private List<string> _namesBefore;
		private List<string> _namesAfter;

		public List<string> NamesBefore
		{
			get => _namesBefore;
			set
			{
				_namesBefore = value;
				RaisePropertyChanged(nameof(NamesBefore));
			}
		}

		public List<string> NamesAfter
		{
			get => _namesAfter;
			set
			{
				_namesAfter = value;
				RaisePropertyChanged(nameof(NamesAfter));
			}
		}

		public string Result
		{
			get => _result;
			set
			{
				_result = value;
				RaisePropertyChanged(nameof(Result));
			}
		}

		private readonly string _pattern;
		private readonly Dictionary<string, string> _tokens;
		private string _result;

		public RenamingWindow(Event @event)
		{
			NamesBefore = @event.PictureEvent.Select(x => x.PictureNavigation).Select(x => x.FullName).ToList();

			string rootFoler = new ScrapsSettings((Application.Current as App).Context.Settings).RootFoler;

			_pattern = $@"{rootFoler}\<event.name>";
			_tokens = new Dictionary<string, string>
			{
				{"event.name", @event.Name}
			};

			Dictionary<string, string> preview = FileHelper.SubstituteNames(NamesBefore, _pattern, _tokens);
			NamesAfter = preview.Values.ToList();

			InitializeComponent();
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{

		}

		private void OnNextPage(object sender, CancelRoutedEventArgs e)
		{
			var wizard = (Wizard) sender;

			if (wizard.CurrentPage.Equals(PreviewPage))
			{
				PerformRenaming(e);
			}
			
		}

		private void PerformRenaming(CancelRoutedEventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to continue?", "Scraps", MessageBoxButton.OKCancel, MessageBoxImage.Question) != MessageBoxResult.OK)
			{
				e.Cancel = true;
				return;
			}

			try
			{
				FileHelper.RenameFiles(new FileSystem(), NamesBefore, _pattern, _tokens);
				Result = "Operation successful :)";
			}
			catch (Exception exception)
			{
				Result = $"ERROR :(\n\n{exception.Message}\n\n{exception.StackTrace}";
			}
		}

		#region INotifyPropertyChanged Members

		/// <summary>
		/// Raised when a property on this object has a new value.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Raises this object's PropertyChanged event.
		/// </summary>
		/// <param name="propertyName">The property that has a new value.</param>
		protected virtual void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				var e = new PropertyChangedEventArgs(propertyName);
				handler(this, e);
			}
		}

		#endregion
	}
}
