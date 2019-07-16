using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OpenALPR_MVC_Project.Models
{
    [Table("Cars")]
    public class Car
    {
        //Set automatically when images uploaded
        [Key]
        public int Key { get; set; }
        public string Plate { get; set; }
        public string Timestamp { get; set; }
        public int NumberOffense { get; set; }
        public byte[] FrontPicture { get; set; }
        public byte[] BackPicture { get; set; }
        //Must be set manually by user
        public int GotTicketed { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }

        public Car() { } //Default constructor

        //Constructor used when POST call made
        public Car(string make, string model, string color, string platenumber, string fronturl, string backurl)
        {
            //Set properties from API call
            Make = make;
            Model = model;
            Color = color;
            Timestamp = DateTime.Now.ToString(); //Current time as timestamp
            Plate = platenumber;
            GotTicketed = 0; // False
            //Get image blobs
            using (WebClient w = new WebClient())
            {
                FrontPicture = w.DownloadData(fronturl);
                BackPicture = w.DownloadData(backurl);
            }
        }
    }

    //Class made so that a list of cars can be returned without massive image data
    public class CarSansPictures
    {
        public int Key { get; set; }
        public string Plate { get; set; }
        public string Timestamp { get; set; }
        public int NumberOffense { get; set; }
        public int GotTicketed { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }

        public CarSansPictures(Car c)
        {
            Key = c.Key;
            Plate = c.Plate;
            Timestamp = c.Timestamp;
            NumberOffense = c.NumberOffense;
            GotTicketed = c.GotTicketed;
            Make = c.Make;
            Model = c.Model;
            Color = c.Color;
        }
    }
}