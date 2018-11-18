using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Movie
    {

        public int MovieId { get; set; }

        public string Title { get; set; }

        public string Director { get; set; }
        
        public int Length { get; set; }

        public string Img { get; set; }
    }
}
