using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Note.Resources;
using System.IO.IsolatedStorage;
using System.IO;

namespace Note
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            BuildLocalizedApplicationBar();
        }

        // ++++++++++++++++++++++++++++
        // Funkcja tworząca pasek aplikacji
        private void BuildLocalizedApplicationBar()
        {            
            ApplicationBar = new ApplicationBar();

            // ++++++++++++++++++++++++++++
            // Dodawanie przycisków
            ApplicationBarIconButton appBarButtonAdd = new ApplicationBarIconButton(new 
                        Uri("/Images/add.png", UriKind.Relative));
            appBarButtonAdd.Text = "dodaj";
            ApplicationBar.Buttons.Add(appBarButtonAdd);

            ApplicationBarIconButton appBarButtonDelete = new ApplicationBarIconButton(new
                        Uri("/Images/delete.png", UriKind.Relative));
            appBarButtonDelete.Text = "usuń";
            ApplicationBar.Buttons.Add(appBarButtonDelete);

            ApplicationBarIconButton appBarButtonEdit = new ApplicationBarIconButton(new
                        Uri("/Images/edit.png", UriKind.Relative));
            appBarButtonEdit.Text = "edytuj";
            ApplicationBar.Buttons.Add(appBarButtonEdit);
            
            appBarButtonAdd.Click += appBarButtonAdd_Click;
            appBarButtonDelete.Click += appBarButtonDelete_Click;
            appBarButtonEdit.Click += appBarButtonEdit_Click;
        }

        void appBarButtonEdit_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/EditPage.xaml", UriKind.Relative));
        }

        void appBarButtonDelete_Click(object sender, EventArgs e)
        {
            using (IsolatedStorageFile storage =
                IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (StreamWriter writer = new StreamWriter(storage.OpenFile(
                            "note.txt", FileMode.Create, FileAccess.ReadWrite)))
                {
                    writer.Write("");
                }
            }
            load();
        }

        void appBarButtonAdd_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/EditPage.xaml", UriKind.Relative));
        }

        void load()
        {
            // ++++++++++++++++++++++++++++
            // Wczytanie notatki
            using (IsolatedStorageFile storage = 
                        IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (StreamReader read = new StreamReader(storage.OpenFile
                        ("note.txt", FileMode.OpenOrCreate, FileAccess.Read)))
                {
                    NoteText.Text = read.ReadToEnd();
                }
            }

            // ++++++++++++++++++++++++++++
            // Ustalanie stanów przycisków
            ApplicationBarIconButton Addbutton = (ApplicationBarIconButton)
                                                    ApplicationBar.Buttons[0];
            ApplicationBarIconButton Deletebutton = (ApplicationBarIconButton)
                                                    ApplicationBar.Buttons[1];
            ApplicationBarIconButton Editbutton = (ApplicationBarIconButton)
                                                    ApplicationBar.Buttons[2];

            if (NoteText.Text == "")
            {
                Addbutton.IsEnabled = true;
                Deletebutton.IsEnabled = false;
                Editbutton.IsEnabled = false;
            }
            else
            {
                Addbutton.IsEnabled = false;
                Deletebutton.IsEnabled = true;
                Editbutton.IsEnabled = true;
            }

            // ++++++++++++++++++++++++++++
            // Wyświetlanie notatki na ekranie blokowania
            ShellTile.ActiveTiles.First().Update(
                new FlipTileData(){WideBackContent = NoteText.Text});
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            load();
        }

    }
}