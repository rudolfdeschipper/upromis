using Microsoft.VisualStudio.TestTools.UnitTesting;
using uPromis.APIUtils.Copier;


namespace uPromis.APIUtils.Tests
{

    public class TestPerson
    {
        [ClassCopier(typeof(TestEmployee), "Name")]
        public string Name { get; set; }
        [ClassCopier(typeof(TestEmployee), "Birthdate")]
        public System.DateTime DOB { get; set; }
        [ClassCopier(typeof(TestEmployee), "Cost")]
        public decimal Salary { get; set; }
        [ClassCopier(typeof(TestEmployee), "Address")]
        public string Address { get; set; }
        [ClassCopier(typeof(TestEmployee), "Spouse")]
        public string Spouse { get; set; }
    }

    public class TestEmployee
    {
        [ClassCopier(typeof(TestPerson), "Name")]
        [ClassCopier(typeof(TestPersonViewModel), "Name")]
        public string Name { get; set; }
        [ClassCopier(typeof(TestPerson), "DOB")]
        [ClassCopier(typeof(TestPersonViewModel), "Birthdate")]
        public System.DateTime Birthdate { get; set; }
        [ClassCopier(typeof(TestPerson), "Salary")]
        [ClassCopier(typeof(TestPersonViewModel), "Salary")]
        public decimal Cost { get; set; }

        [ClassCopier(typeof(TestPerson), "Address")]
        [ClassCopier(typeof(TestPersonViewModel), "Address")]
        public string Address { get; set; }
        [ClassCopier(typeof(TestPersonViewModel), "Position")]
        public int Position { get; set; }
        public string Spouse { get; set; }
    }

    public class TestPersonViewModel
    {
        public string Name { get; set; }
        public System.DateTime Birthdate { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; }
        public int Position { get; set; }
        public string Spouse { get; set; }
    }



    [TestClass]
    public class TestClassCopier
    {
        [TestMethod]
        public void ClassCopierNormal()
        {
            var pSource = new TestPerson()
            {
                Name = "test",
                Address = "address",
                DOB = new System.DateTime(1964, 3, 21),
                Salary = 25000,
                Spouse = "spouse"
            };
            var pTarget = new TestEmployee()
            {
                Name = "target",
                Address = "employee address",
                Birthdate = new System.DateTime(1962, 2, 2),
                Cost = 15000,
                Position = 5
            };

            ClassCopier<TestPerson, TestEmployee>.CopyClassProperties(pSource, pTarget);

            Assert.AreEqual(pSource.Name, pTarget.Name);
            Assert.AreEqual(pSource.Address, pTarget.Address);
            Assert.AreEqual(pSource.DOB, pTarget.Birthdate);
            Assert.AreEqual(pSource.Salary, pTarget.Cost);
            Assert.AreEqual("spouse", pSource.Spouse);
            Assert.AreEqual(5, pTarget.Position);
            Assert.IsNull(pTarget.Spouse);
        }
        [TestMethod]
        public void ClassCopierViewmodel()
        {
            var pSource = new TestPersonViewModel()
            {
                Name = "test",
                Address = "address",
                Birthdate = new System.DateTime(1964, 3, 21),
                Salary = 25000,
                Position = 4,
                Spouse = "spouse"
            };
            var pTarget = new TestEmployee()
            {
                Name = "target",
                Address = "employee address",
                Birthdate = new System.DateTime(1962, 2, 2),
                Cost = 15000,
                Position = 5
            };

            ClassCopier<TestPersonViewModel, TestEmployee>.CopyClassProperties(pSource, pTarget);

            Assert.AreEqual(pSource.Name, pTarget.Name);
            Assert.AreEqual(pSource.Address, pTarget.Address);
            Assert.AreEqual(pSource.Birthdate, pTarget.Birthdate);
            Assert.AreEqual(pSource.Salary, pTarget.Cost);
            Assert.AreEqual(pSource.Position, pTarget.Position);
            Assert.IsNull(pTarget.Spouse);
        }
        [TestMethod]
        public void ClassCopierReverse()
        {
            var pTarget = new TestPerson()
            {
                Name = "test",
                Address = "address",
                DOB = new System.DateTime(1964, 3, 21),
                Salary = 25000,
                Spouse = "spouse"
            };
            var pSource = new TestEmployee()
            {
                Name = "target",
                Address = "employee address",
                Birthdate = new System.DateTime(1962, 2, 2),
                Cost = 15000,
                Position = 5
            };

            ClassCopier<TestEmployee, TestPerson>.CopyClassProperties(pSource, pTarget);

            Assert.AreEqual(pSource.Name, pTarget.Name);
            Assert.AreEqual(pSource.Address, pTarget.Address);
            Assert.AreEqual(pSource.Birthdate, pTarget.DOB);
            Assert.AreEqual(pSource.Cost, pTarget.Salary);
            Assert.AreEqual(pSource.Spouse, pTarget.Spouse);
        }
    }
}
