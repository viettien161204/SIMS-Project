using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SIMS_SE06206.Models;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using SIMS_SE06206.Controllers;



namespace SIMS_SE06204.Controllers.Tests
{
    public class StudentControllerTests
    {
        [Fact]
        public void TestLoadStudentsFromJson()
        {
            // Arrange
            string filePath = @"C:\Users\Gigabyte\Downloads\APDP-BTec-main\APDP-BTec-main\data-sims\data-student.json";
            StudentController controller = new StudentController();

            // Act
            IActionResult result = controller.Index();
            var viewResult = result as ViewResult;
            List<StudentViewModel> students = (List<StudentViewModel>)viewResult.Model;

            // Assert
            Assert.NotNull(students);
            Assert.Equal(3, students.Count); // Assuming the JSON file contains 3 student records

            // Verify the first student's data
            StudentViewModel firstStudent = students[0];
            Assert.Equal("John Doe", firstStudent.firstName + " " + firstStudent.lastName);
            Assert.Equal("john.doe@example.com", firstStudent.email);
            Assert.Equal("123 Main St", firstStudent.address);
            Assert.Equal("555-1234", firstStudent.phone);
            Assert.Equal("Male", firstStudent.gender);
            
        }
        [Fact]
        public void TestAddStudent()
        {
            // Arrange
            var controller = new StudentController();
            var newStudent = new StudentViewModel
            {
                firstName = "John",
                lastName = "Doe",
                email = "john.doe@example.com"
            };

            // Act
            var result = controller.Add(newStudent) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Student", result.ControllerName);
        }
        

    }
}