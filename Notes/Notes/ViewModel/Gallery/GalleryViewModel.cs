using MvvmHelpers;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.ViewModel
{
    public class GalleryViewModel
    {
        public ObservableRangeCollection<Gallery> GaleryList { get; set; }

        public GalleryViewModel()
        {
            GaleryList = new ObservableRangeCollection<Gallery>();
            Gallery gallery = new Gallery() { Name = "First", ImageUrl= "https://atmousphere.hb.bizmrg.com/saga.jpg" };
            Gallery gallery1 = new Gallery() { Name = "Second", ImageUrl= "https://atmousphere.hb.bizmrg.com/saga.jpg" };
            Gallery gallery2 = new Gallery() { Name = "Third", ImageUrl= "https://atmousphere.hb.bizmrg.com/saga.jpg" };
            Gallery gallery3 = new Gallery() { Name = "Quad", ImageUrl= "https://atmousphere.hb.bizmrg.com/saga.jpg" };
            Gallery gallery4 = new Gallery() { Name = "5th", ImageUrl= "https://atmousphere.hb.bizmrg.com/saga.jpg" };
            Gallery gallery5 = new Gallery() { Name = "6th", ImageUrl= "https://atmousphere.hb.bizmrg.com/saga.jpg" };
            GaleryList.Add(gallery);
            GaleryList.Add(gallery1);
            GaleryList.Add(gallery2);
            GaleryList.Add(gallery3);
            GaleryList.Add(gallery4);
            GaleryList.Add(gallery5);
        }
    }
}
