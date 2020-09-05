using System;
using System.Collections.Generic;
using System.Text;
using Akov.DataGenerator.Attributes;

namespace Akov.DataGenerator.Models
{
    enum Variant
    {
        A,
        B,
        C
    }

    class Root
    {
        public int? Count { get; set; }

        [DgLength(Max = 3)]
        public List<Student> Students { get; set; } 
    }

 
    class Student
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [DgSource("firstnames.txt")]
        public string LastName { get; set; }
        public Variant Variant {get; set;}
        [DgIgnore]
        public Variant[] Variants { get; set; }
        public Subject Subject { get; set; }
        public Subject[] Subjects { get; set; }
        public List<int> Numbers { get; set; }

        public int[] Numbers2 { get; set; }
    }

    class Subject
    {
        public int[] Numbers2 { get; set; }
    }
}
