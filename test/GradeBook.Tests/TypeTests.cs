using System;
using Xunit;


namespace GradeBook.Tests
{
    
    public delegate string WriteLogDelegate(string logMessage);
    
    public class TypeTests
    {
        int count = 0;
        [Fact]
        public void WriteLogDelegateCanPointToMethod(){
            WriteLogDelegate log = ReturnMessage;
            log += ReturnMessage;
            log += IncrementCount;
            //log = new WriteLogDelegate(ReturnMessage);
            
            var result = log("Hello!");
            
            Assert.Equal(3,count);

        }


        string IncrementCount(string message){
            count++;
            return message.ToLower();
        }
        string ReturnMessage(string message){
            count++;
            return message;
        }
        [Fact]
        public void StringsBehaveLikeValueTypes()
        {
            string name = "Jim";
            var upper = MakeUppercase(name);

            Assert.Equal("Jim",name);
            Assert.Equal("JIM",upper);

        }

        private string MakeUppercase(string parameter)
        {
           return parameter.ToUpper();
        }

        [Fact]
        public void ValueTypeAlsoPassByValue(){
            //arrange
            var x = GetInt();
            //act
            SetInt(ref x);
            //assert
            Assert.Equal(42,x);
        }

        private void  SetInt(ref int z)
        {


             z = 42;
        }

        
        private int GetInt()
        {
            return 3;
        }

              

        [Fact]
        public void CSharpCanPassByRef()
        {
            //arrange
            var book1 = GetBook("Book 1");
            
            //act
            GetBookSetName(ref book1,"New Name");  //use modifier out

            //assert
            Assert.Equal("New Name", book1.Name);
               
        }

        private void GetBookSetName(ref InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }
        
        [Fact]
        public void CSharpIsPassByValue()
        {
            //arrange
            var book1 = GetBook("Book 1");
            
            //act
            GetBookSetName(book1,"New Name");

            //assert
            Assert.Equal("Book 1", book1.Name);
               
        }

        private void GetBookSetName(InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
            book.Name = name;
        }
        [Fact]
        public void CanSetNameFromReference()
        {
            //arrange
            var book1 = GetBook("Book 1");
            
            //act
            SetName(book1,"New Name");

            //assert
            Assert.Equal("New Name", book1.Name);
               
        }

        private void SetName(InMemoryBook book, string name)
        {
            book.Name = name;
        }

        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            //arrange
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");
            //act
            

            //assert
            Assert.Equal("Book 1",book1.Name);
            Assert.Equal("Book 2",book2.Name);
            Assert.NotSame(book1,book2);
               
        }
        [Fact]
        public void TwoVariablesCanReferenceSameObject()
        {
            //arrange
            var book1 = GetBook("Book 1");
            var book2 = book1;

            //act
            

            //assert
            Assert.Same(book1,book2);
            Assert.True(Object.ReferenceEquals(book1,book2));  //values in these variables point to the same object
               
        }

        InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }
    }
}
