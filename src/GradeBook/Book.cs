using System;
using System.Collections.Generic;

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

        public virtual event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);

        public virtual Statistics getStatistics()
        {
            throw new NotImplementedException();
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
            result.Average = 0.0;
            result.Low = double.MaxValue;
            result.High = double.MinValue;

            // foreach (var grade in grades)
            // {
            //     result.High = Math.Max(grade, result.High);
            //     result.Low = Math.Min(grade, result.Low );
            //     result.Average += grade;
            // }
            
            
            for(var index =0; index<grades.Count; index++)
            {
                result.High = Math.Max(grades[index], result.High);
                result.Low = Math.Min(grades[index], result.Low );
                result.Average += grades[index];
            }
            
            result.Average /= grades.Count;
            switch(result.Average)
            {
                case var d when d >= 90.0:
                    result.Letter = 'A';
                    break;
                case var d when d >= 80.0:
                    result.Letter = 'B';
                    break;
                case var d when d >= 70.0:
                    result.Letter = 'C';
                    break;
                case var d when d >= 60.0:
                    result.Letter = 'D';
                    break;
                default:
                    result.Letter = 'F';
                    break;
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
