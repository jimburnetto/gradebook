using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{
    
    
    
    //public delegate void GradeAddedDelegate(double grade); //valid but not common.
    
    public delegate void GradeAddedDelegate(object sender, EventArgs args); //common onvention
    
    public class NamedObject
    {
        public NamedObject(string name)
        {
            Name = name;
        }
        public string Name{
            get; set;
        }
    }
    public interface IBook
    {
        void AddGrade(double grade );
        Statistics getStatistics();
        string Name{get;}
        event GradeAddedDelegate GradeAdded;
    }

    public abstract class Book :NamedObject, IBook
    {
        public Book(string name): base(name)
        {
            
        }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);

        public abstract Statistics getStatistics();
        
    }
     public class DiskBook : Book, IBook
    {
        public DiskBook(string name) : base(name)
        {
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            
            //File.AppendAllText(Name + ".txt", grade.ToString());
            
            // var writer = File.AppendText($"{Name}.txt");
            // writer.WriteLine(grade);
            // writer.Dispose(); //instead of close.  either or
            
            using ( var writer = File.AppendText($"{Name}.txt"))
            {
                writer.WriteLine(grade);
                if(GradeAdded != null){
                    GradeAdded(this, new EventArgs());
                }
            }
            
            
            

        }

        public override Statistics getStatistics()
        {
            var result = new Statistics();
            using(var reader = File.OpenText($"{Name}.txt"))
            {
                
                var line = reader.ReadLine();
                while (line != null){
                    var number = double.Parse(line);
                    result.Add(number);
                    line = reader.ReadLine();
                }

            }

            return result;
        }
    }
    
    
    
    public class InMemoryBook : Book, IBook
    {
        public InMemoryBook(string name) : base(name)
        {
            
            grades = new List<double>();   
            Name = name;            
//            category = "";        
        }
        public void AddLetterGrade(char letter){
            switch(letter)
            {
                case 'A' : 
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(80);
                    break;
                case 'C':
                    AddGrade(70);
                    break;
                case 'D':
                    AddGrade(60);
                    break;
                default:
                    AddGrade(0);
                    break;
            }
                
            
        }

        public override void AddGrade(double grade)
        {
            
            if(grade<=100 && grade >= 0)
            {
                grades.Add(grade);    
                if(GradeAdded != null){ //ie. someone is listening for event
                    GradeAdded(this,new EventArgs());
                }
            }else{
              System.Console.WriteLine( "Invalid value");
              //throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        public override event GradeAddedDelegate GradeAdded;
        public override Statistics getStatistics()
        {
            var result = new Statistics();


            // foreach (var grade in grades)
            // {
            //     result.High = Math.Max(grade, result.High);
            //     result.Low = Math.Min(grade, result.Low );
            //     result.Average += grade;
            // }
            
            
            for(var index =0; index<grades.Count; index++)
            {
                result.Add(grades[index]);
            }
            
            

            return result;

        }

        private List<double> grades = new List<double>();          
        
        // public string Name
        // {
        //     get
        //     {
        //         return name;
        //     }
        //     set
        //     {
        //         if (!String.IsNullOrEmpty(value))
        //         {
        //             name = value;
        //         }
        //     }
        // }
        const string category = "Science";
    }
}
