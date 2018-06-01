using System;
using System.IO;
using System.Windows;

namespace FileSaver
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		/// <summary>
		/// 
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if(IsDirectory())
			{
				string text = CopyFilesFromDirectory(from.Text, to.Text) ? "File or directory copied" : "Error !" ;
				MessageBox.Show(text);
			}
			else
			{
				try
				{
					File.Copy(from.Text, to.Text);
				}
				catch (DirectoryNotFoundException)
				{
					MessageBox.Show("File to copy or destination directory doesn't exist");
				}
				catch(FileNotFoundException)
				{
					MessageBox.Show("File to copy doesn't exist");
				}
				catch (Exception ex)
				{
					throw ex;
				}				
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		bool IsDirectory()
		{
			try
			{
				FileAttributes attr = File.GetAttributes(from.Text);

				return ((attr & FileAttributes.Directory) == FileAttributes.Directory);
			}
			catch (Exception ex)
			{
				throw ex;
			}			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sourcePath"></param>
		/// <param name="destinationPath"></param>
		/// <returns></returns>
		bool CopyFilesFromDirectory(string sourcePath, string destinationPath)
		{
			try
			{
				if (Directory.Exists(sourcePath))
				{
					if (!Directory.Exists(destinationPath))
					{
						Directory.CreateDirectory(destinationPath);
					}

					foreach (string file in Directory.GetFiles(sourcePath))
					{
						var fileInfo = new FileInfo(file);
						fileInfo.CopyTo(destinationPath + @"\" + fileInfo.Name, true);
					}

					foreach (string directory in Directory.GetDirectories(sourcePath))
					{
						var directoryInfo = new DirectoryInfo(directory);
						CopyFilesFromDirectory(directory, destinationPath + @"\" + directoryInfo.Name);
					}

					return true;
				}

				return false;
			}
			catch (Exception)
			{
				return false;
			}
			
		}
	}
}
