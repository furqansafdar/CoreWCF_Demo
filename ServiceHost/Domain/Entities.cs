namespace BusinessDomain
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
        [DataMember] public string Name { get; set; }
    }

    [Serializable]
    [DataContract]
    public class Manager : Person
    {
        [DataMember] public List<Employee> Employees { get; set; }
    }

    [Serializable]
    [DataContract]
    public class Employee : Person
    {
        [DataMember] public Manager Manager { get; set; }

    }
}
