using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections;
using System.Windows.Input;
using DAL;
using System.ComponentModel.Composition;



namespace DAL.ViewModels
{
   
    public class ListboxPersonenKategorienViewModel:INotifyPropertyChanged 
    {
        SteinbachEntities db;
        ObservableCollection<firma> _selectedNames = new ObservableCollection<firma>();
        ObservableCollection<Personen_Kategorien> fk;
        Firmen_Personen CurrentPerson;

        List<firma> _AvailableNames;

        public ListboxPersonenKategorienViewModel(Firmen_Personen person, SteinbachEntities db)
        {
            _selectedNames.CollectionChanged += (sender, e) => UpdateCollection(sender, e);
            this.db = db;
            CurrentPerson = person;
            _AvailableNames = db.firmen.Where(f => f.istFirma == 1).ToList();



        }

        void UpdateCollection(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    {
                        if (e.NewItems != null && e.NewItems.Count > 0)
                        {
                            foreach (firma item in e.NewItems)
                            {
                                if (fk.Where(k => k.id_Kategorie == item.id).Count() == 0)
                                {
                                    var k = new Personen_Kategorien();
                                    k.id_Kategorie = item.id;
                                    k.Firmen_Personen = CurrentPerson;


                                    fk.Add(k);
                                    db.AddToPersonen_Kategorien(k);
                                    db.SaveChanges();

                                }

                            }
                        }

                        break;
                    }

                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    {
                        if (e.OldItems != null && e.OldItems.Count > 0)
                        {
                            foreach (firma item in e.OldItems)
                            {
                                if (fk.Where(k => k.id_Kategorie == item.id).Count() == 1)
                                {
                                    var k = fk.Where(ka => ka.id_Kategorie == item.id).SingleOrDefault();
                                    fk.Remove(k);
                                    db.DeleteObject(k);
                                    db.SaveChanges();
                                }
                            }
                        }
                        break;
                    }


                default:
                    {
                        break;
                    }

            }

        }




        public void AddSelectedNames()
        {

            fk = new ObservableCollection<Personen_Kategorien>(db.Personen_Kategorien.Where(k => k.id_Person == CurrentPerson.id));
            if (fk.Count > 0)
            {
                SelectedNames.Clear();
                foreach (var item in fk)
                {

                    SelectedNames.Add(item.firma);
                }
                OnPropertyChanged("SelectedNames");
            }

        }



        public IEnumerable<firma> AvailableNames
        {
            get
            {

                return _AvailableNames;
            }
        }




        public ObservableCollection<firma> SelectedNames
        {
            get
            {
                return _selectedNames;
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


    }
}
