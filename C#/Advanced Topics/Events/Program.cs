using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Events
{
    //Events
    //A mechanism for communication between objects
    //Used in building loosely coupled applications
    //Helps extending applications
    //publisher - subscriber concept
    //Uses delegates

    public class Video
    {
        public string Title { get; set; }
    }

    public class VideoEncoder
    {
        public void Encode(Video video)
        {
            Console.WriteLine("Encoding video...");
            Thread.Sleep(2000);

            //Now say we want to notify when the encoding is completed,
            //we can use an event.
            //Three steps:
            //1 - Define a delegate
            //2 - Define an event based on that delegate
            //3 - Raise the event

            //Raise event:
            //OnVideoEncoded();
            OnVideoEncoded(video);
        }

        //I wrote the code here for lesson readability
        //public delegate void VideoEncodedEventHandler(object source, EventArgs args);
        //public delegate void VideoEncodedEventHandler(object source, VideoEventArgs args);
        //public event VideoEncodedEventHandler VideoEncoded;
        //OR with less code, use EventHandler class
        public EventHandler<VideoEventArgs> VideoEncoded;

        //protected virtual void OnVideoEncoded()
        //{
        //    if (VideoEncoded != null)
        //        VideoEncoded(this, EventArgs.Empty);
        //}

        //if you want to use event args
        protected virtual void OnVideoEncoded(Video video)
        {
            if (VideoEncoded != null)
                VideoEncoded(this, new VideoEventArgs() { Video = video });
        }
    }

    //if you want to use event args
    public class VideoEventArgs : EventArgs
    {
        public Video Video { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var video = new Video { Title = "Dune" };
            var encoder = new VideoEncoder();       //publisher of event
            var mailService = new MailService();    //Subcriber of event

            encoder.VideoEncoded += mailService.OnVideoEncoded;
            //now we can extend this to sms notifications without
            //modifying the encoder/publisher class
            var msgService = new MessageService();    //Subcriber of event
            encoder.VideoEncoded += msgService.OnVideoEncoded;

            encoder.Encode(video);
        }
    }

    public class MailService
    {
        public void OnVideoEncoded(object source, EventArgs args)
        {
            Console.WriteLine("Email notification sent");
        }
    }

    public class MessageService
    {
        public void OnVideoEncoded(object source, EventArgs args)
        {
            Console.WriteLine("SMS notification sent");
        }
    }
}
