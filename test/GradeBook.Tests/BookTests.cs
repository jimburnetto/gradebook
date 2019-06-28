using System;
using Xunit;


namespace GradeBook.Tests
{
    public class BookTests
    {
        [Fact]
        public void BookGradeBetweenZeroAnd100()
        {
            //arrange
            var book = new InMemoryBook("");
            //act
            
            var origMin= book.getStatistics().Low;
            book.AddGrade(105);
            ///assert
            Assert.Equal(origMin, book.getStatistics().Low);
           // Assert.Equal(double.MinValue, book.getStatistics().Low,1);
        }
        [Fact]
        public void BookCalculatesAnAverageGrade()
        {
            //arrange
            var book = new InMemoryBook("");
            book.AddGrade(89.1);
            book.AddGrade(90.5);
            book.AddGrade(77.3);
            //act
            var result = book.getStatistics();

            //assert
            Assert.Equal(85.6, result.Average, 1);
            Assert.Equal(90.5, result.High,1);
            Assert.Equal(77.3, result.Low,1);
            Assert.Equal('B', result.Letter);
               
        }
    }
}
