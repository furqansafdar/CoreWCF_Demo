using System.Runtime.Serialization;

namespace ClientDomain
{
    [Serializable]
    [DataContract]
    public abstract class EntityBase : IExtensibleDataObject
    {
        #region IExtensibleDataObject Members

        [field: NonSerialized]
        public ExtensionDataObject ExtensionData { get; set; }

        #endregion
    }

    [Serializable]
    [DataContract]
    public abstract class Person : EntityBase
    {
        protected Person(string name)
        {
            Name = name;
        }

        [DataMember] public string Name { get; set; }
    }

    [Serializable]
    [DataContract]
    public class Manager : Person
    {
        public Manager(string name) : base(name) { }

        [DataMember] public List<Employee> Employees { get; set; } = new List<Employee>();

        public void AddEmployee(Employee employee)
        {
            employee.Manager = this;
            Employees.Add(employee);
        }
    }

    [Serializable]
    [DataContract]
    public class Employee : Person
    {
        public Employee(string name) : base(name) { }

        [DataMember] public Manager Manager { get; set; }

    }
}
