using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{

    //A Delegate is a reference or pointer to a function.
    //For designing extensible and flexible applications (eg frameworks)
    //Aletrnatively, we can use interfaces
    //system.Action and System.Func are tow built in generic delegates. The difference is that Func has an out parameter.
    //Interfaces or Delegates?
    //Use a delegate when:
    //1. An event driven design pattern is used.
    //2. The caller doesn't need to access other properties or methods on the object implementing the method.

    public class PhotoFilters
    {
        public void ApplyBrightness(Photo photo)
        {
            Console.WriteLine("Apply brightness to photo.");
        }

        public void ApplyContrast(Photo photo)
        {
            Console.WriteLine("Apply contrast to photo.");
        }

        public void Resize(Photo photo)
        {
            Console.WriteLine("Resize photo.");
        }
    }

    public class Photo
    {
        public static Photo Load(string path)
        {
            return new Photo();
        }

        public void Save()
        {
        }
    }

    public class PhotoProcessor
    {
        public delegate void PhotoFilterHandler(Photo photo);

        public void Process(string path, PhotoFilterHandler filterHandler)
        {
            Photo photo = Photo.Load(path);

            var filters = new PhotoFilters();

            //Instead of doing this, we create a delegate and add it as a parameter input to make the application more flexible
            //filters.ApplyBrightness(photo);
            //filters.ApplyContrast(photo);
            //filters.Resize(photo);

            filterHandler(photo);

            photo.Save();
        }

        //Or we could use Action
        public void Process(string path, Action<Photo> filterHandler)   //Can use any delegate that takes a photo as an argument
        {
            Photo photo = Photo.Load(path);

            var filters = new PhotoFilters();

            //Instead of doing this, we create a delegate and add it as a parameter input to make the application more flexible
            //filters.ApplyBrightness(photo);
            //filters.ApplyContrast(photo);
            //filters.Resize(photo);

            filterHandler(photo);

            photo.Save();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            PhotoProcessor processor = new PhotoProcessor();
            
            var filters = new PhotoFilters();
            PhotoProcessor.PhotoFilterHandler filterHandler = filters.ApplyBrightness;
            filterHandler += filters.ApplyContrast; //multicast delegate-

            processor.Process("photo.jpg", filterHandler);

            Action<Photo> filterhand = filters.ApplyBrightness;
            processor.Process("photo1.jpg", filterhand);
        }
    }
}
