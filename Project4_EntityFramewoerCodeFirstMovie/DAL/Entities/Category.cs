using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4_EntityFramewoerCodeFirstMovie.DAL.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
/*
 list,ICOLLECTİON,IQuerytable arasında ki fark nedir makale yaz*/
