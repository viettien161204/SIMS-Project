using Microsoft.AspNetCore.Mvc;
using SIMS_SE06206.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace SIMS_SE06206.Controllers
{
    public class StudentController : Controller
    {
        private string filePathStudent = @"C:\Users\Gigabyte\Downloads\APDP-BTec-main\APDP-BTec-main\data-sims\data-student.json";

        [HttpGet]
        public IActionResult Index()
        {
            string dataJson = System.IO.File.ReadAllText(filePathStudent);
            StudentModel model = new StudentModel();
            model.StudentsList = new List<StudentViewModel>();

            var students = JsonConvert.DeserializeObject<List<StudentViewModel>>(dataJson);
            var dataStudents = (from s in students select s).ToList();
            foreach (var item in dataStudents)
            {
                model.StudentsList.Add(new StudentViewModel
                {
                    Id = item.Id,
                    code = item.code,
                    firstName = item.firstName,
                    lastName = item.lastName,
                    email = item.email,
                    address = item.address,
                    phone = item.phone,
                    gender = item.gender,
                    birthday = item.birthday,
                });
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Add()
        {
            StudentViewModel model = new StudentViewModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string dataJson = System.IO.File.ReadAllText(filePathStudent);
                    var student = JsonConvert.DeserializeObject<List<StudentViewModel>>(dataJson);
                    int maxId = 0;
                    if (student != null)
                    {
                        maxId = int.Parse((from s in student select s.Id).Max()) + 1;
                    }
                    string idIncrement = maxId.ToString();

                    student.Add(new StudentViewModel
                    {
                        Id = idIncrement,
                        code = model.code,
                        firstName = model.firstName,
                        lastName = model.lastName,
                        email = model.email,
                        address = model.address,
                        phone = model.phone,
                        gender = model.gender,
                        birthday = model.birthday,
                    });
                    var dtJson = JsonConvert.SerializeObject(student, Formatting.Indented);
                    System.IO.File.WriteAllText(filePathStudent, dtJson);
                    TempData["saveStatus"] = true;
                }
                catch
                {
                    TempData["saveStatus"] =  false;
                }
                return RedirectToAction(nameof(StudentController.Index), "Student");
            }
            return View(model);

        }
        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            try
            {
                string dataJson = System.IO.File.ReadAllText(filePathStudent);
                var student = JsonConvert.DeserializeObject<List<StudentViewModel>>(dataJson);
                var itemToDelete = student.Find(item => item.Id == id.ToString());
                if (itemToDelete != null)
                {
                    student.Remove(itemToDelete);
                    string deletedJson = JsonConvert.SerializeObject(student, Formatting.Indented);
                    System.IO.File.WriteAllText(filePathStudent, deletedJson);
                    TempData["DeleteStatus"] = true;
                }
                else
                {
                    TempData["DeleteStatus"] = false;
                }
            }
            catch
            {
                TempData["DeleteStatus"] = false;
            }
            return RedirectToAction(nameof(StudentController.Index), "Student");
        }

        [HttpGet]
        public IActionResult Update(int id = 0)
        {
            string dataJson = System.IO.File.ReadAllText(filePathStudent);
            var student = JsonConvert.DeserializeObject<List<StudentViewModel>>(dataJson);
            var itemStudent = student.Find(item => item.Id == id.ToString());

            StudentViewModel model = new StudentViewModel();

            if (itemStudent != null)
            {
                model.Id = itemStudent.Id;
                model.code = itemStudent.code;
                model.firstName = itemStudent.firstName;
                model.lastName = itemStudent.lastName;
                model.email = itemStudent.email;
                model.phone = itemStudent.phone;
                model.address = itemStudent.address;
                model.gender = itemStudent.gender;
                model.birthday = itemStudent.birthday;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(StudentViewModel model)
        {
            try
            {
                string dataJson = System.IO.File.ReadAllText(filePathStudent);
                var student = JsonConvert.DeserializeObject<List<StudentViewModel>>(dataJson);
                var itemStudent = student.Find(item => item.Id == model.Id.ToString());

                if (itemStudent != null)
                {
                    itemStudent.code = model.code;
                    itemStudent.firstName = model. firstName;
                    itemStudent.lastName = model.lastName;
                    itemStudent.email = model.email;
                    itemStudent.phone = model.phone;
                    itemStudent.address= model.address;
                    itemStudent.gender = model.gender;
                    itemStudent.birthday = model.birthday;

                    string updateJson = JsonConvert.SerializeObject(student, Formatting.Indented);
                    System.IO.File.WriteAllText(filePathStudent, updateJson);
                    TempData["UpdateStatus"] = true;
                }
                else
                {
                    TempData["UpdateStatus"] = false;
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateStatus"] = false;
            }
            return RedirectToAction(nameof(StudentController.Index), "Student");
        }






    }
}
