using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.IO;

namespace Note
{
    public partial class EditPage : PhoneApplicationPage
    {
        public EditPage()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();
        }

        private void BuildLocalizedApplicationBar()
        {
            ApplicationBar = new ApplicationBar();

            // ++++++++++++++++++++++++++++
            // Dodawanie przycisków
            ApplicationBarIconButton appBarButtonSave = new ApplicationBarIconButton(new
                        Uri("/Images/save.png", UriKind.Relative));
            appBarButtonSave.Text = "zapisz";
            ApplicationBar.Buttons.Add(appBarButtonSave);
            appBarButtonSave.Click += appBarButtonSave_Click;
        }

        void appBarButtonSave_Click(object sender, EventArgs e)
        {
            using (IsolatedStorageFile storage = 
                IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (StreamWriter writer = new StreamWriter(storage.OpenFile(
                            "note.txt", FileMode.Create, FileAccess.ReadWrite)))
                {
                    writer.Write(EditText.Text);
                }
            }
            NavigationService.GoBack();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            // ++++++++++++++++++++++++++++
            // Wczytanie notatki
            using (IsolatedStorageFile storage =
                        IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (StreamReader read = new StreamReader(storage.OpenFile
                        ("note.txt", FileMode.OpenOrCreate, FileAccess.Read)))
                {
                    EditText.Text = read.ReadToEnd();
                }
            }

            EditText.Focus();
        }
    }
}